using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    SituationManager situationManagerScript;

    public bool GetIsGamePlay()
    {
        return gameMasterScript.GetIsGamePlay();
    }

    public int GetPlayerTurn()
    {
       return situationManagerScript.GetPlayerTurn();
    }

}
