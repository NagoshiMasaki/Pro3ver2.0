using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandManager : MonoBehaviour
{

    [SerializeField]
    DeckHand decxHand1Script;
    [SerializeField]
    DeckHand decxHand2Script;

    public Vector3 GetInstancePos(int num)
    {
        switch (num)
        {
            case 1:
                return decxHand1Script.GetPos();
                break;
            case 2:
                return decxHand2Script.GetPos();
                break;
        }
        return Vector3.zero;
    }

}
