using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float MAX_Y;
    public ProjectileKind rangedAttackKind;
    public GameObject spriteComp;
    public enum ProjectileKind
    {
        Parabola,
        Bullet
    }

    private float damage;
    private GameObject target;
    private Rigidbody2D rb2d;

    public void Init(GameObject _target, float _damage)
    {
        target = _target;
        damage = _damage;
    }

    // Start is called before the first frame update
    void Start()
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
        float vx =  -(transform.position.x - target.transform.position.x) / dat;

        rb2d.velocity = new Vector2(vx, vy);
    }

    // Update is called once per frame
    void Update()
    {
        spriteComp.transform.Rotate(new Vector3(0f, 0f, 1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<LivingEntity>().GetDamaged(damage);
            Destroy(gameObject);
        }
    }
}
