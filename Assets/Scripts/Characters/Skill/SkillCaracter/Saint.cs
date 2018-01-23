using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saint : CharacterSkill {

    [SerializeField]
    SummonStatus parentObj;
    [SerializeField]
    int skillDamage;

    public override void EnemyMoveEndSkill(SummonStatus character)
    {
        SaintSkill(character);
    }

    public override SummonStatus GetCharacter()
    {
        return parentObj;
    }

    void SaintSkill(SummonStatus character)
    {
        SkillManager skillmanager = parentObj.GetSkillManager();
        SituationManager.Phase phase = skillmanager.GetPhase();
        if(phase != SituationManager.Phase.Battle)
        {
            return;
        }
        Debug.Log("聖女のスキル発動");
        MassStatus onmass = character.GetAttachMass();
        int massnumber = onmass.GetMaterialNumber();
        int playernumber = parentObj.GetPlayer();
        if (playernumber != massnumber)
        {
            return;
        }
        if (playernumber != character.GetPlayer())
        {
            Debug.Log("敵なので容赦なく攻撃");
            character.Damage(skillDamage);
        }
    }
}
