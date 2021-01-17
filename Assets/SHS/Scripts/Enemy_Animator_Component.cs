using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animator_Component : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Trigger_Animator_Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void Set_Animator_Damaged(bool boolean)
    {
        _animator.SetBool("isDamaged", boolean);
    }
}
