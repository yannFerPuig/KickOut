using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // References to other components and scripts
    private FighterStats stats;
    private AIMovement movement;
    private AIAttack attack;
    private Fighter fighter;

    // AI specific data
    public Transform playerTransform;
    private float decisionTimer;
    private float decisionCooldown = 1f; // Cooldown between decisions

    // Random movement variables
    private int randomDirection;
    private float randomMoveDuration;
    private float moveTimer;

    // Crouch variables
    private bool isCrouching = false;
    private float crouchTimer = 0f;
    private float crouchCooldownTimer = 0f;
    public float crouchDuration = 2.0f;
    public float crouchCooldown = 5.0f;
    public float crouchChance = 0.2f; // 20% chance to crouch

    void Start()
    {
        // Get references to the necessary components
        stats = GetComponent<FighterStats>();
        movement = GetComponent<AIMovement>();
        attack = GetComponent<AIAttack>();
        fighter = GetComponent<Fighter>();

        // Find the player transform (assuming there's only one player)
        playerTransform = fighter.enemy.transform;

        // Initialize decision timer
        decisionTimer = decisionCooldown;

        // Initialize random movement variables
        randomDirection = 1; // 1 for right, -1 for left
        randomMoveDuration = Random.Range(0.5f, 2f);
        moveTimer = randomMoveDuration;      

        stats.attackPoint = GameObject.FindGameObjectWithTag("AttackPoint2");
    }

    void Update()
    {
        // Update the decision timer
        decisionTimer -= Time.deltaTime;

        // Make decisions based on the cooldown
        if (decisionTimer <= 0)
        {
            MakeDecision();
            decisionTimer = decisionCooldown;
        }

        // Update crouch cooldown timer
        if (crouchCooldownTimer > 0)
        {
            crouchCooldownTimer -= Time.deltaTime;
        }

        // Ensure AI faces the player
        fighter.LookAtEnemy();
    }

    void MakeDecision()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (isCrouching)
        {
            crouchTimer -= Time.deltaTime;
            if (crouchTimer <= 0)
            {
                EndCrouch();
            }
        }
        else if (crouchCooldownTimer <= 0 && Random.value < crouchChance)
        {
            //StartCrouch();
        }
        else if (distanceToPlayer < attack.attackRange)
        {
            // Attack or block randomly
            int randomAction = Random.Range(0, 10);

            if (randomAction < 6) // 60% chance to attack
            {
                PerformAttack();
            }
            else if (randomAction < 9) // 30% chance to block
            {
                PerformBlock();
            }
            else // 10% chance to perform special move
            {
                PerformSpecial();
            }
        }
        else
        {
            // Move randomly or towards the player
            int randomMoveAction = Random.Range(0, 10);

            if (randomMoveAction < 6) // 60% chance to move towards player
            {
                MoveTowardsPlayer();
            }
            else // 40% chance to move randomly
            {
                MoveRandomly();
            }
        }
    }

    void StartCrouch()
    {
        isCrouching = true;
        crouchTimer = crouchDuration;
        crouchCooldownTimer = crouchCooldown;

        // Adjust collider size and offset for crouching
        stats.capsuleCollider2D.size = new Vector2(stats.crouchWidth, stats.crouchHeight);
        stats.capsuleCollider2D.offset = new Vector2(stats.crouchOffsetX, stats.crouchOffsetY);
        movement.isCrouching = true;
    }

    void EndCrouch()
    {
        isCrouching = false;

        // Revert collider size and offset
        stats.capsuleCollider2D.size = new Vector2(stats.width, stats.height);
        stats.capsuleCollider2D.offset = new Vector2(stats.offsetX, stats.offsetY);
        movement.isCrouching = false;
    }

    void PerformAttack()
    {
        // Trigger the attack animation and logic
        attack.animator.SetTrigger("Attack");
    }

    void PerformSpecial()
    {
        // Trigger the special attack animation and logic
        attack.animator.SetTrigger("Special");
    }

    void PerformBlock()
    {
        // Start blocking for a short duration
        StartCoroutine(BlockCoroutine(Random.Range(0.5f, 2.5f))); // Block for 0.5 to 2.5 seconds
    }

    IEnumerator BlockCoroutine(float duration)
    {
        movement.isBlocking = true;
        attack.isAttacking = true; // Prevent attacking while blocking
        attack.animator.SetBool("IsBlocking", true);
        yield return new WaitForSeconds(duration);
        movement.isBlocking = false;
        attack.isAttacking = false;
        attack.animator.SetBool("IsBlocking", false);
    }

    void MoveTowardsPlayer()
    {
        // Move the AI towards the player's position
        if (transform.position.x < playerTransform.position.x)
        {
            movement.horizontalInput = 1; // Move right
        }
        else
        {
            movement.horizontalInput = -1; // Move left
        }
    }

    void MoveRandomly()
    {
        // Random movement direction and duration
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            randomDirection = Random.Range(0, 2) == 0 ? -1 : 1; // Randomize direction
            randomMoveDuration = Random.Range(0.5f, 2f); // Randomize duration
            moveTimer = randomMoveDuration;
        }

        movement.horizontalInput = randomDirection;
    }
}

