using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustrationStatus : MonoBehaviour
{
    [SerializeField]
    int playerNumber;
    [SerializeField]
    int dictionaryNumber;
    [SerializeField]
    int rateNumber;
    [SerializeField]
    int costNumber;
    [SerializeField]
    MoveData.Rate rate;
    [SerializeField]
    float zoomValue;
    [SerializeField]
    Vector3 defaultScale;
    [SerializeField]
    Sprite infomationSprite;
    DeckHandManager deckHandManagerScript;
    [SerializeField]
    int hpNumber;
    [SerializeField]
    int attackNumber;
    [SerializeField]
    SpriteRenderer hpNumberSprite1;
    [SerializeField]
    SpriteRenderer hpNumberSprite2;
    [SerializeField]
    SpriteRenderer hpNumberSprite3;
    [SerializeField]
    SpriteRenderer attackNumberSprite1;
    [SerializeField]
    SpriteRenderer attackNumberSprite2;
    [SerializeField]
    SpriteRenderer attackNumberSprite3;
    [SerializeField]
    GameObject iconParentObj;
    Sprite defaultSprite;
    int instanceID;

    public int InstanceID { get { return instanceID; } set { instanceID = value; } }

    public GameObject GetIconParentObj()
    {
        return iconParentObj;
    }

    public void Ini(DeckHandManager set)
    {
        instanceID = InstanceId.instanceid;
        InstanceId.instanceid++;
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        deckHandManagerScript = set;
        SetHpNumberSprite();
        SetAttackNumberSprite();
    }

    public Sprite GetDefaultSprite()
    {
        return defaultSprite;
    }
    void SetHpNumberSprite()
    {
        SetSpriteNumbers(hpNumber,hpNumberSprite1, hpNumberSprite2, hpNumberSprite3);
    }

    void SetSpriteNumbers(int number, SpriteRenderer sprite1, SpriteRenderer sprite2, SpriteRenderer sprite3)
    {
        if (number < 10)
        {
            Sprite getsprite = deckHandManagerScript.GetNumber(number);
            sprite1.sprite = getsprite;
            sprite2.sprite = null;
            sprite3.sprite = null;
        }
        else
        {
            Sprite getsprite1 = deckHandManagerScript.GetNumber(number / 10);
            sprite2.sprite = getsprite1;
            int i = number & 10;
            Sprite getsprite2 = deckHandManagerScript.GetNumber(i);
            sprite3.sprite = getsprite2;
            sprite1.sprite = null;
        }
    }

    void SetAttackNumberSprite()
    {
        SetSpriteNumbers(attackNumber,attackNumberSprite1, attackNumberSprite2, attackNumberSprite3);
    }

    public void SetDefaultScale( Vector3 set)
    {
        defaultScale = set;
    }

    public Sprite GetInfomationSprite()
    {
        return infomationSprite;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public void ZoomUp()
    {
        Vector3 zoom = transform.localScale;
        zoom.x *= zoomValue;
        zoom.y *= zoomValue;
        transform.localScale = zoom;
    }

    public void ResetScale()
    {
        transform.localScale = defaultScale;
    }

    public int GetDictionaryNumber()
    {
        return dictionaryNumber;
    }

    public int GetRateNum()
    {
        return rateNumber;
    }

    public void GetRate_Dictionary_Cost_Player_Number(ref int ratenum, ref int dictionarynum, ref int costnum, ref int playernum)
    {
        ratenum = rateNumber;
        dictionarynum = dictionaryNumber;
        costnum = costNumber;
        playernum = playerNumber;
    }

    public void SetPlayerNumber(int num)
    {
        playerNumber = num;
    }

    public int GetCost()
    {
        return costNumber;
    }

    public MoveData.Rate GetRate()
    {
        return rate;
    }

    public void IniSendDataSetting()
    {
        SocketGameStatus.inidata += instanceID.ToString() + "," + dictionaryNumber.ToString() + "/";
    }
}
