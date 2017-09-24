using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPAPManager : MonoBehaviour {

    [SerializeField]
    SPAPStatus spapStatus1;
    [SerializeField]
    SPAPStatus spapStatus2;

    public void GetSP(int playernum)
    {
        switch (playernum)
        {
            case 1:
                spapStatus1.GetSP();
                break;
        }

    }
}
