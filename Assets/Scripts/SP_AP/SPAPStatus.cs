using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPAPStatus : MonoBehaviour
{
    [SerializeField]
    GameObject leverObj;
    [SerializeField]
    int maxSP;
    [SerializeField]
    List<SpSprite> spList = new List<SpSprite>();
    [SerializeField]
    int iniSP;
    [SerializeField]
    SPAPAction spapActionScript;
    [SerializeField]
    int SP;
    [SerializeField]
    int AP;
    int copySP;
    [SerializeField]
    GameObject SPObj;
    [SerializeField]
    GameObject APObj;
    [SerializeField]
    GameObject SPPos;
    [SerializeField]
    GameObject APPos;
    Vector3 defaultAPPos;
    Vector3 defaultSPPos;
    [SerializeField]
    SpriteRenderer SPNumber;
    SPAPManager spapManagerScript;

    public void Ini(SPAPManager set)
    {
        spapManagerScript = set;
        copySP = SP;
    }

    public int GetIniSp()
    {
        return iniSP;
    }
    public void spListAdd(SpSprite set)
    {
        spList.Add(set);
    }
    public GameObject GetLeverObj()
    {
        return leverObj;
    }

    public int GetMaxSP()
    {
        return maxSP;
    }

    public int GetSP()
    {
        return SP;
    }

    public void SetSP(int set,int usecount)
    {
        spapActionScript.UpdateSPSprite(usecount);
        SP = set;
        SPNumber.sprite = spapManagerScript.GetNumberSprte(SP);
    }

    public void SetSP()
    {
        SPNumber.sprite = spapManagerScript.GetNumberSprte(SP);
    }

    public GameObject GetAPObj()
    {
        return APObj;
    }

    public GameObject GetSPObj()
    {
        return SPObj;
    }

    public List<SpSprite> GetSPSpriteList()
    {
        return spList;
    }

    public void SetAP(int set)
    {
        AP = set;
    }

    public int GetAP()
    {
        return AP;
    }

    public void AddSP()
    {
        if (maxSP >= copySP + 1)
        {
            SP = copySP;
            SP++;
            copySP = SP;
        }

        else
        {
            SP = maxSP;
        }

        if (spapActionScript != null)
        {
            SPNumber.sprite = spapManagerScript.GetNumberSprte(SP);
            spapActionScript.ResetSP();
        }
    }

}
