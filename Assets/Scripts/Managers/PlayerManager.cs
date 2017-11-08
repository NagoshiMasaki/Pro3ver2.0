//////////////////////////////////
//制作者　名越大樹
//クラス　プレイヤーを管理するクラス
//////////////////////////////////

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
    [SerializeField]
    BattleManager battleManagerScript;
    [SerializeField]
    SkillManager skillManagerScript;
    [SerializeField]
    PlayerAction playerActionScript;
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

    public List<GameObject> GetInstancePos(int playernum, IllustrationStatus status)
    {
        return boardManagerScript.GetInstancePos(playernum,status);
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

    public void DecrementMoveCount()
    {
        situationManagerScript.DecrementMoveCount();
    }

    public void AddMoveList(GameObject target)
    {
        boardManagerScript.AddMoveList(target);
    }

    public bool CheckMoveList(GameObject target)
    {
       return boardManagerScript.CheckMoveList(target);
    }

    public BattleStatus.ResultStatus Battle(SummonStatus player,SummonStatus enemy)
    {
       return battleManagerScript.Battle(player,enemy);
    }

    public void ClearUpdateMoveList()
    {
        boardManagerScript.ClearUpdateMoveList();
    }

    public void NextPhase()
    {
        situationManagerScript.NextPhase();
    }

    public void GameFinish(int playernum)
    {
        battleManagerScript.GameFinish(playernum);
        gameMasterScript.SetIsGamePlay(false);
    }

    public SkillManager GetSkillManager()
    {
        return skillManagerScript;
    }

    /// <summary>
    /// バトル開始のスキル発動
    /// </summary>
    /// <param name="player"></param>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public SkillStatus.Status BattleStart(GameObject player,GameObject enemy)
    {
       return skillManagerScript.BattleStart(player,enemy);
    }

    public void ActiveSkill(GameObject player)
    {
       
    }

    public void SetAttachStatus(PlayerAction.AttachStatus set)
    {
        playerActionScript.SetAttachStatus(set);
    }

    public void AttachSkillTarget(SummonStatus target)
    {
        skillManagerScript.AttachSkillTarget(target);
    }
}
