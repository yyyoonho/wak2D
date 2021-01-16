using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool isGround;
    public Animator playerAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGround = true; //땅을 밟고 있으면 true를 리턴.
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGround = false; //공중에 있으면 false를 리턴.
    }
    public bool getIsGround()
    {
        return isGround;
    }

}
