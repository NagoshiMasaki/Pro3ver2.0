using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    [SerializeField]
    PlayerManager playerManagerScript;
    [SerializeField]
    LayerMask massLayer;
    [SerializeField]
    LayerMask deckLayer;
    [SerializeField]
    LayerMask illustrationLayer;
    [SerializeField]
    int iniDeckHandCount;
    [SerializeField]
    GameObject attachiIlustCard;
    [SerializeField]
    GameObject attachSumonCard;
    public bool GetIsGamePlay()
    {
        return playerManagerScript.GetIsGamePlay();
    }

    public int GetPlayerTurn()
    {
        return playerManagerScript.GetPlayerTurn();
    }

    public LayerMask GetMassLayer()
    {
        return massLayer;
    }

    public LayerMask GetDeckLayer()
    {
        return deckLayer;
    }

    public void SetPhase(SituationManager.Phase set)
    {
        playerManagerScript.SetPhase(set);
    }

    public SituationManager.Phase GetPhase()
    {
        return playerManagerScript.GetPhase();
    }
    public GameObject GetDrawObj(int number)
    {
        return playerManagerScript.GetDraw(number);
    }

    public int GetIniDeckHandCount()
    {
        return iniDeckHandCount;
    }

    public LayerMask GetIllustrationLayer()
    {
        return illustrationLayer;
    }

    public GameObject GetAttachIllustCard()
    {
        return attachiIlustCard;
    }

    public void SetAttachIllustCard(GameObject set)
    {
        attachiIlustCard = set;
    }

    public GameObject GetAttachSumonCard()
    {
        return attachSumonCard;
    }
    public void SetAttachSumonCard(GameObject set)
    {
        attachSumonCard = set;
    }

    public void RemoveDeckHand(GameObject target)
    {

    }
}
