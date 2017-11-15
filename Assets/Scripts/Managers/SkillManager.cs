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

    public SkillStatus.Status BattleStart(GameObject playercharacter, GameObject enemycharacter)
    {
        return skillActionScript.BattleStart(playercharacter, enemycharacter);
    }

    public void ActiveSkill(GameObject invoker)
    {
        CharacterSkill skill = invoker.GetComponent<SummonStatus>().GetSkill();
        skill.ActiveSkill();
    }

    public SkillStatus.Status BattleEnd(GameObject playercharacter, GameObject enemycharacter)
    {
        return skillActionScript.BattleEnd(playercharacter, enemycharacter);
    }

    public SkillStatus.Status BattleEnd(GameObject wincharacter)
    {
        return skillActionScript.BattleEnd(wincharacter);
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
}
