using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public float startHP;
    protected float HP;
    protected bool isDead = false;

    protected virtual void Start()
    {
        HP = startHP;
    }

    public void GetDamaged(float damgage)
    {
        HP -= damgage;
        if (HP <= 0 && !isDead)
            Die();
    }

    protected void Die()
    {
        isDead = true;
        GameObject.Destroy(this.gameObject);
    }
}
