using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackP1 : PlayerAttack
{
    void Update()
    {
        if(Input.GetButtonDown("Punch P1"))    
        {
            animator.SetTrigger("Attack");
        }

        if(Input.GetButtonDown("Special P1"))    
        {
            animator.SetTrigger("Special");
        }
    }
}
