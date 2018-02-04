using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    bool isGamePlay = true;
    [SerializeField]
    Ini iniScript;
    [SerializeField]
    bool isNetWork;

    void Start()
    {
        if(!isNetWork)
        {
            iniScript.IniStart();
        }
    }

    public bool GetIsNetWork()
    {
        return isNetWork;
    }

    public void SetIsGamePlay(bool set)
    {
        isGamePlay = set;
    }

    public bool GetIsGamePlay()
    {
        return isGamePlay;
    }

    public void Ini()
    {
        iniScript.IniStart();
    }
}
