using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckClass : MonoBehaviour {

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

    public void SetCharacter(int num)
    {
      GameObject character = dictopnaryManagerScript.GetCharacter(num);
      characterList.Add(character);
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
            deckHandScript.SetDrawObj(characterList[0]);
            characterList.RemoveAt(0);
        }
    }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }
}
