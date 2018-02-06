using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandManager : MonoBehaviour
{
    [SerializeField]
    DeckHand decxHand1Script;
    [SerializeField]
    DeckHand decxHand2Script;
    [SerializeField]
    Sprite backillustlation;
    [SerializeField]
    AnimationManager animationManagerScript;
    [SerializeField]
    UImanager uiManagerScript;
    [SerializeField]
    SpriteManager spriteManagerScript;
    [SerializeField]
    SituationManager situaionManagerScript;
    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    SocketManager socketManagerScript;

    public void AddSocketStataus()
    {
        socketManagerScript.AddStaus(SocketAction.GameStatus.DrawCardID);
    }

    public bool GetIsNetWork()
    {
        return gameMasterScript.GetIsNetWork();
    }

    public int GetNetWorkPlayerNumber()
    {
        return gameMasterScript.NetWorkPlayerNumber;
    }

    public Sprite GetNumber(int number)
    {
        return spriteManagerScript.GetNumberList(number);
    }
    public void GameFinish(int num)
    {
        uiManagerScript.GameFinish(num);
    }

    public void DrawcardAnimation(GameObject drawobj,Vector3 target,GameObject deckobj,DeckHand deckhand)
    {
        animationManagerScript.DrawCardAnimation(drawobj,target,deckobj,deckhand);
    }

    public Sprite BackIllustlation()
    {
        return backillustlation;
    }

    public Vector3 GetInstancePos(int num)
    {
        switch (num)
        {
            case 1:
                return decxHand1Script.GetPos();
            case 2:
                return decxHand2Script.GetPos();
        }
        return Vector3.zero;
    }

    public void DeckHandIni()
    {
        decxHand1Script.Ini();
        decxHand2Script.Ini();
    }


    public void InstanceDrawCard(int num,GameObject drawobj)
    {
        switch (num)
        {
            case 1:
                decxHand1Script.SetDrawObj(drawobj);
                break;
            case 2:
                decxHand2Script.SetDrawObj(drawobj);
                break;
        }
    }

    public void RemoveIllustCard(int num,GameObject target)
    {
        switch (num)
        {
            case 1:
                decxHand1Script.RemoveIllustCard(target);
                break;
            case 2:
                decxHand2Script.RemoveIllustCard(target);
                break;
        }
    }

    public int GetPlayerTurn()
    {
        return situaionManagerScript.GetPlayerTurn();
    }

    public void AllChangeCard(int playernum, bool set)
    {
        switch (playernum)
        {
            case 1:
                decxHand1Script.AllChangeCard(set);
                break;
            case 2:
                decxHand2Script.AllChangeCard(set);
                break;
        }

    }
}
