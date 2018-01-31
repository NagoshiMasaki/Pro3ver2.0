/////////////////
//制作者　名越大樹
//クラス名　手札を管理するクラス
/////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHand : MonoBehaviour {

    [SerializeField]
    List<GameObject> deckHandList;
    [SerializeField]
    GameObject deckHandPos;
    [SerializeField]
    int playerNumber;
    [SerializeField]
    DeckHandManager deckHandManagerScript;
    Vector3 defaultPos;
    [SerializeField]
    float spaceX;
    [SerializeField]
    Vector3 illsutlationScale;
    [SerializeField]
    GameObject instanceDrawPosObj;
    bool isIniDraw = false;

    public void GameFinish(int num)
    {
        deckHandManagerScript.GameFinish(num);
    }

    public void SetIsIniDraw(bool set)
    {
        isIniDraw = set;
    }

    public void Ini()
    {
        defaultPos = deckHandPos.transform.position;
    }

    public void IniDeckHand(List<GameObject> inilist)
    {
        deckHandList = inilist;
    }

    public void SetDeckHand(GameObject set)
    {
        deckHandList.Add(set);
    }

    public void RemoveDeckHand(GameObject obj)
    {
        for (int count = 0; count < deckHandList.Count; count++)
        {
            if (obj == deckHandList[count].gameObject)
            {
                Destroy(deckHandList[count]);
                deckHandList.RemoveAt(count);
            }
        }
    }

    public void RemoveIllustCard(GameObject obj)
    {
        RemoveDeckHand(obj);
        ResetPos();
    }

    public void SetDrawObj(GameObject obj)
    {
        InstanceDraw(obj);
    }

    void InstanceDraw(GameObject obj)
    {
        Vector3 pos = deckHandManagerScript.GetInstancePos(playerNumber);
        Vector3 copypos = pos;
        GameObject instsnceobj = null;
        if (isIniDraw)
        {
            Debug.Log(obj);
            instsnceobj = Instantiate(obj, instanceDrawPosObj.transform.position, Quaternion.identity);//カードの生成
        }
        else
        {
            instsnceobj = Instantiate(obj,pos, Quaternion.identity);//カードの生成
        }
        instsnceobj.GetComponent<IllustrationStatus>().Ini(deckHandManagerScript);
        instsnceobj.GetComponent<IllustrationStatus>().SetPlayerNumber(playerNumber);
        instsnceobj.GetComponent<IllustrationStatus>().SetDefaultScale(illsutlationScale);
        if (playerNumber == 2)
        {
            instsnceobj.GetComponent<SpriteRenderer>().sprite = deckHandManagerScript.BackIllustlation();
        }
        deckHandList.Add(instsnceobj);
        MoveDeckHandPos();
        if (isIniDraw)
        {
            deckHandManagerScript.DrawcardAnimation(instsnceobj,copypos,instanceDrawPosObj,this);
        }
        else
        {
            instsnceobj.GetComponent<IllustrationStatus>().ResetScale();
        }
    }

    void MoveDeckHandPos()
    {
        Vector3 pos = deckHandPos.transform.position;
        pos.x+= spaceX;
        deckHandPos.transform.position = pos;
    }

    public void ResetPos()
    {
        deckHandPos.transform.position = defaultPos;
        for(int count = 0;count < deckHandList.Count;count++)
        {
            deckHandList[count].transform.position = deckHandPos.transform.position;
            MoveDeckHandPos();
        }
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public Vector3 GetPos()
    {
        return deckHandPos.transform.position;
    }

    public void SetPos(Vector3 pos)
    {
        deckHandPos.transform.position = pos;
    }


}
