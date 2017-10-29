using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{

    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    BoardManager boardManagerScript;
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
    int moveCount;
    [SerializeField]
    Phase status = Phase.Draw;
    [SerializeField]
    int playerTurn;
    int copyMoveCount;
    void Start()
    {
        status = Phase.Draw;//デバック用
        copyMoveCount = moveCount;
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
        boardManagerScript.ClearMoveDataList();
        playerTurn = set;
    }

    public int GetPlayerTurn()
    {
        return playerTurn;
    }

    public void DecrementMoveCount()
    {
        moveCount--;
        if(moveCount == 0)
        {
            switch (playerTurn)
            {
                case 1:
                    SetPlayerTurn(2);
                    break;
                case 2:
                    SetPlayerTurn(1);
                    break;
            }
            boardManagerScript.ClearMoveDataList();
            moveCount = copyMoveCount;
            status = Phase.Draw;
        }
    }
}
