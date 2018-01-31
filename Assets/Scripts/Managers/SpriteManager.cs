using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    [SerializeField]
    List<Sprite> numberList;
    [SerializeField]
    List<Sprite> spList;
    [SerializeField]
    List<Sprite> turnSpriteList;

    public Sprite GetTurnSprite(int number)
    {
        return turnSpriteList[number];
    }

    public Sprite GetSpList(int num)
    {
        return spList[num];
    }

    public Sprite GetNumberList(int num)
    {
        if(num > 100)
        {
            num = 11;
        }
        return numberList[num];
    }
}
