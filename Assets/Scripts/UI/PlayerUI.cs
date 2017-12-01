using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{

    [SerializeField]
    Text playerText;
    [SerializeField]
    GameObject Pos1;
    [SerializeField]
    GameObject Pos2;
    public void ChangerTurn(int num)
    {
        string turn = "」ターン";
        playerText.text = "プレイヤー「" + num.ToString() + turn;
        switch (num)
        {
            case 1:

                playerText.rectTransform.transform.position = Pos1.transform.position;
                break;
            case 2:
                playerText.rectTransform.transform.position = Pos2.transform.position;
                break;
        }
    }
}
