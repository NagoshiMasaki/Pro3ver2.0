/////////////////////
//制作者　名越大樹
//クラス名　戦闘を管理するクラス
/////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    BattleStatus battleStatusScript;
    [SerializeField]
    UImanager uiManagerScirpt;
    public BattleStatus.ResultStatus Battle(SummonStatus player, SummonStatus enemy)
    {
       return battleStatusScript.Battle(player,enemy);
    }

    public void GameFinish(int playernum)
    {
       int result = battleStatusScript.GameFinish(playernum);
        uiManagerScirpt.GameFinish(result);
    }

}
