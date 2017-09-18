using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassStatus : MonoBehaviour {

    [SerializeField]
    int lengthNumber;
    [SerializeField]
    int sideNumber;
    [SerializeField]
    int massNumber;
    [SerializeField]
    GameObject characterobj;
    [SerializeField]
    Material[] playermaterial;
    int materialNumber;
    public void SetNumber(int length,int side,int number,int material)
    {
        lengthNumber = length;
        sideNumber = side;
        massNumber = number;
        materialNumber = material;
        GetComponent<Renderer>().material = playermaterial[materialNumber];
    }

    public void GetNumbers(ref int length,ref int side,ref int number)
    {
        length = lengthNumber;
        side = sideNumber;
        number = massNumber;
    }

    public void SetCharacterObj(GameObject obj)
    {
        characterobj = obj;
    }

    public GameObject GetCharacterObj()
    {
        return characterobj;
    }

    public void SetMaterial(int number)
    {
        GetComponent<Renderer>().material = playermaterial[number];
    }

    public int GetMaterialNumber()
    {
        return materialNumber;
    }
}
