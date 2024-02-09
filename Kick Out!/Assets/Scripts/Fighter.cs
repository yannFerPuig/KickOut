using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //##############################################################################################################################################
    //ATTRIBUTES
    private float _health;
    private float _attack;
    private float _defense;

    //to know if the fighter is in blocking position or not
    private bool _isBlocking = false;

    //to know if the fighter is attacking to avoid getting multiples attacks at same time
    private bool _isAttacking = false;
    
    //the time between the attacks
    private float _timeBetweenAttacks = 0;

    //##############################################################################################################################################
    //UNITY COMPONENTS
    private Rigidbody2D rb;

    public SpriteRenderer sp;

    public Animator animator;

    //##############################################################################################################################################
    //ANIMATIONS
    private bool _isPunch = false;
    private bool _isJab = false;

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
    public LayerMask collisionLayer;
    public float groundCheckRadius;

    //##############################################################################################################################################
    void Start()
    {
        //Get all components that we nned to acces
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //##############################################################################################################################################
    //Update et FixedUpdate sont toutes les deux des fonctions qui sont appelées à certains intervalle de temps
    
    //Update est utilisée pour les inputs du joueur et les modifications en temps réel
    void Update() 
    {
        //Detect when the player presses the keys binded to horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        //Animation
        animator.SetBool("IsJumping", isJumping);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsPunch", _isPunch);
        animator.SetBool("IsJab", _isJab);

        //Detect if the players presses the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
            isJumping = true;

        //Blocking
        Blocking();

        //Attacks
        BasicAttack();

        Flip(_moveSpeed);
    }

    
    //FixedUpdate est appelée toutes les certains frames (choses que l'on peut modifier dans les paramètres de projet) - Utilisée pour tout ce qui concerne la physique et les rigidbody
    void FixedUpdate()
    {
        //OverlapArea creates a hitbox between 2 positions and checks if it is in collision with something
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        //Movement
        MoveHorizontal();
        Jump();
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

    //##############################################################################################################################################
    //ATTACK FUNCTIONS
    void BasicAttack() 
    {
        //Cette fonction va permettre de gérer toutes les attaques basiques des combattants (coup de poing, coup de pied, ...)

        //Jab
        if(Input.GetKey(KeyCode.X) && _isAttacking == false)
        {
            _isAttacking = true; 
            _isJab = true;
        }

        //Punch
        if(Input.GetKey(KeyCode.Z) && _isAttacking == false)
        {
            _isAttacking = true; 
            _isPunch = true;
        }

        //if player has made an attack, turn _isAttacking to false after x seconds to avoid spamming
        if(_isAttacking) 
        {
            _timeBetweenAttacks += Time.deltaTime;

            //modify the .3f depending on the time you want between attacks (default .3f)
            if (_timeBetweenAttacks > .6f)
            {
                _timeBetweenAttacks = 0f;

                StartCoroutine(MyFunctionAfterDelay(.1f));
                StopCoroutine(MyFunctionAfterDelay(.5f));
            }
        }

        if(_isBlocking) 
        {
            _isBlocking = true;
        }
    }

    void Blocking() 
    {
        //Permet de savoir si le joueur est en train de bloquer ou non
        if (Input.GetKeyDown(KeyCode.B) && !_isAttacking) 
        {
            _isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            _isBlocking = false;
        }
    }

    void Flip(float _velocity) 
    {
        if (_velocity > 0.1f) 
        {
            sp.flipX = false;
        } 
        else if (_velocity < -0.1f) 
        {
            sp.flipX = true;
        }
    }

    IEnumerator MyFunctionAfterDelay(float delay)
    {
        //Cette fonction permet de déclencher une action après un certain temps (en lien avec StartCoroutine())
        //Évite que le joueur puisse spammer les attaques

        yield return new WaitForSeconds(delay);

        _isAttacking = false;
        _isJab = false;
        _isPunch = false;
    }
}
