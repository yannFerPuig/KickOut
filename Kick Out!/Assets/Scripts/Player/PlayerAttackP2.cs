using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackP2 : PlayerAttack
{
    void Update()
    {
        aCD -= Time.deltaTime;
        aCDSpe -= Time.deltaTime;

        if(Input.GetButtonDown("Punch P2")  && aCD <= 0)    
        {
            animator.SetTrigger("Attack");
            aCD = stats.attackCooldown;
        }

        if(Input.GetButtonDown("Special P2") && aCDSpe <= 0)    
        {
            animator.SetTrigger("Special");
            aCDSpe = stats.attackCooldownSpe;
        }
    }
}
