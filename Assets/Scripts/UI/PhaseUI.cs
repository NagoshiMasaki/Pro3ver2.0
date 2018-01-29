using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUI : MonoBehaviour
{
    [SerializeField]
    Image phaseImage;
    [SerializeField]
    float showTime;
    [SerializeField]
    float copyShowTime;
    [SerializeField]
    PhaseUIAnimation phauseUIAnimationScript;
    [SerializeField]
    float alphaAddValue;
    int bgmNumber;
    [SerializeField]
    UImanager uiManagerScript;
    [SerializeField]
    int nextPhaseNumber;

    public void UpdatePhase(SituationManager.Phase phase,int playernum,int bgmnumber)
    {
        Sprite turnsprite = null;
        switch (playernum)
        {
            case 1:
                switch(phase)
                {
                    case SituationManager.Phase.Battle:
                        turnsprite = uiManagerScript.GetTurnSprite(ConstValues.YOUR_BATTLE_PHASE);
                        break;
                    case SituationManager.Phase.Summon:
                        turnsprite = uiManagerScript.GetTurnSprite(ConstValues.YOUR_SUMMON_PHASE);
                        break;
                }
                break;
            case 2:
                switch (phase)
                {
                    case SituationManager.Phase.Battle:
                        turnsprite = uiManagerScript.GetTurnSprite(ConstValues.ENEMY_BATTLE_PHASE);
                        break;
                    case SituationManager.Phase.Summon:
                        turnsprite = uiManagerScript.GetTurnSprite(ConstValues.ENEMY_SUMMON_PHASE);
                        break;
                }
                break;
        }
        phaseImage.sprite = turnsprite;
        bgmNumber = bgmnumber;
        phauseUIAnimationScript.StartAnimation();
        uiManagerScript.SetNextPhase(0);
        Color copycolor = phaseImage.color;
        copycolor.a = 1.0f;
        phaseImage.color = copycolor;
    }

    void Update()
    {
        showTime -= Time.deltaTime;
        Color copycolor = phaseImage.color;
        copycolor.a -= Time.deltaTime * alphaAddValue;
        phaseImage.color = copycolor;
        if (phaseImage.color.a <= 0.0f)
        {
            uiManagerScript.SetNextPhase(nextPhaseNumber);
            uiManagerScript.BgmPlay(bgmNumber);
            enabled = false;
            phauseUIAnimationScript.enabled = false;
            showTime = copyShowTime;
        }
    }
}
