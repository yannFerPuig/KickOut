using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //SCRIPTS
    private FighterStats stats;
    private PlayerAttack attack;
    public Fighter fighter;

    //COMPONENTS
    private Rigidbody2D rb;
    private Animator animator;
    private Transform groundCheck;
    public Slider blockSlider;

    //EXTRAS
    private LayerMask collisionLayer;

    //DATA
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

        if (gameObject.CompareTag("Player") || gameObject.CompareTag("Player1"))
        {
            blockSlider = GameObject.Find("BlockP1").GetComponent<Slider>();
        }
        else if (gameObject.tag == "AI" || gameObject.tag == "Player2")
        {
            blockSlider = GameObject.Find("BlockP2").GetComponent<Slider>();
        }
    }

    void Update()
    {
        //Detect if player is trying to move
        //Player can move only when he is not attacking
        if (!attack.isAttacking && !isCrouching)
        {
            horizontalInput = Input.GetAxis("Horizontal");
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouching = false;
            animator.SetBool("Crouch", false);
        }

        //Detect if the players presses the jump button
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.B) && blockCD > 0  && !isBlockCooldown)
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

    void FixedUpdate() 
    {
        //OverlapArea creates a hitbox between 2 positions and checks if it is in collision with something
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        if (isCrouching)
        {
            stats.capsuleCollider2D.size = new Vector2(stats.crouchWidth, stats.crouchHeight);
            stats.capsuleCollider2D.offset = new Vector2(stats.crouchOffsetX, stats.crouchOffsetY);
        }
        else 
        {
            stats.capsuleCollider2D.size = new Vector2(stats.width, stats.height);
            stats.capsuleCollider2D.offset = new Vector2(stats.offsetX, stats.offsetY);
        }

        //Movement
        MoveHorizontal();
        Jump();
        Crouch();

        fighter.LookAtEnemy();
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
            horizontalInput = 0f;
        }
    }
}
