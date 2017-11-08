/////////////////////////////////////
//制作者　名越大樹
//クラス　キャラクターのスキルの実行する処理
/////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour {

    [SerializeField]
    SkillStatus skillStatusScript;
    [SerializeField]
    SkillManager skillManagerScript;

    /// <summary>
    /// 自分のターンの開始時の処理
    /// </summary>
    public void MyTurnStart()
    {
        List<CharacterSkill> invorkerlist = skillStatusScript.GetInvokerList();

        for (int count = 0;count < invorkerlist.Count;count++)
        {
            invorkerlist[count].MyTurnStart();
        }
    }

    /// <summary>
    /// 相手のターンが開始した時の処理
    /// </summary>
    public void EnemyTurnStart()
    {
        List<CharacterSkill> invorkerlist = skillStatusScript.GetInvokerList();

        for (int count = 0; count < invorkerlist.Count; count++)
        {
            invorkerlist[count].EnemyTurnStart();
        }
    }

    /// <summary>
    /// 戦闘が開始した時の処理
    /// </summary>
    /// <returns></returns>
    public SkillStatus.Status BattleStart(GameObject playercharacter, GameObject enemycharacter)
    {
        BattleManager battleManagerScript = skillManagerScript.GetBattleManager();
        battleManagerScript.SetSumonCharacters(playercharacter, enemycharacter);
        SkillStatus.Status status = skillStatusScript.GetStatus();
        CharacterSkill playerskill = playercharacter.GetComponent<SummonStatus>().GetSkill();
        CharacterSkill enemyskill = enemycharacter.GetComponent<SummonStatus>().GetSkill();
        playerskill.BattleStart();
        if (status == SkillStatus.Status.Finish)
        {
            return status;
        }
        enemyskill.BattleStart();
        return SkillStatus.Status.None;
    }

    /// <summary>
    /// 発動の対照のターゲットの処理
    /// </summary>
    /// <param name="target"></param>
    public void AttachSkillTarget(SummonStatus target)
    {
       CharacterSkill invorker = skillStatusScript.GetInvorker();
        invorker.AttachSkillCharacter(target);
    }
}
