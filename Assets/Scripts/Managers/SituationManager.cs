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
    [SerializeField]
    AttachCard attachCardScript;
    [SerializeField]
    float timer;
    [SerializeField]
    BgmSeManager bgmSeManagerScript;
    float copyTimer;
    public enum Phase
    {
        None,
        Draw,
        Summon,
        Battle,
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
    [SerializeField]
    PlayerManager playerManagerScript;
    static int turnMasterNumber;
    void Update()
    {
        timer -= Time.deltaTime;
    }

    public static int TurnMasterNumber { get { return turnMasterNumber; } set { turnMasterNumber = value; } }

    public void Ini()
    {
        copyTimer = timer;
        copyMoveCount = moveCount;
        spapManagerScript.IniInstanceSp(playerTurn);
        enabled = true;
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
    }

    public void ChecMoveCount()
    {
        if (moveCount <= 0)
        {
            moveCount = copyMoveCount;
            status = Phase.End;
            UpdatePhase();
        }
    }

    public void TurnChange()
    {
        if(gameMasterScript.GetIsNetWork())
        {
            NetWorkTurnChange();
        }
        else
        {
            switch (playerTurn)
            {
                case 1:
                    SetPlayerTurn(2);
                    spapManagerScript.AddSP(2);
                    DrawAction(2);
                    P1Obj.SetActive(false);
                    P2Obj.SetActive(true);
                    deckHandManagerScript.AllChangeCard(1,false);
                    deckHandManagerScript.AllChangeCard(2, true);
                    break;
                case 2:
                    SetPlayerTurn(1);
                    spapManagerScript.AddSP(1);
                    DrawAction(1);
                    P1Obj.SetActive(true);
                    P2Obj.SetActive(false);
                    deckHandManagerScript.AllChangeCard(2, false);
                    deckHandManagerScript.AllChangeCard(1, true);
                    break;
            }
        }
        skillManagerScript.TurnEndSkillList();
        playerManagerScript.PlayerAllAttachNull();
        attachCardScript.SetSprite(null);
        uiManegerScript.Reset();
        boardManagerScript.SummonCharacterListColorClear();
        boardManagerScript.ClearMoveDataList();
        moveCount = copyMoveCount;
        status = Phase.Summon;
        UpdatePhase();
    }


    void NetWorkTurnChange()
    {
        if (turnMasterNumber == 1)
        {
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
        }
        else
        {
            switch (playerTurn)
            {
                case 1:
                    SetPlayerTurn(1);
                    spapManagerScript.AddSP(1);
                    DrawAction(1);
                    P1Obj.SetActive(false);
                    P2Obj.SetActive(true);
                    break;
                case 2:
                    SetPlayerTurn(2);
                    spapManagerScript.AddSP(2);
                    DrawAction(2);
                    P1Obj.SetActive(true);
                    P2Obj.SetActive(false);
                    break;
            }
        }

    }

    public void NextPhase()
    {
        if (timer > 0.0f)
        {
            return;
        }
        status++;
        timer = copyTimer;
        if (status >= Phase.End)
        {
            TurnChange();
        }
        else
        {
            UpdatePhase();
        }
    }

    public void UpdatePhase()
    {
        if (status == Phase.End)
        {
            TurnChange();
        }
        int bgmnumber = 0;
        bgmSeManagerScript.BgmStop();
        switch (status)
        {
            case Phase.Summon:
                bgmnumber = 2;
                bgmSeManagerScript.SePlay(1);

                break;
            case Phase.Battle:
                bgmnumber = 3;
                bgmSeManagerScript.SePlay(2);
                break;
        }
        uiManegerScript.UpdatePhase(status, playerTurn, bgmnumber);
    }

    void DrawAction(int playernum)
    {
        GameObject drawobj = deckManagerScript.GetDrawObj(playernum);
        deckHandManagerScript.InstanceDrawCard(playernum, drawobj);
        status = Phase.Summon;
        UpdatePhase();
    }

    public SkillManager GetSkillManager()
    {
        return skillManagerScript;
    }
}
