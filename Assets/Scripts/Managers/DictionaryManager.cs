using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryManager : MonoBehaviour {

    [SerializeField]
    GameObject[] CharacterArray;
    
    public GameObject GetCharacter(int num)
    {
        return CharacterArray[num];
    }
}
