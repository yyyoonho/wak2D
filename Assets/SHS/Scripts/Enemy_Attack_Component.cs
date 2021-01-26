using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Component : MonoBehaviour
{
    public bool isRanged;
    public float attackDamage;
    public float TimeBetAttack;
    public float SpendingTimeToAttack;
    public Projectile projectile;
    public Transform attackPoint;
    public Vector2 hitBox;

    private GameObject _player;
    private Enemy_Sprite_Component enemy_Sprite_Component;
    private Enemy_Animator_Component enemy_Animator_Component;
    private float attackTimer;
    public bool isAttacking = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        enemy_Sprite_Component = GetComponent<Enemy_Sprite_Component>();
        enemy_Animator_Component = GetComponent<Enemy_Animator_Component>();
        attackTimer = TimeBetAttack;
    }

    public void Enter_Attack_State()
    {
        enemy_Animator_Component.Set_Animator_Waiting(true);
    }

    public void RefreshAttackCycle()
    {
        attackTimer = attackTimer / 2;
        isAttacking = false;
        enemy_Animator_Component.Set_Animator_Waiting(false);
    }

    public void Attack_Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= TimeBetAttack && !isAttacking)
        {
            isAttacking = true;
            attackTimer = 0f;
            enemy_Sprite_Component.LookAtTarget(_player.transform);

            if (isRanged)
            {
                //원거리 공격인 경우
                enemy_Animator_Component.Trigger_Animator_Attack();
            }
            else
            {
                StartCoroutine(MeleeAttackCoroutine());
            }
        }
    }

    //원거리 공격 함수
    private void RangeAttack()
    {
        isAttacking = false;
        Projectile pj = Instantiate(projectile, transform.position, Quaternion.identity) as Projectile;
        pj.Init(_player, attackDamage);
    }

    //근접공격 코루틴
    private IEnumerator MeleeAttackCoroutine()
    {
        enemy_Animator_Component.Trigger_Animator_Attack();

        yield return new WaitForSeconds(SpendingTimeToAttack);

        attackTimer = 0f;
    }

    //박스안의 콜라이더들에게 데미지를 주는 근접공격 함수
    private void MeleeAttack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackPoint.position, hitBox, 0);
        foreach (Collider2D coll in collider2Ds)
        {
            if (coll.CompareTag("Player"))
            {
                coll.GetComponent<LivingEntity>().GetDamaged(attackDamage);
            }
        }
        isAttacking = false;
    }

    //박스의 크기를 Gizmos로 보여주는 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, hitBox);
    }
}
