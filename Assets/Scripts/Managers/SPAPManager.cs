using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPAPManager : MonoBehaviour {

    [SerializeField]
    SPAPStatus spapStatus1;
    [SerializeField]
    SPAPStatus spapStatus2;
    [SerializeField]
    GameObject apObj;
    [SerializeField]
    GameObject spObj;


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

    public void SetSP(int playernum,int set)
    {
        switch (playernum)
        {
            case 1:
                spapStatus1.SetSP(set);
                break;
            case 2:
                spapStatus2.SetSP(set);
                break;
        }
    }

    public void IniInstanceSp(int playernum)
    {
        switch (playernum)
        {
            case 1:
                spapStatus1.IniInstanceSP();
                break;
            case 2:
                spapStatus2.IniInstanceSP();
                break;
        }
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
    public void SetAP(int playernum,int set)
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

    public GameObject GetSpObj()
    {
        return spObj;
    }
}
