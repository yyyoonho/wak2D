using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol_Component : MonoBehaviour
{
    public float move_Speed;
    public Transform DetectPoint;

    private Rigidbody2D rb2d;
    private Enemy_Sprite_Component enemy_Sprite_Component;
    private bool lookingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemy_Sprite_Component = GetComponent<Enemy_Sprite_Component>();
        if (DetectPoint.position.x > transform.position.x)
        {
            lookingRight = true;
        }
        else
        {
            lookingRight = false;
        }
    }

    public void EnterPatrol()
    {
        if (DetectPoint.position.x > transform.position.x)
        {
            lookingRight = true;
        }
        else
        {
            lookingRight = false;
        }
    }

    public void StopMoving()
    {
        rb2d.velocity = Vector2.zero;
    }

    // Update is called once per frame
    public void Patrol_Update()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D ray = Physics2D.Raycast(DetectPoint.position, Vector2.down, 1f, layerMask);
        if(ray)
        {
            Patrol();
        }
        else
        {
            SwapSide();
        }
    }

    public void KnockBack(Transform target)
    {
        if(target.position.x < transform.position.x)
        {
            transform.position += Vector3.right * 0.7f;
        }
        else
        {
            transform.position += Vector3.left * 0.7f;
        }
    }

    private void SwapSide()
    {
        lookingRight = !lookingRight;
        enemy_Sprite_Component.SwapSide();
    }

    private void Patrol()
    {
        if(lookingRight)
        {
            rb2d.velocity = new Vector2(move_Speed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(-move_Speed, rb2d.velocity.y);
        }
    }
}
