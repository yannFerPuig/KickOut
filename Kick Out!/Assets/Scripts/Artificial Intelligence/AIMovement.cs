using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private FighterStats stats;
    private PlayerAttack attack;
    public Fighter fighter;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform groundCheck;

    private LayerMask collisionLayer;

    private bool isJumping = false;
    private bool isGrounded = false;
    private bool isBlockCooldown = false;
    public bool isBlocking = false;
    public bool isCrouching = false;

    private float moveSpeed;
    private float jumpForce;
    private float blockCD;
    private float blockCooldownTimer;
    private float gravityScale;
    private float fallingGravityScale;
    private float groundCheckRadius = 0.5f;
    public float horizontalInput;

    private GameObject player;

    void Start()
    {
        stats = gameObject.GetComponent<FighterStats>();
        attack = gameObject.GetComponent<PlayerAttack>();   
        fighter = gameObject.GetComponent<Fighter>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        
        groundCheck = gameObject.transform.Find("GroundCheck");

        collisionLayer = 1 << LayerMask.NameToLayer("Default");

        moveSpeed = stats.moveSpeed;
        blockCD = stats.blockCD;
        jumpForce = stats.jumpForce;
        gravityScale = stats.gravityScale;
        fallingGravityScale = stats.fallingGravityScale;
        groundCheckRadius = stats.groundCheckRadius;

        blockCooldownTimer = 0f;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isBlockCooldown) 
        {
            blockCooldownTimer += Time.deltaTime;

            if (blockCooldownTimer > 1.5f)
            {
                isBlockCooldown = false;
                blockCooldownTimer = 0;
            }
        }

        HandleMovement();
        HandleBlocking();

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));   
    }

    void FixedUpdate() 
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        MoveHorizontal();
        Jump();
        Crouch();

        fighter.LookAtEnemy();
    }

    void HandleMovement()
    {
        float distance = player.transform.position.x - transform.position.x;

        if (Mathf.Abs(distance) > 1f)
        {
            if (distance > 0)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void HandleBlocking()
    {
        if (blockCD > 0  && !isBlockCooldown)
        {
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
            isBlocking = false;
            blockCD += Time.deltaTime * 0.5f;
            if (blockCD > stats.blockCD) blockCD = stats.blockCD; 
        }
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

    void Crouch()
    {
        if (isGrounded && isCrouching)
        {   
            animator.SetBool("Crouch", true);
        }
    }
}
