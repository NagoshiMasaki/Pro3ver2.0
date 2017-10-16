using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{


    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    SituationManager situationManagerScript;
    [SerializeField]
    DeckManager deckManagerScript;
    [SerializeField]
    DeckHandManager deckHandManagerScript;
    [SerializeField]
    SPAPManager spapManagerScript;
    [SerializeField]
    BoardManager boardManagerScript;
    [SerializeField]
    DictionaryManager dictionaryManagerScript;
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

    public void InstanceDrawCard(int number, GameObject drawobj)
    {
        deckHandManagerScript.InstanceDrawCard(number, drawobj);
    }

    public int GetSP(int num)
    {
        return spapManagerScript.GetSP(num);
    }

    public List<GameObject> GetInstancePos(int playernum)
    {
        return boardManagerScript.GetInstancePos(playernum);
    }

    public GameObject GetIllustObj(int dictionarynum)
    {
        return dictionaryManagerScript.GetIllustCharacter(dictionarynum);
    }

    public void ReMoveIllustCard(int playernum, GameObject target)
    {
        deckHandManagerScript.RemoveIllustCard(playernum, target);
    }

    public GameObject GetSummonObj(int num)
    {
        return dictionaryManagerScript.GetSummonCharacter(num);
    }

    public void InstanceMovePos(MoveData.Rate rate, int playernum, int nowlengthmass, int nowsidemass)
    {
        boardManagerScript.InstanceMoveData(rate, playernum, nowlengthmass, nowsidemass);
    }
}
