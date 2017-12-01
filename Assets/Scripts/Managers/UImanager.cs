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
    public void ChangerTurn(int num)
    {
        playerUIScript.ChangerTurn(num);
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
}
