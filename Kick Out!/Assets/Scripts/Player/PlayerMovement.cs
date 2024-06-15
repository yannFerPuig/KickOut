using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // //SCRIPTS
    public FighterStats stats;
    public PlayerAttack attack;
    public Fighter fighter;

    //COMPONENTS
    public Rigidbody2D rb;
    public Animator animator;
    public Transform groundCheck;
    public Slider blockSlider;

    //EXTRAS
    public LayerMask collisionLayer;

    //DATA
    public bool isJumping = false;
    public bool isGrounded = false;
    public bool isBlockCooldown = false;
    public bool isBlocking = false;
    public bool isCrouching = false;

    public float moveSpeed;
    public float jumpForce;
    public float blockCD;
    public float blockCooldownTimer;
    public float gravityScale;
    public float fallingGravityScale;
    public float groundCheckRadius = 0.5f;
    public float horizontalInput;

    public void Start()
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

    public void FixedUpdate() 
    {
        //OverlapArea creates a hitbox between 2 positions and checks if it is in collision with something
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        if (isCrouching)
        {
            Vector2 newOffset = new Vector2(stats.crouchOffsetX, stats.crouchOffsetY);

            stats.capsuleCollider2D.size = new Vector2(stats.crouchWidth, stats.crouchHeight);
            stats.capsuleCollider2D.offset = newOffset;//new Vector2(stats.crouchOffsetX, stats.crouchOffsetY * 5);
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

    public void MoveHorizontal()
    {
        //Cette fonction permet de déplacer le combattant horizontalement à l'aide des touches qui sont tag "Horizontal" (cf. dans les paramètres du projet)
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    public void Jump()
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

    public void Crouch()
    {
        if (isGrounded && isCrouching)
        {   
            animator.SetBool("Crouch", true);
            horizontalInput = 0f;
        }
    }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.yellow;

    //     Gizmos.DrawSphere(groundCheck.transform.position, groundCheckRadius);
    // }
}
