using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerAnimation:MonoBehaviour
{
    [SerializeField]
    SummonStatusAnimation preemptionCard;
    [SerializeField]
    SummonStatusAnimation lateCard;
    [SerializeField]
    BattleActionAnimation battleActionAnimationScript;
    [SerializeField]
    AnimationManager animaitonManagerScript;
    BattleStatus.ResultStatus resultstatus;
    int preemptionCardAttackEffectNumber;
    int lateCardAttackEffectNumber;
    EffectAnimationBase effect;
    public enum AnimationStatus
    {
        None,
        Wait,
        BattleWait,
        LateBattleWait,
        Fade,
        BattleEffect,
        Destory,
        Dead,
    }

    public enum BattleAnimationStatus
    {
        None,
        PreemptionAttack,
    }
    BattleAnimationStatus battleAnimationStatus;
    BattleActionAnimation battleActionAnimation;

    public SummonStatusAnimation GetPreemptionCard()
    {
        return preemptionCard;
    }

    public SummonStatusAnimation GetLateCard()
    {
        return lateCard;
    }

    public void CompleteAnimation(AnimationStatus set)
    {
        Debug.Log("アニメーション終了");
        switch (set)
        {
            case AnimationStatus.Fade:
                battleActionAnimationScript.SetAnimation(AnimationStatus.Wait);
                break;
            case AnimationStatus.Wait://松アニメーションが終わったとき
                WaitFunction();
                break;
            case AnimationStatus.LateBattleWait:
                break;
            case AnimationStatus.BattleWait:
                CheckBattleResult();
//                battleActionAnimationScript.SetAnimation(AnimationStatus.None);
                break;
            case AnimationStatus.Dead:
                preemptionCard.gameObject.SetActive(false);
                lateCard.gameObject.SetActive(false);
                ResultBattleAction();
                Debug.Log("バトル終了");
                battleActionAnimationScript.enabled = false;
                break;
        }
    }

    public void WaitFunction()
    {
        EffectAnimationBase effectobj = animaitonManagerScript.GetEffectObj(preemptionCardAttackEffectNumber);
        EffectAnimationBase instanceobj = Instantiate(effectobj, lateCard.transform.position, Quaternion.identity);
        instanceobj.Ini(this);
        effect = instanceobj;
        battleAnimationStatus = BattleAnimationStatus.PreemptionAttack;
    }

    void BattleEffectAction()
    {
        Debug.Log("オラオラオラ!");

        int hp = 0;
        resultstatus = animaitonManagerScript.BattlePreeme(ref hp);
        if (hp <= 0)
        {
            hp = 0;
        }
        Sprite hpsprite = animaitonManagerScript.GetNumberSprite(hp);
        lateCard.SetHpNumberSprite(hpsprite);
        animaitonManagerScript.SePlay(1);
        battleActionAnimationScript.SetAnimation(AnimationStatus.BattleWait);
    }

    public void CompleteEffectAnimation()
    {
        Debug.Log("終了");
        Destroy(effect.gameObject);
        switch(battleAnimationStatus)
        {
            case BattleAnimationStatus.PreemptionAttack:
                BattleEffectAction();
                break;
        }
    }

    void ResultBattleAction()
    {
        animaitonManagerScript.ResultBattleAction(resultstatus);
    }
    void CheckBattleResult()
    {
        switch(resultstatus)
        {
            case BattleStatus.ResultStatus.Win:
                battleActionAnimationScript.SetDeadSummon(lateCard);
                battleActionAnimationScript.SetAnimation( AnimationStatus.Dead);
                break;
        }
    }

    public void SetCharacter(SummonStatus preemptioncard, SummonStatus latecard)
    {
        preemptionCardAttackEffectNumber = preemptioncard.GetAttackEffectNumber();
        lateCardAttackEffectNumber = latecard.GetAttackEffectNumber();
        preemptionCard.gameObject.SetActive(true);
        lateCard.gameObject.SetActive(true);
        SetSummontatus(preemptioncard, preemptionCard);
        SetSummontatus(latecard,lateCard);
        battleActionAnimationScript.Ini(preemptionCard,lateCard);
    }

    static void SetSummontatus(SummonStatus summon,SummonStatusAnimation card)
    {
        SpriteRenderer charactersprite = null;
        GameObject hpnumberobj = null;
        GameObject attacknumberobj = null;
        GameObject frameobj =null;
        summon.GetAllStatus(ref hpnumberobj,ref attacknumberobj,ref charactersprite,ref frameobj);
        Sprite hp = hpnumberobj.GetComponent<SpriteRenderer>().sprite;
        Sprite attack = attacknumberobj.GetComponent<SpriteRenderer>().sprite;
        Sprite frame = frameobj.GetComponent<SpriteRenderer>().sprite;
        card.CopySetSprite(charactersprite.sprite,hp,attack,frame);
    }
}
