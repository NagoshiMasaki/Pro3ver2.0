using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonStatus : MonoBehaviour {
    [SerializeField]
    int hp;
    [SerializeField]
    int power;
    [SerializeField]
    bool isMove;
    [SerializeField]
    SkillBase skillobj;
    [SerializeField]
    int playerNumber;
    [SerializeField]
    MoveData.Rate rate;
    public int GetHp()
    {
        return hp;
    }

    public int GetPower()
    {
        return power;
    }

    public void SetIsMove(bool set)
    {
        isMove = set;
    }

    public bool GetIsMove()
    {
        return isMove;
    }

    public int GetPlayer()
    {
        return playerNumber;
    }

    public void SetPlayerNumber(int set)
    {
        playerNumber = set;
    }

    public void SetHp(int set)
    {
        hp = set;
    }
    public MoveData.Rate GetRate()
    {
        return rate;
    }

    public void SetRate(MoveData.Rate set)
    {
        rate = set;
    }
}
