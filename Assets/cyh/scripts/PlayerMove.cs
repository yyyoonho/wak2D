using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;

    Rigidbody2D rigid;
    GameObject groundCheck;
    Animator playerAnim;

    Vector3 movement;
    bool isJumping = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

    }
    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        float xMove = Input.GetAxis("Horizontal");
        Debug.Log(xMove);
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
        if (!isJumping)
            return;

        rigid.velocity = Vector2.zero;

        playerAnim.SetTrigger("Jump");

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
}
