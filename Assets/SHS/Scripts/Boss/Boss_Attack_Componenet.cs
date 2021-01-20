using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack_Componenet : MonoBehaviour
{
    public float attackDamage;
    public Transform attackPoint;
    public Vector2 hitBox;

    private GameObject _player;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MeleeAttack(float attackDamage)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, hitBox);
    }
}
