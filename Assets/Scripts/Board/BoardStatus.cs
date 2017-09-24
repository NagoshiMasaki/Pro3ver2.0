using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardStatus : MonoBehaviour {

    [SerializeField]
    BoardManager boardManagerScript;
    GameObject[,] mathObjects = new GameObject[6, 6];
    [SerializeField]
    int lengthSize;
    [SerializeField]
    int sideSize;
    public GameObject GetMath(int length,int side)
    {
        return mathObjects[side, length];
    }

    public bool GetIsGamePlay()
    {
        return boardManagerScript.GetIsGamePlay();
    }

    public int GetSideSize()
    {
        return sideSize;
    }

    public int GetLengthSize()
    {
        return lengthSize;
    }

    public GameObject GetInstanceMathObj()
    {
        return boardManagerScript.GetInstanceMathObj();
    }

    public void SetMathObjects(int length,int size,GameObject setobj)
    {
        mathObjects[length, size] = setobj;
    }

}
