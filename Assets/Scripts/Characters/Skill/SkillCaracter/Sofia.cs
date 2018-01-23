using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sofia : CharacterSkill
{

    [SerializeField]
    SummonStatus parentObj;

    bool isSkill = false;
    public override void KingSkill()
    {
        AllMyAreaMass();
    }

    public override SummonStatus GetCharacter()
    {
        return parentObj;
    }

    public override void TurnEnd()
    {
        AllDefaultMass();
    }
    void AllMyAreaMass()
    {
        if (isSkill)
        {
            return;
        }
        SkillManager skillManager = parentObj.GetSkillManager();
        skillManager.AllMyArea(parentObj.GetPlayer());
        isSkill = true;

    }

    void AllDefaultMass()
    {
        if (!isSkill)
        {
            return;
        }
        SkillManager skillManager = parentObj.GetSkillManager();
        SituationManager.Phase phase = skillManager.GetPhase();
        int turn = skillManager.GetPlayerTurn();
        if (turn == parentObj.GetPlayer())
        {
            return;
        }
        if (phase != SituationManager.Phase.End)
        {
            return;
        }
        skillManager.AllDefaultArea();
        isSkill = false;
    }
}
