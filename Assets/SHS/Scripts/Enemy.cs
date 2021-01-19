using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public float attackRange;
    public float knockbackTime = 0.6f;

    private Enemy_Attack_Component attack_Component;
    private Enemy_Patrol_Component patrol_Component;
    private Enemy_Sprite_Component sprite_Component;
    private GameObject _player;

    public enum State
    {
        Patrol,
        Attack,
        Damaged
    }
    public State _state;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attack_Component = this.GetComponent<Enemy_Attack_Component>();
        patrol_Component = this.GetComponent<Enemy_Patrol_Component>();
        sprite_Component = this.GetComponent<Enemy_Sprite_Component>();
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
            case State.Damaged:
                break;
        }
    }

    public override void GetDamaged(float damage)
    {
        base.GetDamaged(damage);
        StartCoroutine(KnockBackCoroutine());
    }

    private IEnumerator KnockBackCoroutine()
    {
        patrol_Component.KnockBack(_player.transform);
        sprite_Component.KnockBackSprite(knockbackTime - 0.2f, _player.transform);
        ChangeState(State.Damaged);

        yield return new WaitForSeconds(knockbackTime);

        ChangeState(State.Patrol);
    }

    private void CheckDistance()
    {
        if (_state == State.Damaged) return;

        float distance = Vector2.Distance(transform.position, _player.transform.position);

        if(distance <= attackRange)
        {
            if (_state == State.Attack) 
                return;
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
            case State.Damaged:
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
            case State.Damaged:
                break;
        }
    }
}
