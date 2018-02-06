using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketManager : MonoBehaviour
{
    [SerializeField]
    SocketGameStatus socketGameStatusScript;

    public void AddStaus(SocketAction.GameStatus set)
    {
        socketGameStatusScript.GameStatusList.Add(set);
    }
}
