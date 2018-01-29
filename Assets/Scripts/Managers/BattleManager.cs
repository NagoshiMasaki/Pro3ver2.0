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
    GameObject playerCharacter;
    GameObject enemyCharacter;
    MassStatus attachMassStatus;
    GameObject attachMass;
    [SerializeField]
    PlayerManager playerManagerScript;

    public void ResultBattleAction(BattleStatus.ResultStatus result)
    {
        playerManagerScript.ResultBattleAction(playerSumonCharacter, enemySumonCharacter, result,attachMass,attachMassStatus,playerCharacter,enemyCharacter);
    }

    public void Battle(SummonStatus player, SummonStatus enemy, GameObject attachmass, MassStatus attachmassstatus, GameObject playercharacter, GameObject enemycharacter)
    {
        battleStatusScript.Battle(player,enemy);
        playerCharacter = playercharacter;
        enemyCharacter = enemycharacter;
        attachMassStatus = attachmassstatus;
        attachMass = attachmass;
    }

    public BattleStatus.ResultStatus BattlePreeme(ref int hp)
    {
       return battleStatusScript.BattlePreeme(ref  hp);
    }

    public BattleStatus.ResultStatus BattleLate(ref int hp)
    {
        return battleStatusScript.BattleLate(ref hp);
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
