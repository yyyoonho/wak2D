using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;

    [SerializeField] GameObject groundCheck;
    Rigidbody2D rigid;
    Animator playerAnim;

    bool isGround;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 점프입력을 받는다.
        if (Input.GetButtonDown("Jump"))
        {
            //땅을 밟고있다 -> True, 공중에 있다 -> False.
            isGround = groundCheck.GetComponent<GroundCheck>().getIsGround();
            if(isGround)
                Jump();
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        float xMove = Input.GetAxis("Horizontal");
        playerAnim.SetFloat("Horizontal", xMove);
        if (xMove<0)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xMove > 0)
        { 
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    private void Jump()
    {
        isGround = false;
        playerAnim.SetTrigger("Jump");

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
     }
}
