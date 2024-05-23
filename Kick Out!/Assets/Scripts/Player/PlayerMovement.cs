using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public Slider cbBlock;

    //EXTRAS
    public LayerMask collisionLayer;
    public LayerMask blockLayer;

    //DATA
    private bool isJumping = false;
    public bool isGrounded = false;
    public bool canBlock = false;

    public float moveSpeed;
    private float jumpForce;
    public float blockCD;
    private float gravityScale;
    private float fallingGravityScale;
    private float groundCheckRadius = 0.5f;
    public float horizontalInput;

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

        cbBlock = stats.cdBlock.GetComponent<Slider>();

        collisionLayer = 1 << LayerMask.NameToLayer("Default");
        blockLayer = LayerMask.GetMask("IA");

        moveSpeed = stats.moveSpeed;
        blockCD = stats.blockCD;
        jumpForce = stats.jumpForce;
        gravityScale = stats.gravityScale;
        fallingGravityScale = stats.fallingGravityScale;
        groundCheckRadius = stats.groundCheckRadius;
    }

    void Update()
    {
        //Detect if player is trying to move
        //Player can move only when he is not attacking
        if (!attack.isAttacking)
            horizontalInput = Input.GetAxis("Horizontal");

        //Detect if the players presses the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
            isJumping = true;

        if (Input.GetKey(KeyCode.B) && blockCD > 0)
        {
            moveSpeed = 0;
            animator.SetBool("IsBlocking", true);

            blockCD -= Time.deltaTime;
            if (blockCD < 0) blockCD = 0; 
        }
        else
        {
            moveSpeed = stats.moveSpeed;
            animator.SetBool("IsBlocking", false);

            blockCD += Time.deltaTime * 0.5f;
            if (blockCD > stats.blockCD) blockCD = stats.blockCD;  // Prevent blockCD from exceeding its maximum value
        }

        cbBlock.value = blockCD;

        cbBlock.value = blockCD;

        //Animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));   
    }

    void FixedUpdate() 
    {
        //OverlapArea creates a hitbox between 2 positions and checks if it is in collision with something
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        canBlock = Physics2D.OverlapCircle(transform.position, stats.blockRadius, blockLayer);

        //Movement
        MoveHorizontal();
        Jump();

        //Block();

        player.LookAtEnemy();
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

    void Block()
    {
        //If the player is going backwards and the enemy is in range of blocking
        if (horizontalInput < 0 && canBlock)
        {
            animator.SetBool("IsBlocking", true);
            moveSpeed = stats.blockingMoveSpeed;
        }
        else 
        {
            animator.SetBool("IsBlocking", false);
            moveSpeed = stats.moveSpeed;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, stats.blockRadius);
    }
}
