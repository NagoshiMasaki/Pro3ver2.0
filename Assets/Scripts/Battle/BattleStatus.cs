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
        int enemyhp = enemycharcter.GetHp();
        int playerhp = playercharacter.GetHp();
        enemyhp -= playercharacter.GetPower();
        copyenemydamage = playercharacter.GetPower();
        if (enemyhp <= 0)
        {
            enemycharcter.SetHp(enemyhp);
            return ResultStatus.Win;
        }
        playerhp -= enemycharcter.GetPower();
        copyplayerdamage = enemycharcter.GetPower();
        if (playerhp <= 0)
        {
            playercharacter.SetHp(playerhp);
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
}
