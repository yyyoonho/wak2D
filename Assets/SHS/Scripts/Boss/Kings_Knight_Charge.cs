using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kings_Knight_Charge : StateMachineBehaviour
{
    public float TimeToCharge;

    private float chargeTimer;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chargeTimer = 0f;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chargeTimer += Time.deltaTime;
        if(chargeTimer >= TimeToCharge)
        {
            animator.SetTrigger("EndCharging");
        }
    }
}
