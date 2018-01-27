//////////////////////////////////////
//制作者　名越大樹
//クラス　キャラクターのスキルを管理する処理
//////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    List<CharacterSkill> skillList = new List<CharacterSkill>();
    [SerializeField]
    PlayerManager playerManagerScript;
    [SerializeField]
    BattleManager battleManagerScript;
    [SerializeField]
    BoardManager boardManagerScript;
    [SerializeField]
    SkillAction skillActionScript;
    [SerializeField]
    SkillStatus skillStatusScript;
    [SerializeField]
    SituationManager situationManagerScript;
    [SerializeField]
    SpriteManager spriteManagerScript;

    public Sprite GetSpriteNumber(int number)
    {
        if(number == 9999)
        {
            
        }
        else if(number <= 0)
        {
            number = 0;
        }
       return spriteManagerScript.GetNumberList(number);
    }

    public int GetPlayerTurn()
    {
        return situationManagerScript.GetPlayerTurn();
    }
    public SituationManager.Phase GetPhase()
    {
        return situationManagerScript.GetStatus();
    }
    public SkillStatus.Status BattleStart(GameObject playercharacter, GameObject enemycharacter)
    {
        return skillActionScript.BattleStart(playercharacter, enemycharacter);
    }

    public void ActiveSkill(GameObject invoker)
    {
        CharacterSkill skill = invoker.GetComponent<SummonStatus>().GetSkill();
        skill.ActiveSkill();
    }

    public void AddSkillList(CharacterSkill set)
    {
        skillStatusScript.AddSkillList(set);
    }
    public void TurnEndSkillList()
    {
        skillActionScript.TurnEndSkillList();
    }

    public SkillStatus.Status BattleEnd(GameObject playercharacter, GameObject enemycharacter)
    {
        return skillActionScript.BattleEnd(playercharacter, enemycharacter);
    }

    public SkillStatus.Status BattleEnd(GameObject wincharacter)
    {
        return skillActionScript.BattleEnd(wincharacter);
    }

    public void SummonCharacterRemoveat(GameObject target)
    {
        boardManagerScript.SummonCharacterRemoveat(target);
    }

    public bool CheckMoveList(GameObject target)
    {
        return boardManagerScript.CheckMoveList(target);
    }

    public void RemoveMoveList(GameObject target)
    {
        boardManagerScript.ReMoveMoveList(target);
    }
    public void AttachSkillTarget(SummonStatus target)
    {
        skillActionScript.AttachSkillTarget(target);
    }

    public GameObject GetEnemy()
    {
        return battleManagerScript.GetEnemySumoncharacter();
    }

    public GameObject GetPlayer()
    {
        return battleManagerScript.GetPlayerSumonCharacter();
    }

    public void SetStatus(SkillStatus.Status set)
    {
        skillStatusScript.SetStatus(set);
    }

    public SkillStatus.Status Getstatus()
    {
        return skillStatusScript.GetStatus();
    }

    public void DestoryInstancePos()
    {
        boardManagerScript.DestroyInstancePosMass();
    }

    public void SetAttachStatus(PlayerAction.AttachStatus set)
    {
        playerManagerScript.SetAttachStatus(set);
    }

    public BattleManager GetBattleManager()
    {
        return battleManagerScript;
    }

    public SkillStatus.Status MoveEndSkill(GameObject character)
    {
        return skillActionScript.MoveEnd(character);
    }

    public int GetEnemyDamage()
    {
        return battleManagerScript.GetEnemyDamage();
    }

    public int GetPlayerDamage()
    {
        return battleManagerScript.GetPlayerDamage();
    }

    public List<MassStatus> GetSearchMassAround(int length, int side)
    {
        return boardManagerScript.SearchMassAround(length, side);
    }

    public List<MassStatus> GetSearchPlayerMass(int player)
    {
        return boardManagerScript.SearchPlayerMass(player);
    }

    public void PasshiveSkill(SummonStatus player)
    {
        skillActionScript.InvocationPasshiveSkillList(player);
    }

    public void RemoveAtPasshiveSkillList(CharacterSkill target)
    {
        skillStatusScript.RemoveatPhassiveSkillList(target);
    }

    public void AddPasshiveSkillList(CharacterSkill set)
    {
        skillStatusScript.AddSkillList(set);
    }

    ////////////////////////////////////////////////
    //キング専用のスキル
    ////////////////////////////////////////////////
    public void AllMyArea(int player)
    {
        boardManagerScript.AllMyArea(player);
    }

    public void AllDefaultArea()
    {
        boardManagerScript.AllDefaultArea();
    }
    /////////////////////////////////////////////
    //////////////////////////////////////////////
}
