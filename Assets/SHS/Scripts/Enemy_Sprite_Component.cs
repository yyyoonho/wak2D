using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sprite_Component : MonoBehaviour
{
    public Transform lookingPoint;

    private SpriteRenderer sprite;
    private Enemy_Animator_Component enemy_Animator_Component;
    public bool lookingRight;
    private float blinkTimer = 0f;

    //적의 바라보는 방향을 바꾸는 함수
    public void SwapSide()
    {
        lookingRight = !lookingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    //매개변수로 들어온 목표를 바라보는 함수
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

    //공격받았을 때 넉백시키는 함수.
    public void KnockBackSprite(float knockbackTime, Transform targetTransform)
    {
        blinkTimer = 0f;
        LookAtTarget(targetTransform);
        enemy_Animator_Component.Set_Animator_Damaged(true);
        StartCoroutine(SpriteBlink(knockbackTime));
    }

    //공격받았을 때, 스프라이트를 깜빡거리는 코루틴
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
        enemy_Animator_Component.Set_Animator_Damaged(false);
    }

    void Start()
    {
        if (lookingPoint.transform.position.x > transform.position.x) lookingRight = true;
        else lookingRight = false;

        sprite = GetComponent<SpriteRenderer>();
        enemy_Animator_Component = GetComponent<Enemy_Animator_Component>();
    }
}
