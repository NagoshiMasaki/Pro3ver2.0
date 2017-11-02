using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAction : MonoBehaviour
{

    [SerializeField]
    BoardStatus boardStatusScript;
    bool isIni = true;
    [SerializeField]
    BoardManager boardManagerScript;
    void Start()
    {
        Ini();
    }

    void Ini()
    {
        boardManagerScript.DeckHandIni();
        SetInstanceMathObjects();
    }
    public void SetInstanceMathObjects()
    {
        GameObject mathobject = boardStatusScript.GetInstanceMathObj();
        int lengthsize = boardStatusScript.GetLengthSize();
        int sidesize = boardStatusScript.GetSideSize();
        Vector3 pos = Vector3.zero;
        int number = 0;
        for (int length = 0; length < lengthsize; length++)
        {
            for (int side = 0; side < sidesize; side++)
            {
                int materialnum = 0;
                if(length < 2)
                {
                    materialnum = 1;
                }
                else if(length >= 4)
                {
                    materialnum = 2;
                }
                GameObject instanceobj = Instantiate(mathobject, pos, Quaternion.identity);
                instanceobj.GetComponent<MassStatus>().SetNumber(length,side,number,materialnum);
                boardStatusScript.SetMathObjects(length,side,instanceobj);
                pos.x++;
                number++;
            }
            pos.y++;
            pos.x = 0;
        }
        boardManagerScript.DoneBoardMass();
    }
}
