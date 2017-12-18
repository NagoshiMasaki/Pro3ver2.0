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
    [SerializeField]
    BoardManager.MassMoveStatus status = BoardManager.MassMoveStatus.Not;

    [SerializeField]
    SpriteRenderer sprite;
    public void SetNumber(int length, int side, int number, int materialnum)
    {
        lengthNumber = length;
        sideNumber = side;
        massNumber = number;
        materialNumber = materialnum;
        defaultNumber = materialnum;
        status = BoardManager.MassMoveStatus.None;
        SetMaterial(materialnum);
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
        switch (number)
        {
            case 0:
                sprite.color = Color.white;
                break;
            case 1:
                sprite.color = Color.red;
                break;
            case 2:
                sprite.color = Color.blue;
                break;
            case 3:
                sprite.color = Color.yellow;
                break;
            case 4:
                sprite.color = Color.green;
                break;
        }
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

    public void SetDefaultMaterial()
    {
        SetMaterial(defaultNumber);
    }
}
