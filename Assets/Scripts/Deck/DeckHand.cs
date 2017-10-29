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

    void Start()
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
        GameObject instanceobj = Instantiate(obj,pos,Quaternion.identity);
        instanceobj.GetComponent<IllustrationStatus>().SetPlayerNumber(playerNumber);
        deckHandList.Add(instanceobj);
        MoveDeckHandPos();
    }

    void MoveDeckHandPos()
    {
        Vector3 pos = deckHandPos.transform.position;
        pos.x++;
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
