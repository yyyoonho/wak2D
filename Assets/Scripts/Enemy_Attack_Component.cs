using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Component : MonoBehaviour
{
    public bool isRanged;
    public float TimeBetAttack;
    public GameObject projectile;

    private GameObject _player;
    private Enemy_Sprite_Component enemy_Sprite_Component;
    private float attackTimer;
    private bool isAttacking = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        enemy_Sprite_Component = GetComponent<Enemy_Sprite_Component>();
        attackTimer = TimeBetAttack;
    }

    public void RefreshAttackCycle()
    {
        attackTimer = TimeBetAttack;
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
            }
            else
            {
                enemy_Sprite_Component.LookAtTarget(_player.transform);
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        //공격 코드

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Attacked");

        isAttacking = false;

        attackTimer = 0f;
    }
}
