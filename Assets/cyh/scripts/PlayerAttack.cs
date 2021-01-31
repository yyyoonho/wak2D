
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //참조를 위한 컴포넌트
    Animator playerAnim;

    //Inspector에서 조정하기 위한 속성
    public float attackTerm;
    public Transform pos;
    public Vector2 boxSize;

    //내부에서 다루는 변수
    private float inClassAttackTerm;

    /*-----------------------------------------------------------------------------------*/


    //컴포넌트 실행 시작
    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(inClassAttackTerm<=0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                for(int i=0; i<collider2Ds.Length; i++)
                {
                    Debug.Log(collider2Ds[i].tag);
                }

                playerAnim.SetTrigger("playerAttack1");
                inClassAttackTerm = attackTerm;
            }
        }
        else
        {
            inClassAttackTerm -= Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
