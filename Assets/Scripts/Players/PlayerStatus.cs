﻿///////////////////////////////////
//制作者　名越大樹
//クラス　ユーザが操作さる情報を管理するクラス
///////////////////////////////////

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
    GameObject IlustCard;
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
    [SerializeField]
    MassStatus copyMass;
    [SerializeField]
    GameObject attachiIlustCard;
    int sp;
    List<MassStatus> moveList = new List<MassStatus>();
    MassStatus lockOnAttachMass;
    MassStatus copyLockOnAttachMass = null;

    public void SetLockOnAttachMass(MassStatus set)
    {
        copyLockOnAttachMass = lockOnAttachMass;
        lockOnAttachMass = set;
    }

    public MassStatus GetLockOnAttachMass()
    {
        return lockOnAttachMass;
    }

    public MassStatus GetCopyLockOnAttachMass()
    {
        return copyLockOnAttachMass;
    }

    public void AddmoveList(MassStatus set)
    {
        moveList.Add(set);
    }
    public bool GetIsGamePlay()
    {
        return playerManagerScript.GetIsGamePlay();
    }
    public int GetSP()
    {
        return sp;
    }
    public void SetSP(int set)
    {
        sp = set;
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
        if(attachMass == null)
        {
            attachMass = set;
        }
        if (set.GetComponent<MassStatus>() != attachMass.GetComponent<MassStatus>())
        {
            copyMass = set.GetComponent<MassStatus>();
        }
    }

    public GameObject GetAttachMass()
    {
        return attachMass;
    }

    public LayerMask GetNormalIconLayer()
    {
        return normalIconLayer;
    }

    public void AllAttachNull()
    {
        attachMass = null;
        attachSumonCard = null;
        attachiIlustCard = null;
        copyMass = null;
        lockOnAttachMass = null;
        copyLockOnAttachMass = null;
    }
}
