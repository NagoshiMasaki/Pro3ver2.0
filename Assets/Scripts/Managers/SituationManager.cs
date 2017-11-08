using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{

    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    BoardManager boardManagerScript;
    [SerializeField]
    UImanager uiManegerScript;
    [SerializeField]
    DeckManager deckManagerScript;
    [SerializeField]
    DeckHandManager deckHandManagerScript;
    [SerializeField]
    SkillManager skillManagerScript;
    public enum Phase
    {
        None,
        Draw,
        Main1,
        Move,
        Main2,
        End,
    }
    public enum SkillStatus
    {

    }
    [SerializeField]
    int moveCount;
    [SerializeField]
    Phase status = Phase.Draw;
    [SerializeField]
    int playerTurn;
    int copyMoveCount;
    public void Ini()
    {
        copyMoveCount = moveCount;
        DrawAction(playerTurn);
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
        UpdatePhase();
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
            TurnChange();
        }
    }

    void TurnChange()
    {
        switch (playerTurn)
        {
            case 1:
                SetPlayerTurn(2);
                DrawAction(2);
                break;
            case 2:
                SetPlayerTurn(1);
                DrawAction(1);
                break;
        }
        boardManagerScript.ClearMoveDataList();
        moveCount = copyMoveCount;
        status = Phase.Main1;
        UpdatePhase();
    }

    public void NextPhase()
    {
        status++;
        if (status == Phase.End)
        {
            TurnChange();
        }
        else {
            UpdatePhase();
        }
    }

    public void UpdatePhase()
    {
        uiManegerScript.UpdatePhase(status,playerTurn);
    }

    void DrawAction(int playernum)
    {
        GameObject drawobj = deckManagerScript.GetDrawObj(playernum);
        deckHandManagerScript.InstanceDrawCard(playernum,drawobj);
        status = Phase.Main1;
        UpdatePhase();
    }

    public SkillManager GetSkillManager()
    {
        return skillManagerScript;
    }
}
