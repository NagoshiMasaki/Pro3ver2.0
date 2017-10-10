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

    public void GetSP(int playernum)
    {
        switch (playernum)
        {
            case 1:
                spapStatus1.GetSP();
                break;
        }
    }

    public void IniInstanceSp(int playernum)
    {
        Debug.Log("hoge");
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
