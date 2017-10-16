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

    void Start()
    {
        IniInstanceSp(1);
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

    public void IniInstanceSp(int playernum)
    {
        switch (playernum)
        {
            case 1:
                spapStatus1.IniInstanceSP();
                break;
        }
    }

    public GameObject GetApObj(int playernum)
    {
        return apObj;
    }

    public GameObject GetSpObj()
    {
        return spObj;
    }
}
