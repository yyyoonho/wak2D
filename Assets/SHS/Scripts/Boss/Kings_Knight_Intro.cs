using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kings_Knight_Intro : StateMachineBehaviour
{
    private Transform rayPoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rayPoint = animator.GetComponent<BossControl>().groundCheck;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D ray = Physics2D.Raycast(rayPoint.position, Vector2.down, 0.2f, layerMask);
        if(ray)
        {
            animator.SetTrigger("EndIntro");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {

    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
