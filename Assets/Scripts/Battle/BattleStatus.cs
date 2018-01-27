using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatus : MonoBehaviour
{
    [SerializeField]
    public enum ResultStatus
    {
        Win,
        Draw,
        Lose,
        Next,
    }
    [SerializeField]
    UImanager uiManagerScript;
    int copyplayerdamage;
    int copyenemydamage;
    [SerializeField]
    SummonStatus playerCharacter;
    [SerializeField]
    SummonStatus enemyCharacter;
    public void Battle(SummonStatus playercharacter, SummonStatus enemycharcter)
    {
        playerCharacter = playercharacter;
        enemyCharacter = enemycharcter;
 
    }

    public ResultStatus BattlePreeme(ref int hp)
    {
        /////////////////////
        //ログ開始
        /////////////////////
        string log = "";
        int enemyhp = enemyCharacter.GetHp();
        int playerhp = playerCharacter.GetHp();
        log += "「";
        log += playerCharacter.GetName();
        log += "」";
        log += "の攻撃";
        uiManagerScript.LogUpdate(log);
        log = "";
        enemyhp -= playerCharacter.GetPower();
        log += "「";
        log += playerCharacter.GetName();
        log += "」";
        log += "に" + playerCharacter.GetPower().ToString() + "のダメージ";
        uiManagerScript.LogUpdate(log);
        copyenemydamage = playerCharacter.GetPower();
        hp = enemyhp;
        if (enemyhp <= 0)
        {
            enemyCharacter.SetHp(enemyhp);
            WinLog(playerCharacter);
            return ResultStatus.Win;
        }
        return ResultStatus.Next;
        /////////////////////
        //ログ終了
        /////////////////////
    }

    public ResultStatus BattleLate()
    {
        int enemyhp = enemyCharacter.GetHp();
        int playerhp = playerCharacter.GetHp();

        playerhp -= enemyCharacter.GetPower();
        copyplayerdamage = enemyCharacter.GetPower();

        if (playerhp <= 0)
        {
            playerCharacter.SetHp(playerhp);
            WinLog(enemyCharacter);
            return ResultStatus.Lose;
        }
        playerCharacter.SetHp(playerhp);
        enemyCharacter.SetHp(enemyhp);
        return ResultStatus.Draw;

    }

    public int GameFinish(int playernumber)
    {
        switch (playernumber)
        {
            case 1:
                return 2;
            case 2:
                return 1;
        }
        return 0;
    }

    public int GetPlayerDamage()
    {
        return copyplayerdamage;
    }

    public int GetEnemyDamage()
    {
        return copyenemydamage;
    }

    void WinLog(SummonStatus character)
    {
        string log = "";
        log += "「";
        log += character.GetName();
        log += "」";
        log += "の勝利";
        uiManagerScript.LogUpdate(log);
    }
}
