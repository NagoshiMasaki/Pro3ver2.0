/////////////////////////////////////
//製作者　名越大樹
//クラス　ベヒモスのスキルに関するクラス
/////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behemoth : CharacterSkill
{
    [SerializeField]
    SummonStatus parentObj;
    [SerializeField]
    int recoveryvalue;
    [SerializeField]
    int skilldamage;
    public override void BattleEnd()
    {
        GroupRecovery();
    }
    public override SummonStatus GetCharacter()
    {
        return parentObj;
    }
    void GroupRecovery()
    {
        Debug.Log("ベヒーモスのスキル発動");
        int hp =parentObj.GetHp();
        hp -= skilldamage;
        parentObj.SetHp(hp);
        if(parentObj.GetHp() <= 0)
        {
            parentObj.DestoryThisObj();
            return;
        }
        SkillManager skillmanager = parentObj.GetSkillManager();
        MassStatus mass = parentObj.GetAttachMass();
        int length = mass.GetLengthNumber();
        int side = mass.GetSideNumber();
        List<MassStatus> masslist = new List<MassStatus>();
           masslist = skillmanager.GetSearchMassAround(length,side);
        for(int count = 0; count < masslist.Count;count++)
        {
            GameObject character = masslist[count].GetCharacterObj();
            if(character != null)
            {
                SummonStatus status = character.GetComponent<SummonStatus>();
                int number = status.GetPlayer();
                if(number == parentObj.GetPlayer())
                {
                    status.RecoveryHp(recoveryvalue);
                }
            }
        }
    }
}
