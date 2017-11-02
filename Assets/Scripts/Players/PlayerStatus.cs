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
    [SerializeField]
    LayerMask nextphaselayer;
    [SerializeField]
    LayerMask moveIconLayer;
    [SerializeField]
    LayerMask skillIconLayer;
    [SerializeField]
    GameObject attachMass;
    [SerializeField]
    LayerMask normalIconLayer;
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

    public LayerMask GetNextPhaseLayer()
    {
        return nextphaselayer;
    }

    public LayerMask GetMoveIconLayer()
    {
        return moveIconLayer;
    }

    public LayerMask GetSkillLayer()
    {
        return skillIconLayer;
    }

    public void SetAttachMass(GameObject set)
    {
        attachMass = set;
    }

    public GameObject GetAttachMass()
    {
        return attachMass;
    }

    public LayerMask GetNormalIconLayer()
    {
        return normalIconLayer;
    }
}
