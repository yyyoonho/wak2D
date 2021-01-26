using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    private void TurnOff()
    {
        this.gameObject.SetActive(false);
        this.transform.localScale = new Vector3(1, 1, 1);
    }
}
