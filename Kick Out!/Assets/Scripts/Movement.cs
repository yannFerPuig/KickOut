using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //##############################################################################################################################################
    //UNITY COMPONENTS
    private Rigidbody2D rb;

    public SpriteRenderer sp;

    public Animator animator;
    public LayerMask collisionLayer;


    //##############################################################################################################################################
    //to know if the fighter is in blocking position or not
    private bool _isBlocking = false;


    //##############################################################################################################################################
    //MOVE VARIABLES
    private float _moveSpeed = 5f;
    float horizontalInput = 0;
    private float _blockingSpeed = 2.5f;


    //##############################################################################################################################################
    //JUMP VARIABLES

    //if gravityScale is increased, augment also jumpForce or the fighter will not jump
    public float jumpForce = 20f; //jumpForce of the fighter
    public float gravityScale = 10f; //gravity when the fighter is going up (1st part of jump)
    public float fallingGravityScale = 40f; //gravity when the fighter is falling (2n part of jump)

    public bool isJumping = false;
    public bool isGrounded = false; //check if the player is on the ground to avoid mid-air jumps

    //Gestion de collision pour éviter les sauts à l'infini
    public Transform groundCheck; //allows to check the left foot
    public float groundCheckRadius;

    //##############################################################################################################################################
    // Start is called before the first frame update
    void Start()
    {
        //Get all components that we nned to acces
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Detect when the player presses the keys binded to horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        //Animation
        animator.SetBool("IsJumping", !isGrounded);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        //Detect if the players presses the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
            isJumping = true;

        //Blocking
        Blocking();
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

    //##############################################################################################################################################
    //MOVEMENT FUNCTIONS
    void MoveHorizontal()
    {
        //Cette fonction permet de déplacer le combattant horizontalement à l'aide des touches qui sont tag "Horizontal" (cf. dans les paramètres du projet)
        
        float currentSpeed = _moveSpeed;

        //if the fighter is blocking, the move speed is reduced
        if (_isBlocking)
            currentSpeed = _blockingSpeed;

        Vector2 movement = new Vector2(horizontalInput * currentSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    void Jump()
    {
        //Cette fonction permet de faire saute le combattant en appuyant sur la touche espace (modifiable)
        //Le joueur ne peut pas sauter s'il est en train de bloquer
        if (isJumping && isGrounded == true && _isBlocking == false)
        {
            animator.SetBool("IsJumping", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
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

    void Blocking() 
    {
        //Permet de savoir si le joueur est en train de bloquer ou non
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            _isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            _isBlocking = false;
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
