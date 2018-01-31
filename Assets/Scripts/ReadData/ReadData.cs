
using UnityEngine;

public class ReadData : MonoBehaviour
{
    [SerializeField]
    BgmSeManager bgmSeManagerScript;
    [SerializeField]
    int bgmNumber;
    public void Ini()
    {
        bgmSeManagerScript.BgmPlay(bgmNumber);
        Destroy(this);
    }
}
