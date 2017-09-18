using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    [SerializeField]
    PlayerManager playerManagerScript;
    [SerializeField]
    LayerMask MassLayer;
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
        return MassLayer;
    }
}
