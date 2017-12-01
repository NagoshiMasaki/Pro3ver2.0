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

    void Start()
    {
        defaultScale = transform.localScale;
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
}
