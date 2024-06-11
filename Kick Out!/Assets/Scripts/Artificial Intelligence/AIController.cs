using System.Collections;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // References to other components and scripts
    private FighterStats stats;
    private PlayerMovement movement;
    private PlayerAttack attack;
    private Fighter fighter;

    // AI specific data
    private Transform playerTransform;
    private float decisionTimer;
    private float decisionCooldown = 1f; // Cooldown between decisions

    void Start()
    {
        // Get references to the necessary components
        stats = GetComponent<FighterStats>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        fighter = GetComponent<Fighter>();

        // Find the player transform (assuming there's only one player)
        playerTransform = fighter.enemy.transform;

        // Initialize decision timer
        decisionTimer = decisionCooldown;
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

        // Ensure AI faces the player
        fighter.LookAtEnemy();
    }

    void MakeDecision()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Example decision making based on distance
        if (distanceToPlayer < attack.attackRange)
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
            // Move towards the player
            MoveTowardsPlayer();
        }
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
        StartCoroutine(BlockCoroutine(1f)); // Block for 1 second
    }

    IEnumerator BlockCoroutine(float duration)
    {
        movement.isBlocking = true;
        attack.isAttacking = true; // Prevent attacking while blocking
        yield return new WaitForSeconds(duration);
        movement.isBlocking = false;
        attack.isAttacking = false;
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
}