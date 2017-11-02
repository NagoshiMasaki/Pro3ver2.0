using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    List<CharacterSkill> skillList = new List<CharacterSkill>();
    [SerializeField]
    PlayerManager playerManagerScript;
    public void BattleStart(CharacterSkill player, CharacterSkill enemy)
    {
        player.BattleStart();
        enemy.BattleStart();
    }
}
