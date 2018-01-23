////////////////////////////////
//制作者　名越大樹
//クラス　キャラクター「ジャジャ」のスキル処理
////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zyazya : CharacterSkill
{
    [SerializeField]
    SummonStatus parentObj;
    public override void BattleStart()
    {
        Debug.Log("ジャジャスキル発動");
        DestroyTarget();
    }

    public override SummonStatus GetCharacter()
    {
        return parentObj;
    }

    void DestroyTarget()
    {
        if (parentObj.GetIsSkillActive()) {
            SkillManager skillmanager = parentObj.GetSkillManager();
            GameObject target = skillmanager.GetEnemy();
            SummonStatus targetstatus = target.GetComponent<SummonStatus>();
            if(targetstatus.GetRate() == MoveData.Rate.King)
            {
                return;
            }
            skillmanager.DestoryInstancePos();
            target.GetComponent<SummonStatus>().DestoryThisObj();
            skillmanager.SetStatus(SkillStatus.Status.Finish);
        }
    }
}
