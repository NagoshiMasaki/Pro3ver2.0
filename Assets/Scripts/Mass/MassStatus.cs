using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassStatus : MonoBehaviour
{

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
    [SerializeField]
    int materialNumber;
    int defaultNumber;
    [SerializeField]
    bool isMove = false;
    BoardManager.MassMoveStatus status = BoardManager.MassMoveStatus.Not;
    public void SetNumber(int length, int side, int number, int materialnum)
    {
        lengthNumber = length;
        sideNumber = side;
        massNumber = number;
        materialNumber = materialnum;
        defaultNumber = materialnum;
        GetComponent<Renderer>().material = playermaterial[materialNumber];
        status = BoardManager.MassMoveStatus.None;
    }

    public void GetNumbers(ref int length, ref int side, ref int number)
    {
        length = lengthNumber;
        side = sideNumber;
        number = massNumber;
    }
    public int GetSideNumber()
    {
        return sideNumber;
    }

    public int GetLengthNumber()
    {
        return lengthNumber;
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
        materialNumber = number;
        GetComponent<Renderer>().material = playermaterial[number];
    }
    public int GetDefaultNumber()
    {
        return defaultNumber;
    }
    public int GetMaterialNumber()
    {
        return materialNumber;
    }

    public bool GetIsMove()
    {
        return isMove;
    }

    public void SetIsMove(bool set)
    {
        isMove = set;
    }

    public void SetMassStatus(BoardManager.MassMoveStatus set)
    {
        status = set;
    }

    public BoardManager.MassMoveStatus GetMoveStatus()
    {
        return status;
    }
}
