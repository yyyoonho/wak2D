using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : LivingEntity
{
    public List<float> PatternHPList;
    public float teleportGap;
    public Transform groundCheck;

    public int patternIdx = 0;
    public bool isLookingRight = false;
    private bool isActing = false;
    private GameObject _player;
    private Animator _animator;

    void Start()
    {
        base.Start();
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(patternIdx < PatternHPList.Count && HP <= PatternHPList[patternIdx])
        {
            patternIdx++;
            _animator.SetTrigger("TriggerPattern");
        }
    }

    //플레이어의 좌 또는 우로 이동시키는 함수
    public void TelePortToPlayer()
    {
        //0 = 왼쪽, 1= 오른쪽
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            transform.position = _player.transform.position + new Vector3(-teleportGap, 0f, 0f);
            if (!isLookingRight) SwapSide();
        }
        else
        {
            transform.position = _player.transform.position + new Vector3(teleportGap, 0f, 0f);
            if (isLookingRight) SwapSide();
        }
    }

    //플레이어를 바라보게 하는 함수
    private void LookAtPlayer()
    {
        if (_player.transform.position.x >= transform.position.x)
        {
            if(!isLookingRight)
            {
                SwapSide();
            }
        }
        else
        {
            if(isLookingRight)
            {
                SwapSide();
            }
        }
    }

    //바라보는 방향을 바꾸는 함수
    private void SwapSide()
    {
        isLookingRight = !isLookingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
