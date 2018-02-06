///////////////////////////////////////////
//製作者 名越大樹
//暮らす　ランスロットのスキルに関するクラス
///////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancelot : CharacterSkill
{
    [SerializeField]
    SummonStatus parentObj;
    public override void BattleEnd()
    {
        Recovery();
    }

    public override SummonStatus GetCharacter()
    {
        return parentObj;
    }

    void Recovery()
    {
        Debug.Log("ランスロットのスキル発動");
        SkillManager skillmanager = parentObj.GetSkillManager();
        int damage = skillmanager.GetEnemyDamage();
        int recovery = damage / 2;
        parentObj.RecoveryHp(recovery);
    }
}
