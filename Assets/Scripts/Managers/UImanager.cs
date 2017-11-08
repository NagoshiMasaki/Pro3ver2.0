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

    public void UpdatePhase(SituationManager.Phase phase, int playernum)
    {
        phaseUIScript.UpdatePhase(phase, playernum);
    }

    public void GameFinish(int winnumber)
    {
        gameFinishScript.GameFinish(winnumber);
    }
}
