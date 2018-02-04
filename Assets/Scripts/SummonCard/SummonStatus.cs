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
    [SerializeField]
    SpriteRenderer hpSpriteRedner1;
    [SerializeField]
    SpriteRenderer hpSpriteRender2;
    [SerializeField]
    SpriteRenderer hpSpriteRender3;
    [SerializeField]
    SpriteRenderer attackSpriteRender1;
    [SerializeField]
    SpriteRenderer attackSpriteRender2;
    [SerializeField]                
    SpriteRenderer attackSpriteRender3;
    int instanceID;

    public int InstanceID { get { return instanceID; } set { instanceID = value; } }

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
        instanceID = InstanceId.instanceid;
        InstanceId.instanceid++;
        characterSprite = GetComponent<SpriteRenderer>();
        SetSpriteAttack();
        SetSpriteHp();
        hpSpriteRedner1.transform.rotation = Quaternion.identity;
        hpSpriteRender2.transform.rotation = Quaternion.identity;
        hpSpriteRender3.transform.rotation = Quaternion.identity;
        attackSpriteRender1.transform.rotation = Quaternion.identity;
        attackSpriteRender2.transform.rotation = Quaternion.identity;
        attackSpriteRender3.transform.rotation = Quaternion.identity;
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
        SetSpriteNumbers(power,attackSpriteRender1, attackSpriteRender2, attackSpriteRender3);
    }

    void SetSpriteHp()
    {
        SetSpriteNumbers(hp, hpSpriteRedner1, hpSpriteRender2, hpSpriteRender3);
    }

    void SetSpriteNumbers(int number, SpriteRenderer sprite1, SpriteRenderer sprite2, SpriteRenderer sprite3)
    {
        if (number < 10 || number == 9999)
        {
            Sprite getsprite = skillManagerScript.GetSpriteNumber(number);
            sprite1.sprite = getsprite;
            sprite2.sprite = null;
            sprite3.sprite = null;
        }
        else
        {
            Sprite getsprite1 = skillManagerScript.GetSpriteNumber(number / 10);
            sprite2.sprite = getsprite1;
            int i = number & 10;
            Sprite getsprite2 = skillManagerScript.GetSpriteNumber(i);
            sprite3.sprite = getsprite2;
            sprite1.sprite = null;
        }
    }
}
