
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUI : MonoBehaviour
{
    [SerializeField]
    Text phaseText;
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
        switch (playernum)
        {
            case 1:
                phaseText.color = Color.red;
                break;
            case 2:
                phaseText.color = Color.blue;
                break;
        }
        bgmNumber = bgmnumber;
        phauseUIAnimationScript.StartAnimation();
        uiManagerScript.SetNextPhase(0);
        phaseText.text = phase.ToString() + "フェイズ" ;
        Color copycolor = phaseText.color;
        copycolor.a = 1.0f;
        phaseText.color = copycolor;
    }

    void Update()
    {
        showTime -= Time.deltaTime;
        Color copycolor = phaseText.color;
        copycolor.a -= Time.deltaTime * alphaAddValue;
        phaseText.color = copycolor;
        if (phaseText.color.a <= 0.0f)
        {
            uiManagerScript.SetNextPhase(nextPhaseNumber);
            uiManagerScript.BgmPlay(bgmNumber);
            phaseText.text = "";
            enabled = false;
            phauseUIAnimationScript.enabled = false;
            showTime = copyShowTime;
        }
    }
}
