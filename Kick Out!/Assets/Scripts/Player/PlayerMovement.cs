using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //SCRIPTS
    public FighterStats stats;
    public PlayerAttack attack;
    public StartRoundTimer startRoundTimer;
    public Player player;

    //COMPONENTS
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sp;
    public Transform groundCheck;
    public Transform attackPoint;

    //EXTRAS
    public LayerMask collisionLayer;

    //DATA
    private bool isJumping = false;
    public bool isGrounded = false;
    public bool isFlipped = false;

    private float moveSpeed;
    private float jumpForce;
    private float gravityScale;
    private float fallingGravityScale;
    private float groundCheckRadius = 0.5f;
    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<FighterStats>();
        if (stats is LouisStats louisStats) louisStats.Initialize();
        attack = gameObject.GetComponent<PlayerAttack>();   
        startRoundTimer = GameObject.FindGameObjectWithTag("Canvas").GetComponent<StartRoundTimer>();
        player = gameObject.GetComponent<Player>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        attackPoint = gameObject.transform.Find("AttackPoint");

        collisionLayer = 1 << LayerMask.NameToLayer("Default");

        moveSpeed = stats.moveSpeed;
        jumpForce = stats.jumpForce;
        gravityScale = stats.gravityScale;
        fallingGravityScale = stats.fallingGravityScale;
        groundCheckRadius = stats.groundCheckRadius;
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
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));   
    }

    void FixedUpdate() 
    {
        //OverlapArea creates a hitbox between 2 positions and checks if it is in collision with something
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        //Movement
        MoveHorizontal();
        Jump();

        player.LookAtEnemy();
        //if the player is attacking, we don't want to allow him to flip
        // if(!attack.isAttacking)
        //     Flip();
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
            animator.SetTrigger("Jump");
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

    // void Flip() 
    // {
    //     if (horizontalInput > 0.1f) 
    //     {
    //         isFlipped = false;
    //         sp.flipX = false;
    //     } 
    //     else if (horizontalInput < -0.1f) 
    //     {
    //         isFlipped = true;
    //         sp.flipX = true;
    //     }
    // }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(groundCheck.position, 0.5f);
    }
}
