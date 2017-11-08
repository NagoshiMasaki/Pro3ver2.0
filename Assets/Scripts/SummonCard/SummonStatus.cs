///////////////////////////////////
//制作者　名越大樹
//クラス　フィールド上に生成されたキャラクターのスキル
///////////////////////////////////
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
    CharacterSkill skillobj;
    [SerializeField]
    GameObject skillEfect;
    [SerializeField]
    int playerNumber;
    [SerializeField]
    MoveData.Rate rate;
    bool isSkillActive = false;
    [SerializeField]
    SkillManager skillManagerScript;
    MassStatus onAttachMass;
    [SerializeField]
    int intervalSkll;
    [SerializeField]
    int skillCount;

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

    public void SetSkillEfeect(bool set)
    {
            isSkillActive = set;
            skillEfect.SetActive(isSkillActive);
    }

    public void SetSkillEfeect()
    {
        isSkillActive = !isSkillActive;
        skillEfect.SetActive(isSkillActive);
    }
    public bool GetIsSkillActive()
    {
        return isSkillActive;
    }

    public CharacterSkill GetSkill()
    {
        return skillobj;
    }

    public void SetSkillManager(SkillManager set)
    {
        skillManagerScript = set;
    }

    public SkillManager GetSkillManager()
    {
        return skillManagerScript;
    }

    public GameObject GetSumonObj()
    {
        return gameObject;
    }

    public void SetAttachMass(MassStatus set)
    {
        onAttachMass = set;
    }

    public MassStatus GetAttachMass()
    {
        return onAttachMass;
    }

    public void AddIntervalSkill(int set)
    {
        intervalSkll += set;
    }

    public int GetIntervalSkill()
    {
        return intervalSkll;
    }

    public void DestoryThisObj()
    {
        onAttachMass.SetMassStatus(BoardManager.MassMoveStatus.None);
        onAttachMass.SetCharacterObj(null);
        Destroy(gameObject);
    }

    public void SetMassNull()
    {
        onAttachMass.SetMassStatus(BoardManager.MassMoveStatus.None);
        onAttachMass.SetCharacterObj(null);
    }

    public void SubscriotnSkillCount()
    {
        skillCount--;
    }

    public int GetSkillCount()
    {
        return skillCount;
    }
}
