using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackP1 : PlayerAttack
{
    void Update()
    {
        aCD -= Time.deltaTime;
        aCDSpe -= Time.deltaTime;

        if(Input.GetButtonDown("Punch P1") && aCD <= 0)     
        {
            animator.SetTrigger("Attack");
            aCD = stats.attackCooldown;
        }

        if(Input.GetButtonDown("Special P1") && aCDSpe <= 0)    
        {
            animator.SetTrigger("Special");
            aCDSpe = stats.attackCooldownSpe;
        }
    }
}
