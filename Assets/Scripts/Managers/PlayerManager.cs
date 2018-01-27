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
    [SerializeField]
    PlayerStatus playerStatusScript;
    [SerializeField]
    UImanager uiManagerScript;
    [SerializeField]
    AttachCard attachCardScript;
    [SerializeField]
    AnimationManager animationManagerScript;
    [SerializeField]
    BgmSeManager bgmSeManagerScript;

    public void SetCardAnimation(SummonStatus preemptioncard,SummonStatus latecard)
    {
        animationManagerScript.SetAnimationCard(preemptioncard,latecard);
    }

    public void ResultBattleAction(GameObject player, GameObject enemy, BattleStatus.ResultStatus result, GameObject attachmass, MassStatus attachmassstatus, GameObject playercharacter, GameObject enemycharacter)
    {
       playerActionScript.BattleResult( player,  enemy, result,  attachmass,  attachmassstatus,  playercharacter,  enemycharacter);
    }
    public void SePlay(int number)
    {
        bgmSeManagerScript.SePlay(number);
    }

    public void SummonAnimation(SummonStatus summon, MassStatus mass, IllustrationStatus illust, int playernumber)
    {
        animationManagerScript.SummonAnimation(summon,mass,illust,playernumber);
    }

    public void SetSprite(Sprite set)
    {
        attachCardScript.SetSprite(set);
    }

    public void MoveAnimation(GameObject target, Vector3 targetPoint)
    {
        animationManagerScript.MoveAnimation(target, targetPoint);
    }

    public void ResetIsAnimation(int player, int usecount)
    {
        spapManagerScript.ResetisAnimation(player,usecount);
    }

    public void SetisAnimation(int player,int usecount)
    {
        spapManagerScript.SetisAnimation(player,usecount);
    }
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

    public void Battle(SummonStatus player,SummonStatus enemy, GameObject attachmass, MassStatus attachmassstatus, GameObject playercharacter, GameObject enemycharacter)
    {
       battleManagerScript.Battle(player,enemy,attachmass,attachmassstatus,playercharacter,enemycharacter);
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

    public SkillStatus.Status BattaleEnd(GameObject player, GameObject enemy)
    {
        return skillManagerScript.BattleEnd(player,enemy);
    }

    public SkillStatus.Status BattaleEnd(GameObject wincharacter)
    {
        return skillManagerScript.BattleEnd(wincharacter);
    }
    public void AddSummonCharacter(SummonStatus set)
    {
        boardManagerScript.AddSummonCharacter(set);
    }
    public void MoveEndSkill(GameObject player)
    {
        skillManagerScript.MoveEndSkill(player);
    }

    
    public void ActiveSkill(GameObject player)
    {
       
    }
    public void SetAP(int player,int set)
    {
        spapManagerScript.SetAP(player,set);
    }

    public void SetSP(int playernum,int set,int usecount)
    {
        spapManagerScript.SetSP(playernum, set, usecount);
    }
    public void SetAttachStatus(PlayerAction.AttachStatus set)
    {
        playerActionScript.SetAttachStatus(set);
    }

    public void AttachSkillTarget(SummonStatus target)
    {
        skillManagerScript.AttachSkillTarget(target);
    }

    public void PasshiveSkill(SummonStatus player)
    {
        skillManagerScript.PasshiveSkill(player);
    }

    public void AddSkillList(CharacterSkill set)
    {
        skillManagerScript.AddPasshiveSkillList(set);
    }

    public int GetAP(int playernumber)
    {
        return spapManagerScript.GetAp(playernumber);
    }

    public List<MassStatus> GetMoveList()
    {

        return boardManagerScript.GetMoveList();
    }

    public void AddSummonMassList(MassStatus set)
    {
        boardManagerScript.AddSummonMassList(set);
    }

    public List<MassStatus> GetSummonMassList()
    {
        return boardManagerScript.GetSummonMassList();
    }

    public void ClearSummonMassList()
    {
        boardManagerScript.ClearSummonMassList();
    }

    public void ClearColorSummonMassList()
    {
        boardManagerScript.ClearColorSummonMassList();
    }

    public void ClearColorUpdateMoveAreaList()
    {
        boardManagerScript.ClearColorUpdateMoveAreaList();
    }

    public void LogUpdate(string log)
    {
        uiManagerScript.LogUpdate(log);
    }

    public void EnemyMoveEndSkill(SummonStatus character)
    {
        boardManagerScript.EnemyMoveEndSkill(character);
    }
}
