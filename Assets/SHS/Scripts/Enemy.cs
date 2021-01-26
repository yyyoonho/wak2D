using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public float attackRange;
    public float knockbackTime = 0.6f;

    //공격, 순찰, 스프라이트를 담당하는 컴포넌트 클래스들.
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

    // 상태에 따라 다른 컴포넌트의 Update함수를 호출.
    void Update()
    {
        CheckDistance();
        switch (_state)
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

    //넉백 당하면 실행되는 코루틴
    private IEnumerator KnockBackCoroutine()
    {
        patrol_Component.KnockBack(_player.transform);
        sprite_Component.KnockBackSprite(knockbackTime - 0.2f, _player.transform);
        ChangeState(State.Damaged);

        yield return new WaitForSeconds(knockbackTime);

        ChangeState(State.Patrol);
    }

    //목표인 플레이어와의 거리를 감지하여 상태를 변경시키는 함수.
    private void CheckDistance()
    {
        if (_state == State.Damaged) return;

        if (attack_Component.isAttacking) return;

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
                attack_Component.Enter_Attack_State();
                sprite_Component.LookAtTarget(_player.transform);
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
