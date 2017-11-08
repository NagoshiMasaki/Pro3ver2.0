using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckClass : MonoBehaviour
{

    [SerializeField]
    DictionaryManager dictopnaryManagerScript;
    [SerializeField]
    List<GameObject> characterList = new List<GameObject>();
    [SerializeField]
    int playerNumber;
    [SerializeField]
    DeckHand deckHandScript;
    [SerializeField]
    PlayerStatus playerStatusScript;
    [SerializeField]
    BoardManager boardManagerScritpt;
    public void SetCharacter(int num,int playernum)
    {
        GameObject character = dictopnaryManagerScript.GetIllustCharacter(num);
        IllustrationStatus status = character.GetComponent<IllustrationStatus>();
        if (status.GetRate() != MoveData.Rate.King)
        {
            characterList.Add(character);
            return;
        }
        SumonKing(playernum, status);
    }

    public GameObject GetCharacter()
    {
        GameObject character = characterList[0];
        characterList.RemoveAt(0);
        return character;
    }

    public void IniShaffle()
    {
        IniDeckDraw();
    }

    void IniDeckDraw()
    {
        int num = playerStatusScript.GetIniDeckHandCount();
        for (int count = 0; num >= count; count++)
        {
            deckHandScript.SetDrawObj(characterList[0]);//デッキの一番先頭のカード
            characterList.RemoveAt(0);//デッキの一番最初のカードを削除
        }
    }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    void SumonKing(int playernum,IllustrationStatus status)
    {
        GameObject sumonobj = dictopnaryManagerScript.GetSummonCharacter(status.GetDictionaryNumber());
        GameObject mass = null;
        MassStatus massstatus;
        switch (playernum)
        {
            case 1:
                mass = boardManagerScritpt.GetMassObject(0, 3);
                massstatus = mass.GetComponent<MassStatus>();
                break;
            case 2:
                mass = boardManagerScritpt.GetMassObject(2, 2);
                massstatus = mass.GetComponent<MassStatus>();
                break;
        }
        Vector3 instancepos = mass.transform.position;
        massstatus = mass.GetComponent<MassStatus>();
        instancepos.z--;
        GameObject instance = Instantiate(sumonobj, instancepos, Quaternion.identity);
        instance.GetComponent<SummonStatus>().SetPlayerNumber(playernum);
        massstatus.SetMassStatus(BoardManager.MassMoveStatus.Not);
        massstatus.SetCharacterObj(instance);
    }
}
