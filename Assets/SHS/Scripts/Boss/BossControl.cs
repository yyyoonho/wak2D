using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : LivingEntity
{
    public List<float> PatternHPList;
    public List<int> timeBetPattern;
    public float teleportGap;
    public Transform groundCheck;

    public int patternIdx = 0;
    private bool isLookingRight = false;
    private GameObject _player;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= PatternHPList[patternIdx])
        {
            Debug.Log(PatternHPList[patternIdx]);
            patternIdx++;
            _animator.SetTrigger("TriggerPattern");
        }
    }

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

    private void SwapSide()
    {
        isLookingRight = !isLookingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
