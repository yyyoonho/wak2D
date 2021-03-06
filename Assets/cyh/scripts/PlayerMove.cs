﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //참조를 위한 컴포넌트
    GameObject groundCheck;
    Rigidbody2D rigid;

    //Inspector에서 조정하기 위한 속성
    public float speed = 12.0f;         //플레이어 캐릭터의 속도
    public float jumpPower = 500.0f;    //플래이어 캐릭터를 점프시켰을 때의 파워

    //내부에서 다루는 변수
    bool grounded;                      //접지 체크
    float xMove;                        //x축 기준 이동방향

    /*-----------------------------------------------------------------------------------*/

    //컴포넌트 실행 시작
    private void Start()
    {
        groundCheck = transform.Find("GroundCheck").gameObject;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        grounded = false;
    }
    
    //Update 함수
    private void Update()
    {
        //땅을 밟고 있는지 체크. (땅을 밟고 있다 -> true/ 밟고 있지 않다 -> false)
        grounded = groundCheck.GetComponent<GroundCheck>().getIsGround();

        //땅을 밟은 상태에서 점프키 입력 시 점프함수 실행.
        if(grounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //이동 키를 입력.
        xMove = Input.GetAxis("Horizontal");

    }

    //FixedUpdate 함수
    private void FixedUpdate()
    {
        if (xMove != 0)
        {
            Move();
        }
    }

    //점프 함수
    private void Jump()
    {
        rigid.AddForce(new Vector2(0.0f, jumpPower));
    }


    //움직임 함수
    private void Move()
    {
        if(xMove>0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rigid.velocity = new Vector2(xMove * speed, rigid.velocity.y);
        }
        else if(xMove<0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            rigid.velocity = new Vector2(xMove * speed, rigid.velocity.y);
        }
    }
}