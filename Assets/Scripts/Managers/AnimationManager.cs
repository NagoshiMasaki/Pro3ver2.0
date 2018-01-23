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
        moveAnimationScript.SetTarget(target,targetpoint);
    }

    public void SummonAnimation(SummonStatus summon, MassStatus mass, IllustrationStatus illust,int playernumber)
    {
        summonAnimationScript.StartAnimation(summon,mass,illust,playernumber);
    }

    public void ReMoveIllustCard(int playernum, GameObject target)
    {
        deckHandManagerScript.RemoveIllustCard(playernum, target);
    }

    public void DrawCardAnimation(GameObject drawcardobj, Vector3 target,GameObject deckobj)
    {
        drawCardAnimationScript.StartAnimation(drawcardobj, target, deckobj);
    }

}
