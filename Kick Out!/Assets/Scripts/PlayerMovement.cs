using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //SCRIPTS
    public FighterStats stats;
    public PlayerAttack attack;

    //COMPONENTS
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sp;
    public Transform groundCheck;

    //EXTRAS
    public LayerMask collisionLayer;

    //DATA
    private bool isBlocking;
    private bool isJumping = false;
    public bool isGrounded = false;
    
    private float moveSpeed;
    private float jumpForce;
    private float gravityScale;
    private float fallingGravityScale;
    private float groundCheckRadius;
    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = stats.moveSpeed.GetValue();
        jumpForce = stats.jumpForce.GetValue();
        gravityScale = stats.gravityScale.GetValue();
        fallingGravityScale = stats.fallingGravityScale.GetValue();
        groundCheckRadius = stats.groundCheckRadius.GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        //Detect if player is trying to move
        //Player can move only when he is not attacking
        if (!attack.isAttacking)
            horizontalInput = Input.GetAxis("Horizontal");

        //Detect if the players presses the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
            isJumping = true;

        //Animation
        animator.SetBool("IsJumping", !isGrounded);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

    void FixedUpdate() 
    {
        //OverlapArea creates a hitbox between 2 positions and checks if it is in collision with something
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        //Movement
        MoveHorizontal();
        Jump();
        Flip();
    }

    void MoveHorizontal()
    {
        //Cette fonction permet de déplacer le combattant horizontalement à l'aide des touches qui sont tag "Horizontal" (cf. dans les paramètres du projet)
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    void Jump()
    {
        //Cette fonction permet de faire saute le combattant en appuyant sur la touche espace (modifiable)
        
        //To jump, the player must press the space bar and be grounded
        if (isJumping && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
            isGrounded = false;
        }

        if (rb.velocity.y >= 0) //if the fighter is going up in is jumping, the gravity is normal
        {
            rb.gravityScale = gravityScale;
        }
        else //when the fighter is falling, increases the gravity so the jump looks more realistic
        {
            rb.gravityScale = fallingGravityScale;
        }
    }

    void Flip() 
    {
        if (horizontalInput > 0.1f) 
        {
            sp.flipX = false;
        } 
        else if (horizontalInput < -0.1f) 
        {
            sp.flipX = true;
        }
    }
}
