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
            case 2:
                return decxHand2Script.GetPos();
        }
        return Vector3.zero;
    }

    public void InstanceDrawCard(int num,GameObject drawobj)
    {
        switch (num)
        {
            case 1:
                decxHand1Script.SetDrawObj(drawobj);
                break;
            case 2:
                decxHand2Script.SetDrawObj(drawobj);
                break;
        }
    }

    public void RemoveIllustCard(int num,GameObject target)
    {
        switch (num)
        {
            case 1:
                decxHand1Script.RemoveIllustCard(target);
                break;
            case 2:
                decxHand2Script.RemoveIllustCard(target);
                break;
        }
    }

}
