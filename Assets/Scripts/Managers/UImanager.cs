//////////////////////////////////
//制作者　名越大樹
//クラス　ゲームのUIを管理するクラス
//////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    PhaseUI phaseUIScript;
    [SerializeField]
    GameFinishUI gameFinishScript;
    [SerializeField]
    PlayerUI playerUIScript;
    [SerializeField]
    LogUI logUIScript;
    [SerializeField]
    Timer timerScript;
    [SerializeField]
    SituationManager situationManagerScript;
    public void Reset()
    {
        timerScript.Reset();
    }

    public void ChangerTurn(int num)
    {
        playerUIScript.ChangerTurn(num);
    }

    public void TurnChange()
    {
        situationManagerScript.TurnChange();
    }

    public void UpdatePhase(SituationManager.Phase phase, int playernum)
    {
        phaseUIScript.enabled = true;
        phaseUIScript.UpdatePhase(phase, playernum);
    }

    public void GameFinish(int winnumber)
    {
        gameFinishScript.GameFinish(winnumber);
    }

    public void LogUpdate(string log)
    {
        logUIScript.LogUpdate(log);
    }
}
