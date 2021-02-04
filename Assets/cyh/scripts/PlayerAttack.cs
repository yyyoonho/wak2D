
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    //==참조를 위한 컴포넌트==
    Animator playerAnim;
    PlayerMove playerMove;

    //==Inspector에서 조정하기 위한 속성==
    public float attackTerm;
    public Transform pos;
    public Vector2 boxSize;

    //==내부에서 다루는 변수==
    private float inClassAttackTerm;

    //==외부파라미터==

    //애니메이션 해시이름
    public readonly static int ANISTS_Idle =
        Animator.StringToHash("Base Layer.playerIdle");
    public readonly static int ANISTS_Run =
        Animator.StringToHash("Base Layer.playerRun");
    public readonly static int ANISTS_Dash =
        Animator.StringToHash("Base Layer.playerDash");
    public readonly static int ANISTS_Jump =
        Animator.StringToHash("Base Layer.playerJump");
    public readonly static int ANISTS_DoubleJump =
        Animator.StringToHash("Base Layer.playerDoubleJump");
    public readonly static int ANISTS_Attack_A =
        Animator.StringToHash("Base Layer.playerAttack1");
    public readonly static int ANISTS_Attack_B =
        Animator.StringToHash("Base Layer.playerAttack2");
    public readonly static int ANISTS_Attack_C =
        Animator.StringToHash("Base Layer.playerAttack3");

    /*-----------------------------------------------------------------------------------*/


    //==컴포넌트 실행 시작==
    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
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
                    //공격처리결과를 넣는다. Ex) Enemy를 참조해서 데미지 입력.
                }
                ActionAttack();
                inClassAttackTerm = attackTerm;
            }
        }
        else
        {
            inClassAttackTerm -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        //현재 스테이트 가져오기.
        AnimatorStateInfo stateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);

        //공격 중인지 확인.
        if(stateInfo.fullPathHash == ANISTS_Attack_A ||
            stateInfo.fullPathHash == ANISTS_Attack_B ||
            stateInfo.fullPathHash == ANISTS_Attack_C)
        {
            //이동 정지
            playerMove.SetSpeed(0);
        }
    }

    public void ActionAttack()
    {
        AnimatorStateInfo stateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.fullPathHash != ANISTS_Attack_A)
        {
            playerAnim.SetTrigger("PlayerAttack1");
        }
        else
        {
            Debug.Log("ㅎㅇ");
            playerAnim.SetTrigger("PlayerAttack2");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
