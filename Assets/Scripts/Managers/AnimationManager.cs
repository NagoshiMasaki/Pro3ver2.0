using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    SummonAnimation summonAnimationScript;
    [SerializeField]
    DeckHandManager deckHandManagerScript;
    [SerializeField]
    DrawCardAnimation drawCardAnimationScript;
    [SerializeField]
    MoveAnimation moveAnimationScript;
    [SerializeField]
    BgmSeManager bgmSeManagerScript;
    [SerializeField]
    BattleManagerAnimation battleManagerAnimationScript;
    [SerializeField]
    BattleManager battleManagerScript;
    [SerializeField]
    SpriteManager spriteMangerScript;
    [SerializeField]
    DictionaryManager dictionaryManagerScript;
    [SerializeField]
    SituationManager situationManagerScript;
    [SerializeField]
    BoardManager boardManagerScript;
    public void CheckMoveCount()
    {
        situationManagerScript.ChecMoveCount();
    }

    public EffectAnimationBase GetEffectObj(int num)
    {
        return dictionaryManagerScript.GetEffectObject(num);
    }

    public Sprite GetNumberSprite(int number)
    {
        return spriteMangerScript.GetNumberList(number);
    }

    public void ResultBattleAction(BattleStatus.ResultStatus result)
    {
        battleManagerScript.ResultBattleAction(result);
    }

    public BattleStatus.ResultStatus BattlePreeme(ref int hp)
    {
        return battleManagerScript.BattlePreeme(ref hp);
    }

    public BattleStatus.ResultStatus BattleLate(ref int hp)
    {
        return battleManagerScript.BattleLate(ref hp);
    }

    public void SetAnimationCard(SummonStatus preemptioncard, SummonStatus latecard)
    {
        battleManagerAnimationScript.SetCharacter(preemptioncard, latecard);
    }

    public void SePlay(int number)
    {
        bgmSeManagerScript.SePlay(number);
    }

    public void MoveAnimation(GameObject target, Vector3 targetpoint)
    {
        if (moveAnimationScript.isGetAnimation())
        {
            moveAnimationScript.AnimationComplete();
        }
        moveAnimationScript.SetTarget(target, targetpoint);
    }

    public void SummonAnimation(SummonStatus summon, MassStatus mass, IllustrationStatus illust, int playernumber)
    {
        summonAnimationScript.StartAnimation(summon, mass, illust, playernumber);
    }

    public void ReMoveIllustCard(int playernum, GameObject target)
    {
        deckHandManagerScript.RemoveIllustCard(playernum, target);
    }

    public void DrawCardAnimation(GameObject drawcardobj, Vector3 target, GameObject deckobj)
    {
        drawCardAnimationScript.StartAnimation(drawcardobj, target, deckobj);
    }

}
