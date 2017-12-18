
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUI : MonoBehaviour {

    [SerializeField]
    Text phaseText;
    [SerializeField]
    float showTime;
    [SerializeField]
    float copyShowTime;
    public void UpdatePhase(SituationManager.Phase phase,int playernum)
    {
        string colortag = "";
        string colortagfinishtag = "</color>";
        switch (playernum)
        {
            case 1:
                colortag = "<color=red>";
                break;
            case 2:
                colortag = "<color=blue>";
                break;
        }
        phaseText.text = colortag + phase.ToString() + "フェイズ" +colortagfinishtag;
    }

    void Update()
    {
        showTime -= Time.deltaTime;
        if (showTime <= 0.0f)
        {
            phaseText.text = "";
            enabled = false;
            showTime = copyShowTime;
        }
    }
}
