using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sprite_Component : MonoBehaviour
{
    public Transform lookingPoint;

    private bool lookingRight;

    public void SwapSide()
    {
        lookingRight = !lookingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void LookAtTarget(Transform _target)
    {
        if(transform.position.x < _target.position.x)
        {
            if(!lookingRight)
            {
                SwapSide();
            }
        }
        else
        {
            if(lookingRight)
            {
                SwapSide();
            }
        }
    }

    void Start()
    {
        if (lookingPoint.transform.position.x > transform.position.x) lookingRight = true;
        else lookingRight = false;
    }
}
