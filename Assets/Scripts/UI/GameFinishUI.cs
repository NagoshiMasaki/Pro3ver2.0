////////////////////////////////////////
//製作者　名越大樹
//ゲームの勝利判定を表示させるクラス
////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
public class GameFinishUI : MonoBehaviour {

    [SerializeField]
    Text gameFinishText;
    [SerializeField]
    float waitTimer;
    private void Start()
    {
        enabled = false;
    }
    public void GameFinish(int winnumber)
    {
        string message = "勝者" + winnumber.ToString() + "Pです";
        gameFinishText.text = message;
        enabled = true;
    }

    void Update()
    {
        waitTimer -= Time.deltaTime;
        if(waitTimer <= 0.0f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
    }
}
