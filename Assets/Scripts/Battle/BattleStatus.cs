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
        Lose
    }
    [SerializeField]
    UImanager uiManagerScript;
    int copyplayerdamage;
    int copyenemydamage;
    public ResultStatus Battle(SummonStatus playercharacter, SummonStatus enemycharcter)
    {
        string log = "";
        int enemyhp = enemycharcter.GetHp();
        int playerhp = playercharacter.GetHp();
        log += "「";
        log += playercharacter.GetName();
        log += "」";
        log += "の攻撃";
        uiManagerScript.LogUpdate(log);
        log = "";
        enemyhp -= playercharacter.GetPower();
        log += "「";
        log += playercharacter.GetName();
        log += "」";
        log += "に" + playercharacter.GetPower().ToString() + "のダメージ";
        uiManagerScript.LogUpdate(log);
        copyenemydamage = playercharacter.GetPower();
        if (enemyhp <= 0)
        {
            enemycharcter.SetHp(enemyhp);
            WinLog(playercharacter);
            return ResultStatus.Win;
        }
        playerhp -= enemycharcter.GetPower();
        copyplayerdamage = enemycharcter.GetPower();
        if (playerhp <= 0)
        {
            playercharacter.SetHp(playerhp);
            WinLog(enemycharcter);
            return ResultStatus.Lose;
        }
        playercharacter.SetHp(playerhp);
        enemycharcter.SetHp(enemyhp);
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
