using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attack : MonoBehaviour
{
    //##############################################################################################################################################
    //UNITY COMPONENTS
    public FighterStats stats;

    //##############################################################################################################################################
    //DATA VARIABLES
    private float _attackRange;

    public Vector3 attackOffset;
    public LayerMask attackMask;

    void Start()
    {   
        _attackRange = stats.attackRange.GetValue();
    }

    public void Punch() 
    {
        Vector3 pos = transform.position;
        pos += transform.right * stats.attackRange.GetValue();
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, _attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<FighterStats>().TakeDamage(stats.damage.GetValue());
        }
    }
}
