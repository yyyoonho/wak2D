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

    //박스안의 콜라이더들에게 데미지를 주는 근접공격 함수
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

    //2페이즈의 충격파를 생성하는 함수
    private void ShockWave()
    {
        GameObject g = EffectPool.instance.GetObject("ShockWave");
        g.transform.position = shockWavePoint.position;
        if(_player.transform.position.x >= transform.position.x)
        {
            g.transform.localScale = new Vector3(-(g.transform.localScale.x), g.transform.localScale.y, g.transform.localScale.z);
        }
    }

    //3페이즈의 전기 구체를 생성하는 함수.
    private void ThunderBall()
    {
        GameObject g = EffectPool.instance.GetObject("ThunderBall");
        g.transform.position = thunderBallPoint.position;
        if(thunderBall == null)
        {
            thunderBall = g;
        }
    }

    //3페이즈의 전기 구체를 던지는 함수.
    private void ThrowThunderBall()
    {
        if (thunderBall != null)
        {
            thunderBall.GetComponent<ThunderBall>().Shot(_player.transform);
            thunderBall = null;
        }
    }

    //지정한 위치들 중 하나로 텔레포트 시키는 함수.
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
