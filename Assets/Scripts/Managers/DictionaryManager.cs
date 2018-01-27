using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryManager : MonoBehaviour {

    [SerializeField]
    GameObject[] illustCharacterArray;
    [SerializeField]
    GameObject[] summonCharacterArray;
    [SerializeField]
    EffectAnimationBase[] effectObjectArray;


    public EffectAnimationBase GetEffectObject(int num)
    {
        return effectObjectArray[num];
    }

    public GameObject GetIllustCharacter(int num)
    {
        return illustCharacterArray[num];
    }

    public GameObject GetSummonCharacter(int num)
    {
        return summonCharacterArray[num];
    }
}
