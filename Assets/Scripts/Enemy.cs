using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackRange;

    private Enemy_Attack_Component attack_Component;
    private Enemy_Patrol_Component patrol_Component;
    private GameObject _player;

    public enum State
    {
        Patrol,
        Attack
    }
    public State _state;

    // Start is called before the first frame update
    void Start()
    {
        attack_Component = this.GetComponent<Enemy_Attack_Component>();
        patrol_Component = this.GetComponent<Enemy_Patrol_Component>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        switch(_state)
        {
            case State.Patrol:
                patrol_Component.Patrol_Update();
                break;
            case State.Attack:
                attack_Component.Attack_Update();
                break;
        }
    }

    private void CheckDistance()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        if(distance <= attackRange)
        {
            if (_state == State.Attack) return
                    ;
            ChangeState(State.Attack);
        }
        else
        {
            ChangeState(State.Patrol);
        }
    }

    private void ChangeState(State state)
    {
        ExitState();
        _state = state;
        EnterState();
    }

    public void EnterState()
    {
        switch (_state)
        {
            case State.Patrol:
                patrol_Component.EnterPatrol();
                break;
            case State.Attack:
                break;
        }
    }

    public void ExitState()
    {
        switch (_state)
        {
            case State.Patrol:
                patrol_Component.StopMoving();
                break;
            case State.Attack:
                attack_Component.RefreshAttackCycle();
                break;
        }
    }
}
