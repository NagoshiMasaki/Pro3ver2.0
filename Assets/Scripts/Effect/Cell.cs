using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : EffectAnimationBase
{
    BattleManagerAnimation battleManagerAnimationScript;
    [SerializeField]
    float animationTime;

    void Update()
    {
        animationTime -= Time.deltaTime;
        if (animationTime <= 0.0f)
        {
            battleManagerAnimationScript.CompleteEffectAnimation();
        }
    }

    public override void Ini(BattleManagerAnimation set)
    {
        battleManagerAnimationScript = set;
    }
}
