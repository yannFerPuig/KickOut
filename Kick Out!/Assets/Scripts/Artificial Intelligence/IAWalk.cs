using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAWalk : StateMachineBehaviour
{
    //SCRIPTS 
    IA ia;
    public RoundTimer startRoundTimer;

    //COMPONENTS
    public Transform player;
    Rigidbody2D rb;

    //Data
    public float speed = 2.5f;
    public float attackRange = 1.7f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //SCRIPTS
        ia = animator.GetComponent<IA>();
        startRoundTimer = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RoundTimer>();

        //COMPONENTS
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (startRoundTimer != null && startRoundTimer.fightStarted)
        {
            ia.LookAtPlayer();

            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        
            rb.MovePosition(newPos);

            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
            animator.SetTrigger("Punch");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Punch");
    }
}
