using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kings_Knight_Pattern : StateMachineBehaviour
{
    public int phaseNum;
    public float TimeBetAttack;
    public bool isAttacking = false;

    private GameObject _player;
    private Boss_Attack_Componenet boss_attack;
    private float attackTimer;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAttacking = false;
        attackTimer = 0f;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= TimeBetAttack && !isAttacking)
        {
            isAttacking = true;
            string s = "Pattern" + phaseNum.ToString();
            animator.SetTrigger(s);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimer = 0f;
    }
}
