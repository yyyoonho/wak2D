﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sprite_Component : MonoBehaviour
{
    public Transform lookingPoint;

    private SpriteRenderer sprite;
    private bool lookingRight;
    private float blinkTimer = 0f;

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

    public void KnockBackSprite(float knockbackTime, Transform targetTransform)
    {
        blinkTimer = 0f;
        LookAtTarget(targetTransform);
        StartCoroutine(SpriteBlink(knockbackTime));
    }

    private IEnumerator SpriteBlink(float knockbackTime)
    {
        while(blinkTimer <= knockbackTime)
        {
            yield return new WaitForSeconds(0.1f);
            blinkTimer += 0.1f;
            sprite.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);
            blinkTimer += 0.1f;
            sprite.color = new Color(1, 1, 1, 1);
        }
    }

    void Start()
    {
        if (lookingPoint.transform.position.x > transform.position.x) lookingRight = true;
        else lookingRight = false;

        sprite = GetComponent<SpriteRenderer>();
    }
}
