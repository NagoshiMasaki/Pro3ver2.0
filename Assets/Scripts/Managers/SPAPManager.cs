using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPAPManager : MonoBehaviour
{

    [SerializeField]
    SPAPStatus spapStatus1;
    [SerializeField]
    SPAPStatus spapStatus2;
    [SerializeField]
    GameObject apObj;
    [SerializeField]
    GameObject spObj;

    [SerializeField]
    SPAPAction spapapActionScriptP1;
    [SerializeField]
    SPAPAction spapapActionScriptP2;
    [SerializeField]
    GameObject instancePosP1;
    [SerializeField]
    GameObject instancePosP2;
    [SerializeField]
    GameObject symbolObj;
    [SerializeField]
    SpriteManager spriteManager;

    public Sprite GetSPSpriteList(int num)
    {
       return spriteManager.GetSpList(num);
    }

    public void Ini()
    {
        GameObject obj = null;
        for (int count = 0; count <= 2;count++)
        {
            Quaternion rot;
            switch (count) {

                case 1:
                    rot = Quaternion.identity;
                    obj = Instantiate(symbolObj,instancePosP1.transform.position,rot);
                    spapapActionScriptP1 = obj.GetComponent<SPAPAction>();
                    spapStatus1 = obj.GetComponent<SPAPStatus>();
                    spapapActionScriptP1.Ini(spapStatus1,1,this);
                    break;
                case 2:
                    rot = Quaternion.Euler(0,0,180);
                    obj = Instantiate(symbolObj, instancePosP2.transform.position, rot);
                    spapapActionScriptP2 = obj.GetComponent<SPAPAction>();
                    spapStatus2 = obj.GetComponent<SPAPStatus>();
                    spapapActionScriptP2.Ini(spapStatus2,2,this);
                    break;
            }
            
        }
    }

    public void SetisAnimation(int player,int usecount)
    {
        switch (player)
        {
            case 1:
                spapapActionScriptP1.SetIsAnimation(usecount);
                break;
            case 2:
                spapapActionScriptP2.SetIsAnimation(usecount);
                break;
        }
    }

    public void ResetisAnimation(int player, int usecount)
    {
        switch (player)
        {
            case 1:
                spapapActionScriptP1.ResetIsAnimation(usecount);
                break;
            case 2:
                spapapActionScriptP2.ResetIsAnimation(usecount);
                break;
        }
    }


    public int GetSP(int playernum)
    {
        switch (playernum)
        {
            case 1:
                return spapStatus1.GetSP();
            case 2:
                return spapStatus2.GetSP();
        }
        return 0;
    }

    public void SetSP(int playernum, int set,int usecount)
    {
        switch (playernum)
        {
            case 1:
                spapStatus1.SetSP(set, usecount);
                break;
            case 2:
                spapStatus2.SetSP(set, usecount);
                break;
        }
    }
    public void IniInstanceSp(int playernum)
    {

    }

    public GameObject GetApObj(int playernum)
    {
        return apObj;
    }

    public int GetAp(int playernum)
    {
        switch (playernum)
        {
            case 1:
                return spapStatus1.GetAP();
            case 2:
                return spapStatus2.GetAP();
        }
        return 0;
    }

    public void SetAP(int playernum, int set)
    {
        switch (playernum)
        {
            case 1:
                spapStatus1.SetAP(set);
                break;
            case 2:
                spapStatus2.SetAP(set);
                break;
        }
    }

    public void AddSP(int playernum)
    {
        switch (playernum)
        {
            case 1:
                spapStatus1.AddSP();
                break;
            case 2:
                spapStatus2.AddSP();
                break;
        }

    }

    public Sprite GetNumberSprte(int num)
    {
        return spriteManager.GetNumberList(num);
    }

    public GameObject GetSpObj()
    {
        return spObj;
    }
}
