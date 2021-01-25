using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack_Componenet : MonoBehaviour
{
    public float attackDamage;
    public Transform shockWavePoint;
    public Transform thunderBallPoint;
    public Transform attackPoint;
    public List<Transform> sideTransformList;
    public Vector2 hitBox;

    private bool isLookingLeft = true;
    private GameObject _player;
    public bool isAttacking = false;
    private GameObject thunderBall;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void LookAtPlayer()
    {
        if (_player.transform.position.x >= transform.position.x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    */

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

    private void ThunderBall()
    {
        GameObject g = EffectPool.instance.GetObject("ThunderBall");
        g.transform.position = thunderBallPoint.position;
        if(thunderBall == null)
        {
            thunderBall = g;
        }
    }

    private void ThrowThunderBall()
    {
        if (thunderBall != null)
        {
            thunderBall.GetComponent<ThunderBall>().Shot(_player.transform);
            thunderBall = null;
        }
    }

    private void TeleportToSide()
    {
        int r = Random.Range(0, sideTransformList.Count);
        transform.position = sideTransformList[r].position;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, hitBox);
    }
}
