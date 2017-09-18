using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{

    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    public enum Status
    {
        None,
        Main1,
        Move,
        Main2,
        End,
    }

    Status status = Status.None;
    [SerializeField]
    int playerTurn;

    public bool GetIsGamePlay()
    {
        return gameMasterScript.GetIsGamePlay();
    }

    public void SetStatus(Status set)
    {
        switch (set)
        {
            case Status.Main1:
                break;
            case Status.Main2:
                break;
            case Status.Move:
                break;
            case Status.End:
                break;
        }
        status = set;
    }

    public Status GetStatus()
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
