using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionAnimation : MonoBehaviour
{
    [SerializeField]
    BattleManagerAnimation.AnimationStatus status;
    [SerializeField]
    BattleManagerAnimation battleManagerAnimationScript;
    [SerializeField]
    float fadeValue;
    [SerializeField]
    Color iniColor;
    [SerializeField]
    float waitValue;
    float copyWaitValue;
    [SerializeField]
    float battleWaitValue;
    float copybattleWaitValue;
    SummonStatusAnimation deadSummonStatusAnimaiton;

    public void SetDeadSummon(SummonStatusAnimation set)
    {
       deadSummonStatusAnimaiton = set;
    }

    void Start()
    {
        copyWaitValue = waitValue;
        copybattleWaitValue = battleWaitValue;
        enabled = false;
    }

    

    public void Ini(SummonStatusAnimation preemptioncard, SummonStatusAnimation latecard)
    {
        preemptioncard.IniSetColor(iniColor);
        latecard.IniSetColor(iniColor);
        status = BattleManagerAnimation.AnimationStatus.Fade;
        enabled = true;
    }

    public void SetAnimation(BattleManagerAnimation.AnimationStatus set)
    {
        status = set;
        switch (set)
        {
            case BattleManagerAnimation.AnimationStatus.Wait:
                waitValue = copyWaitValue;
                break;
            case BattleManagerAnimation.AnimationStatus.BattleWait:
                battleWaitValue = copybattleWaitValue;
                break;
        }
        if (set != BattleManagerAnimation.AnimationStatus.None)
        {
            enabled = true;
        }
    }

    void Update()
    {
        Animation();
    }

    void DeadAnimation()
    {
        Color copycolor = deadSummonStatusAnimaiton.GetCharacterColor();
        copycolor.a -= Time.deltaTime;
        deadSummonStatusAnimaiton.UpdateColor(copycolor);
        if(copycolor.a <= 0.0f)
        {
            battleManagerAnimationScript.CompleteAnimation(status);
        }
    }

    void Animation()
    {
        switch (status)
        {
            case BattleManagerAnimation.AnimationStatus.Fade:
                FadeAnimation();
                break;
            case BattleManagerAnimation.AnimationStatus.Wait:
                WaitAnimation();
                break;
            case BattleManagerAnimation.AnimationStatus.BattleWait:
                BattleWaitAnimation();
                break;
            case BattleManagerAnimation.AnimationStatus.Dead:
                DeadAnimation();
                break;

        }
    }

    void BattleWaitAnimation()
    {
        battleWaitValue -= Time.deltaTime;
        if (battleWaitValue <= 0.0f)
        {
            enabled = false;
            battleManagerAnimationScript.CompleteAnimation(status);
        }
    }

    void WaitAnimation()
    {
        waitValue -= Time.deltaTime;
        if(waitValue <=0.0f)
        {
            enabled = false;
            battleManagerAnimationScript.CompleteAnimation(status);
        }
    }


    void FadeAnimation()
    {
        SummonStatusAnimation preemptioncard = battleManagerAnimationScript.GetPreemptionCard();
        SummonStatusAnimation latecard = battleManagerAnimationScript.GetLateCard();
        Color getcolor = preemptioncard.GetCharacterColor();
        getcolor.a += fadeValue * Time.deltaTime;
        preemptioncard.UpdateColor(getcolor);
        latecard.UpdateColor(getcolor);
        if(getcolor.a >= 1.0f)
        {
            battleManagerAnimationScript.CompleteAnimation(status);
        }
    }

    public void SetAnimationStatus(BattleManagerAnimation.AnimationStatus set)
    {
        status = set;
    }
}
