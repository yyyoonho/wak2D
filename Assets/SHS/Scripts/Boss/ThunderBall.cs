using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBall : MonoBehaviour
{
    public float speed;

    private Vector3 dir;
    private bool isMoving = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Shot(Transform target)
    {
        dir = target.position - transform.position;
        dir = dir.normalized;

        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            transform.position += speed * dir * Time.deltaTime;
        }
    }

    private void Explode()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isMoving)
        {
            if(collision.CompareTag("Player"))
            {
                animator.SetTrigger("Explode");
                dir = Vector3.zero;
                isMoving = false;
            }

            if(collision.CompareTag("Ground"))
            {
                animator.SetTrigger("Explode");
                dir = Vector3.zero;
                isMoving = false;
            }
        }
    }

    private void OnDisable()
    {
        isMoving = false;
        dir = Vector3.zero;
        animator.Rebind();
    }
}
