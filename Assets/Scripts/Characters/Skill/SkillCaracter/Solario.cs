using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solario : CharacterSkill {

    [SerializeField]
    SummonStatus ParentObj;

    public override void MoveEnd()
    {
        MoveRecovery();
    }

    void MoveRecovery()
    {
        Debug.Log("ソラリオのスキル");
        SkillManager skillmanager = ParentObj.GetSkillManager();
        if (ParentObj.GetIsSkillActive() && ParentObj.GetIniSkill() && !skillmanager.CheckMoveList(ParentObj.GetSumonObj()) && ParentObj)
        {
            Debug.Log("ソラリオのスキルを発動しました");
            skillmanager.RemoveMoveList(ParentObj.GetSumonObj());
            ParentObj.SetIniSkill(false);
            ParentObj.SetSkillEfeect(false);
        }
    }
}
