using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDetect : StateMachineBehaviour
{
    Transform player;
    Slime slime;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector2.Distance(player.position , animator.transform.position);

        if (distance <= animator.transform.GetComponent<Slime>().detectRange)
        {
            animator.SetTrigger("Chase");
            Debug.Log("Player detected!");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Chase");
    }
}
