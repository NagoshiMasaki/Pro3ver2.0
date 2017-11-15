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
    SummonStatus ParentObj;
    public override void BattleEnd()
    {
        Recovery();
    }

    void Recovery()
    {
        Debug.Log("ランスロットのスキル発動");
        SkillManager skillmanager = ParentObj.GetSkillManager();
        int damage = skillmanager.GetEnemyDamage();
        int recovery = damage / 2;
        ParentObj.RecoveryHp(recovery);
    }
}
