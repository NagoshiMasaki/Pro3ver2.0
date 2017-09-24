using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    SituationManager situationManagerScript;
    [SerializeField]
    DeckManager deckManagerScript;
    
    public bool GetIsGamePlay()
    {
        return gameMasterScript.GetIsGamePlay();
    }

    public int GetPlayerTurn()
    {
       return situationManagerScript.GetPlayerTurn();
    }

    public void SetPhase(SituationManager.Phase set)
    {
        situationManagerScript.SetPhase(set);
    }
    public SituationManager.Phase GetPhase()
    {
        return situationManagerScript.GetStatus();
    }

    public GameObject GetDraw(int number)
    {
        GameObject drawobj = deckManagerScript.GetDrawObj(number);
        return drawobj;
    }
}
