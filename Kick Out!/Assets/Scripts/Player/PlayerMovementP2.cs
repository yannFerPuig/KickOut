using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementP2 : PlayerMovement
{
    void Update()
    {
        //Detect if player is trying to move
        //Player can move only when he is not attacking
        if (!attack.isAttacking && !isCrouching)
        {
            horizontalInput = Input.GetAxis("Horizontal P2");
        }

        if (isBlockCooldown) 
        {
            blockCooldownTimer += Time.deltaTime;

            if (blockCooldownTimer > 1.5f)
            {
                isBlockCooldown = false;
                blockCooldownTimer = 0;
            }
        }

        if (Input.GetButtonDown("Crouch P2"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch P2"))
        {
            isCrouching = false;
            animator.SetBool("Crouch", false);
        }

        //Detect if the players presses the jump button
        if (Input.GetButtonDown("Jump P2") && isGrounded && !isCrouching)
        {
            isJumping = true;
        }

        if (Input.GetButton("Block P2") && blockCD > 0  && !isBlockCooldown)
        {
            moveSpeed = 0;
            animator.SetBool("IsBlocking", true);

            isBlocking = true;

            blockCD -= Time.deltaTime;
            if (blockCD < 0) 
            {
                blockCD = 0; 
                isBlockCooldown = true;
            }
        }
        else
        {
            moveSpeed = stats.moveSpeed;
            animator.SetBool("IsBlocking", false);

            isBlocking = false;

            blockCD += Time.deltaTime * 0.5f;
            if (blockCD > stats.blockCD) blockCD = stats.blockCD; 
        }

        blockSlider.value = blockCD;

        //Animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));   
    }
}
