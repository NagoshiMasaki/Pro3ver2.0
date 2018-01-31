using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    bool isGamePlay = true;
    [SerializeField]
    Ini iniScript;
    void Start()
    {
        iniScript.IniStart();
    }

    public void SetIsGamePlay(bool set)
    {
        isGamePlay = set;
    }

    public bool GetIsGamePlay()
    {
        return isGamePlay;
    }
}
