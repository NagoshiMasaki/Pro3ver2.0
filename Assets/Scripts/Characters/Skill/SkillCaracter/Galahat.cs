using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galahat : CharacterSkill {

    [SerializeField]
    SummonStatus parentObj;
    public override void BattleStart()
    {
        AddPower();
    }

    public override SummonStatus GetCharacter()
    {
        return parentObj;
    }

    void AddPower()
    {
        Debug.Log("ガルハットのスキル発動");
        SkillManager skillmanager = parentObj.GetSkillManager();
        int damage = skillmanager.GetEnemyDamage();
        int add = damage / 2;
        parentObj.AddPower(add);
    }
}
