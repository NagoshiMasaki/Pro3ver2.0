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

    public void SetSPAPManager(SPAPManager set)
    {
        spapManagerScript = set;
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
        SPNumber.sprite = spapManagerScript.GetNumberSprte(SP);
        SP = set;
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
        if (maxSP >= SP + 1)
        {
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

    /*
    public void AddSP()
    {
        SP = copySP;
        SP++;
        copySP = SP;
        ResetSpInstance();
    }

    public void SetSP(int set)
    {
        SP = set;
        ResetSpInstance();
    }

    public void AddAP()
    {
        AP++;
    }

    public int GetAP()
    {
        return AP;
    }

    public int GetSP()
    {
        return SP;
    }



    public void InstanceAP()
    {
        GameObject instanceobj = Instantiate(APObj, APPos.transform.position, Quaternion.identity);
        APList.Add(instanceobj);
        MoveAP();
    }

    public void InstanceSP()
    {
        GameObject instanceobj = Instantiate(SPObj, SPPos.transform.position, Quaternion.identity);
        SPList.Add(instanceobj);
        MoveSP();
    }

    void MoveAP()
    {
        Vector3 pos = APPos.transform.position;
        pos.x++;
        APPos.transform.position = pos;
    }

    void MoveSP()
    {
        Vector3 pos = SPPos.transform.position;
        pos.x++;
        SPPos.transform.position = pos;
    }

    void ResetAPPos()
    {
        APPos.transform.position = defaultAPPos;
    }

    void ResetSPPos()
    {
        SPPos.transform.position = defaultSPPos;
    }

    public void AllDestroyAP()
    {
        for (int count = 0; count < APList.Count; count++)
        {
            Destroy(APList[count]);
        }
        APList.Clear();
    }

    public void AllDestroySP()
    {
        for (int count = 0; count < SPList.Count; count++)
        {
            Destroy(SPList[count]);
        }
        SPList.Clear();
    }

    public void SubtractionAP(int num)
    {
        for (int count = num; count > 0; count--)
        {
            Destroy(APList[APList.Count - 1]);
            APList.RemoveAt(APList.Count);
            Vector3 pos = APPos.transform.position;
            pos.x--;
            APPos.transform.position = pos;
        }
    }

    public void SubtractionSP(int num)
    {
        for (int count = num; count > 0; count--)
        {
            Destroy(SPList[SPList.Count - 1]);
            SPList.RemoveAt(SPList.Count);
            Vector3 pos = SPPos.transform.position;
            pos.x--;
            SPPos.transform.position = pos;
        }
    }

    public void IniInstanceSP()
    {
        for (int count = 0; count < SP; count++)
        {
            InstanceSP();
        }
    }

    public void ResetApInstance()
    {
        AllDestroyAP();
        ResetAPPos();
        for (int count = 0; count < AP; count++)
        {
            InstanceAP();
        }
    }

    public void ResetSpInstance()
    {
        AllDestroySP();
        ResetSPPos();
        for (int count = 0; count < SP; count++)
        {
            InstanceSP();
        }
    }

    public void Ini()
    {
        InstanceSP();
    }
    public void SetAP(int set)
    {
        AP = set;
    }
    */
}
