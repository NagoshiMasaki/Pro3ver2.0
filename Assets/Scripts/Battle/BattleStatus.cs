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

    public ResultStatus Battle(SummonStatus playercharacter, SummonStatus enemycharcter)
    {
        int enemyhp = enemycharcter.GetHp();
        int playerhp = playercharacter.GetHp();
        enemyhp -= playercharacter.GetPower();
        if(enemyhp <= 0)
        {
            enemycharcter.SetHp(enemyhp);
            return ResultStatus.Win;
        }
        playerhp -= enemycharcter.GetPower();
        if(playerhp <= 0)
        {
            playercharacter.SetHp(playerhp);
            return ResultStatus.Lose;
        }
        playercharacter.SetHp(playerhp);
        enemycharcter.SetHp(enemyhp);
        return ResultStatus.Draw;
    }
}
