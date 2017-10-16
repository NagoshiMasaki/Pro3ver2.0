using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryManager : MonoBehaviour {

    [SerializeField]
    GameObject[] illustCharacterArray;
    [SerializeField]
    GameObject[] summonCharacterArray;
    public GameObject GetIllustCharacter(int num)
    {
        return illustCharacterArray[num];
    }

    public GameObject GetSummonCharacter(int num)
    {
        return summonCharacterArray[num];
    }
}
