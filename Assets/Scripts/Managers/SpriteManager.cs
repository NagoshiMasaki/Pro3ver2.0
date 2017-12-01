using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    [SerializeField]
    List<Sprite> numberList;
    [SerializeField]
    List<Sprite> spList;

    public Sprite GetSpList(int num)
    {
        return spList[num];
    }

    public Sprite GetNumberList(int num)
    {
        return numberList[num];
    }
}
