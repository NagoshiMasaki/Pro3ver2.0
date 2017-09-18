using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    [SerializeField]
    BoardStatus boardStatusScript;
    bool Ini = true;
    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    GameObject mathObject;
    [SerializeField]
    int PlayerTurn;

    public void IniInstanceSet()
    {
        
    }

    
    public bool GetIsGamePlay()
    {
        return gameMasterScript.GetIsGamePlay();
    }

    public GameObject GetInstanceMathObj()
    {
        return mathObject;
    }
}
