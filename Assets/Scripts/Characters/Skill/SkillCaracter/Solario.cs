using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solario : CharacterSkill
{

    [SerializeField]
    SummonStatus parentObj;

    public override void BattleEnd()
    {
        MoveRecovery();
    }

    public override SummonStatus GetCharacter()
    {
        return parentObj;
    }
    void MoveRecovery()
    {
        SkillManager skillmanager = parentObj.GetSkillManager();
        Debug.Log("ソラリオのスキルを発動しました");
        skillmanager.RemoveMoveList(parentObj.GetSumonObj());
        parentObj.SetIniSkill(false);
        parentObj.SetSkillEfeect(false);
    }
}
