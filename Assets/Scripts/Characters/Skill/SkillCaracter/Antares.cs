using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antares : CharacterSkill {

    [SerializeField]
    SummonStatus Parent;
    public override void ActiveSkill()
    {
        AntaresSkill();
    }

    void AntaresSkill()
    {
        if (!Parent.GetIsSkillActive())
        {
            return;
        }

    }
}
