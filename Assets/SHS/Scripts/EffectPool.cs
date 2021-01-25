using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public static EffectPool instance;
    public GameObject shockWavePrefab;
    public GameObject thunderBallPrefab;
    public Transform objPool;

    private GameObject[] shockWaveList;
    private GameObject[] thunderBallList;
    private GameObject[] target;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        shockWaveList = new GameObject[10];
        thunderBallList = new GameObject[10];

        for (int i = 0; i < shockWaveList.Length; i++)
        {
            shockWaveList[i] = Instantiate(shockWavePrefab);
            shockWaveList[i].transform.parent = objPool;
            shockWaveList[i].SetActive(false);
        }

        for (int i = 0; i < thunderBallList.Length; i++)
        {
            thunderBallList[i] = Instantiate(thunderBallPrefab);
            thunderBallList[i].transform.parent = objPool;
            thunderBallList[i].SetActive(false);
        }
    }

    public GameObject GetObject(string s)
    {
        switch(s)
        {
            case "ShockWave":
                target = shockWaveList;
                break;
            case "ThunderBall":
                target = thunderBallList;
                break;
        }

        for (int idx = 0; idx < target.Length; idx++)
        {
            if (!target[idx].activeSelf)
            {
                target[idx].SetActive(true);
                return target[idx];
            }
        }

        return null;
    }
}
