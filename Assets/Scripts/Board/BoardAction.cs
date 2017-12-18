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
    [SerializeField]
    float massSpaceX;
    [SerializeField]
    float massSpaceY;
    [SerializeField]
    GameObject instancePos;
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
        GameObject[] mathobjectarray = boardStatusScript.GetInstanceMathObj();
        int lengthsize = boardStatusScript.GetLengthSize();
        int sidesize = boardStatusScript.GetSideSize();
        Vector3 pos = instancePos.transform.position;
        int number = 0;
        bool isindex = false;
        for (int length = 0; length < lengthsize; length++)
        {
            for (int side = 0; side < sidesize; side++)
            {
                int materialnum = 0;
                int index = 0;
                if(length < 2)
                {
                    materialnum = 1;
                }
                else if(length >= 4)
                {
                    materialnum = 2;
                }
                if (isindex)
                {
                    if (number % 2 == 0)
                    {
                        index = 1;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                else
                {
                    if (number % 2 == 0)
                    {
                        index = 0;
                    }
                    else
                    {
                        index = 1;
                    }
                }
                GameObject instanceobj = Instantiate(mathobjectarray[index], pos, Quaternion.identity);
                instanceobj.GetComponent<MassStatus>().SetNumber(length,side,number,materialnum);
                boardStatusScript.SetMathObjects(length,side,instanceobj);
                pos.x+= massSpaceX;
                number++;
            }
            isindex = !isindex;
            pos.y += massSpaceY;
            pos.x = instancePos.transform.position.x;
        }
        boardManagerScript.DoneBoardMass();
    }

    public void ClearColorUpdateMoveAreaList()
    {
        List<MassStatus> masslist = boardStatusScript.GetUpdateMoveAreaList();
        for (int count = 0; count < masslist.Count; count++)
        {
            masslist[count].SetDefaultMaterial();
        }
    }


    public void ClearColorSummonList()
    {
       List<MassStatus> masslist = boardStatusScript.GetSummonMassList();
        for (int count = 0;count < masslist.Count;count++)
        {
            masslist[count].SetDefaultMaterial();
        }
    }
}
