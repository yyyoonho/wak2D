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
    private bool isAttacking = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        enemy_Sprite_Component = GetComponent<Enemy_Sprite_Component>();
        enemy_Animator_Component = GetComponent<Enemy_Animator_Component>();
        attackTimer = TimeBetAttack;
    }

    public void RefreshAttackCycle()
    {
        attackTimer = 0f;
        isAttacking = false;
    }

    public void Attack_Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= TimeBetAttack && !isAttacking)
        {
            attackTimer = 0f;
            if (isRanged)
            {
                //원거리 공격인 경우
                enemy_Animator_Component.Trigger_Animator_Attack();
            }
            else
            {
                enemy_Sprite_Component.LookAtTarget(_player.transform);
                StartCoroutine(MeleeAttackCoroutine());
            }
        }
    }

    private void RangeAttack()
    {
        Projectile pj = Instantiate(projectile, transform.position, Quaternion.identity) as Projectile;
        pj.Init(_player, attackDamage);
    }

    private IEnumerator MeleeAttackCoroutine()
    {
        isAttacking = true;

        enemy_Animator_Component.Trigger_Animator_Attack();

        yield return new WaitForSeconds(SpendingTimeToAttack);

        isAttacking = false;

        attackTimer = 0f;
    }

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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, hitBox);
    }
}
