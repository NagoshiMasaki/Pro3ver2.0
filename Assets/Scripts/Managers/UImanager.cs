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
    [SerializeField]
    BgmSeManager bgmSeScript;
    [SerializeField]
    GameObject nextPhase;
    [SerializeField]
    SpriteManager spriteManagerScript;

    public Sprite GetTurnSprite(int number)
    {
        return spriteManagerScript.GetTurnSprite(number);
    }

    public void SePlay(int number)
    {
        bgmSeScript.SePlay(number);
    }

    /// <summary>
    /// デバック用
    /// </summary>
    /// <param name="set"></param>
    public void SetNextPhase(int number)
    {
        nextPhase.layer = number;
    }

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

    public void BgmPlay(int number)
    {
        bgmSeScript.BgmPlay(number);
    }

    public void UpdatePhase(SituationManager.Phase phase, int playernum,int bgmnumber)
    {
        phaseUIScript.enabled = true;
        phaseUIScript.UpdatePhase(phase, playernum,bgmnumber);
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
