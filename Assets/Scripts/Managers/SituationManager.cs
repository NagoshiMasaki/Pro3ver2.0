using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{

    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    public enum Phase
    {
        None,
        Draw,
        Main1,
        Move,
        Main2,
        End,
    }
    [SerializeField]
    Phase status = Phase.Draw;
    [SerializeField]
    int playerTurn;

    void Start()
    {
        status = Phase.Draw;//デバック用
    }

    public bool GetIsGamePlay()
    {
        return gameMasterScript.GetIsGamePlay();
    }

    public void SetPhase(Phase set)
    {
        switch (set)
        {
            case Phase.Main1:
                break;
            case Phase.Main2:
                break;
            case Phase.Move:
                break;
            case Phase.End:
                break;
        }
        status = set;
    }

    public Phase GetStatus()
    {
        return status;
    }

    public void SetPlayerTurn(int set)
    {
        playerTurn = set;
    }

    public int GetPlayerTurn()
    {
        return playerTurn;
    }
}
