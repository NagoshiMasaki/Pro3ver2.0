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

    public override SummonStatus GetCharacter()
    {
        return Parent;
    }

    void AntaresSkill()
    {
        if (!Parent.GetIsSkillActive())
        {
            return;
        }

    }
}
