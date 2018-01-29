////////////////////////////////////////
//製作者　名越大樹
//ゲームの勝利判定を表示させるクラス
////////////////////////////////////////

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
