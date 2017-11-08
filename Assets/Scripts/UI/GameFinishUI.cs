using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameFinishUI : MonoBehaviour {

    [SerializeField]
    Text gameFinishText;
    public void GameFinish(int winnumber)
    {
        string message = "勝者" + winnumber.ToString() + "Pです";
        gameFinishText.text = message;
    }
}
