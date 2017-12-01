using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{
    [SerializeField]
    SPAPManager spapManagerScript;
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
    [SerializeField]
    GameObject P1Obj;
    [SerializeField]
    GameObject P2Obj;

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
        spapManagerScript.IniInstanceSp(playerTurn);
        TurnChange();
    }
    public bool GetIsGamePlay()
    {
        return gameMasterScript.GetIsGamePlay();
    }

    public void SetPhase(Phase set)
    {
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
        if (moveCount == 0)
        {
            Debug.Log("main2");
            status = Phase.Main2;
            UpdatePhase();
        }
    }

    void TurnChange()
    {
        skillManagerScript.TurnEndSkillList();
        switch (playerTurn)
        {
            case 1:
                SetPlayerTurn(2);
                spapManagerScript.AddSP(2);
                DrawAction(2);
                P1Obj.SetActive(false);
                P2Obj.SetActive(true);
                break;
            case 2:
                SetPlayerTurn(1);
                spapManagerScript.AddSP(1);
                DrawAction(1);
                P1Obj.SetActive(true);
                P2Obj.SetActive(false);
                break;
        }
        boardManagerScript.SummonCharacterListColorClear();
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
        uiManegerScript.UpdatePhase(status, playerTurn);
    }

    void DrawAction(int playernum)
    {
        GameObject drawobj = deckManagerScript.GetDrawObj(playernum);
        deckHandManagerScript.InstanceDrawCard(playernum, drawobj);
        status = Phase.Main1;
        UpdatePhase();
    }

    public SkillManager GetSkillManager()
    {
        return skillManagerScript;
    }
}
