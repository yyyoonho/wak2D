using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float MAX_Y;
    public float bulletSpeed;
    public ProjectileKind rangedAttackKind;
    public enum ProjectileKind
    {
        Parabola,
        Bullet
    }

    private float damage;
    private Vector3 dir;
    private GameObject target;
    private Rigidbody2D rb2d;
    private Animator _animator;

    public void Init(GameObject _target, float _damage)
    {
        target = _target;
        damage = _damage;
        Destroy(this.gameObject, 5f);// 5초후 삭제 예약
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        dir = (target.transform.position - transform.position).normalized;
        MAX_Y = transform.position.y + MAX_Y;

        if (rangedAttackKind == ProjectileKind.Parabola) ParabolaSetting();
        else
        {
            LookAtDir();
            rb2d.isKinematic = true;
        }
    }

    private void FixedUpdate()
    {
        if(rangedAttackKind == ProjectileKind.Bullet)
        {
            transform.position += dir * bulletSpeed * Time.deltaTime;
        }
        else if(rangedAttackKind == ProjectileKind.Parabola)
        {
            float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x);
            transform.localEulerAngles = new Vector3(0, 0, (angle * 180) / Mathf.PI);
        }
    }

    //날아가는 방향을 향해 회전시키는 함수.
    private void LookAtDir()
    {
        Vector2 direction = target.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    }

    //포물선으로 던질 때, 원하는 지점에 가기위한 velocity를 설정하는 함수.
    private void ParabolaSetting()
    {
        rb2d = GetComponent<Rigidbody2D>();
        MAX_Y = transform.position.y + MAX_Y;

        float dh = target.transform.position.y - transform.position.y;
        float mh = MAX_Y - transform.position.y;

        float g = 9.81f;

        float vy = Mathf.Sqrt(2 * g * mh);

        float b = -2 * vy;
        float c = 2 * dh;

        float dat = (-b + Mathf.Sqrt(b * b - 4 * g * c)) / (2 * g);
        float vx = -(transform.position.x - target.transform.position.x) / dat;

        rb2d.velocity = new Vector2(vx, vy);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _animator.SetTrigger("Explode");
            collision.GetComponent<LivingEntity>().GetDamaged(damage);
            Destroy(gameObject,3f);
            this.enabled = false;
        }

        if (collision.CompareTag("Ground"))
        {
            _animator.SetTrigger("Explode");
            Destroy(gameObject,3f);
            this.enabled = false;
        }
    }
}
