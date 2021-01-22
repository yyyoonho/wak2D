using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack_Componenet : MonoBehaviour
{
    public float attackDamage;
    public Transform shockWavePoint;
    public Transform attackPoint;
    public Vector2 hitBox;

    private GameObject _player;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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

    private void ShockWave()
    {
        GameObject g = EffectPool.instance.GetObject("ShockWave");
        g.transform.position = shockWavePoint.position;
        if(_player.transform.position.x >= transform.position.x)
        {
            g.transform.localScale = new Vector3(-(g.transform.localScale.x), g.transform.localScale.y, g.transform.localScale.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, hitBox);
    }
}
