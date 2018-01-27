///////////////////////////////////
//制作者　名越大樹
//クラス　フィールド上に生成されたキャラクターのステータス
///////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonStatus : MonoBehaviour
{
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
    MassStatus copyAttachMass;
    [SerializeField]
    int intervalSkll;
    [SerializeField]
    int skillCount;
    [SerializeField]
    bool iniSkill;
    [SerializeField]
    int ap;
    [SerializeField]
    SpriteRenderer hpNumber;
    [SerializeField]
    SpriteRenderer attackNumber;
    [SerializeField]
    SpriteRenderer mysprite;
    [SerializeField]
    GameObject frameObj;
    [SerializeField]
    string summonName;
    [SerializeField]
    Sprite infomationSprite;
    [SerializeField]
    GameObject hpNumberObj;
    [SerializeField]
    GameObject attackNumberObj;
    [SerializeField]
    SpriteRenderer characterSprite;
    [SerializeField]
    int attackEffectNumber;

    public int GetAttackEffectNumber()
    {
        return attackEffectNumber;
    }

    public void GetAllStatus(ref GameObject hpnumberobj, ref GameObject attacknumberobj,ref SpriteRenderer charactersprite,ref GameObject frame)
    {
        hpnumberobj = hpNumberObj;
        attacknumberobj = attackNumberObj;
        charactersprite = characterSprite;
        frame = frameObj;
    }

    public void GetIconObjects(ref GameObject hpnumberobj, ref GameObject attacknumberobj)
    {
        hpnumberobj = hpNumberObj;
        attacknumberobj = attackNumberObj;
    }

    public Sprite GetInfomationSptite()
    {
        return infomationSprite;
    }

    public string GetName()
    {
        return summonName;
    }

    public GameObject GetFrame()
    {
        return frameObj;
    }
    public void SetColor(bool color)
    {
        if (color)
        {
            mysprite.color = Color.white;
        }
        else
        {
            mysprite.color = Color.gray;
        }
    }


    public void Ini()
    {
        characterSprite = GetComponent<SpriteRenderer>();
        Debug.Log(characterSprite);
        SetSpriteAttack();
        SetSpriteHp();
    }

    public bool GetIniSkill()
    {
        return iniSkill;
    }

    public void SetIniSkill(bool set)
    {
        iniSkill = set;
    }

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

    public void Damage(int damage)
    {
        hp = hp - damage;
        SetSpriteHp();
    }

    public void SetHp(int set)
    {
        hp = set;
        SetSpriteHp();
    }
    public MoveData.Rate GetRate()
    {
        return rate;
    }

    public int GetAP()
    {
        return ap;
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
        copyAttachMass = onAttachMass;
        onAttachMass = set;
    }

    public MassStatus GetCopyAttachMass()
    {
        return copyAttachMass;
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
        skillManagerScript.RemoveAtPasshiveSkillList(skillobj);
        onAttachMass.SetCharacterObj(null);
        skillManagerScript.SummonCharacterRemoveat(gameObject);
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

    public void RecoveryHp(int add)
    {
        hp += add;
        SetSpriteHp();
    }

    public void AddPower(int add)
    {
        power += add;
        SetSpriteAttack();
    }

    void SetSpriteAttack()
    {
        Sprite sprite = skillManagerScript.GetSpriteNumber(power);
        attackNumber.sprite = sprite;
    }

    void SetSpriteHp()
    {
        Sprite sprite = skillManagerScript.GetSpriteNumber(hp);
        hpNumber.sprite = sprite;
    }
}
