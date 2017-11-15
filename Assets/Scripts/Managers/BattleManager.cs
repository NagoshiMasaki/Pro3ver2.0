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
    GameObject playerSumonCharacter;
    GameObject enemySumonCharacter;
    public BattleStatus.ResultStatus Battle(SummonStatus player, SummonStatus enemy)
    {
       return battleStatusScript.Battle(player,enemy);
    }

    public void GameFinish(int playernum)
    {
       int result = battleStatusScript.GameFinish(playernum);
        uiManagerScirpt.GameFinish(result);
    }

    public GameObject GetPlayerSumonCharacter()
    {
        return playerSumonCharacter;
    }

    public GameObject GetEnemySumoncharacter()
    {
        return enemySumonCharacter;
    }

    public void SetSumonCharacters(GameObject player,GameObject enemy)
    {
        playerSumonCharacter = player;
        enemySumonCharacter = enemy;
    }

    public int GetPlayerDamage()
    {
        return battleStatusScript.GetPlayerDamage();
    }

    public int GetEnemyDamage()
    {
        return battleStatusScript.GetEnemyDamage();
    }
}
