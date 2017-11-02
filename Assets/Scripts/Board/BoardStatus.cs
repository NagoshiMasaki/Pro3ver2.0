using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardStatus : MonoBehaviour
{
    [SerializeField]
    MoveData moveDataScript;
    [SerializeField]
    BoardManager boardManagerScript;
    [SerializeField]
    GameObject[,] massObjects = new GameObject[6, 6];
    [SerializeField]
    MassStatus[,] massStatuses = new MassStatus[6, 6];
    [SerializeField]
    int lengthSize;
    [SerializeField]
    int sideSize;
    List<GameObject> dataList = new List<GameObject>();
    [SerializeField]
    List<GameObject> movedatalist = new List<GameObject>();
    [SerializeField]
    List<MassStatus> updateMoveAreaList = new List<MassStatus>();

    public GameObject GetMass(int length, int side)
    {
        return massObjects[length, side];
    }

    public MassStatus GetMassStatus(int length, int side)
    {
        return massStatuses[length, side];
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

    public void SetMathObjects(int length, int side, GameObject setobj)
    {
        massObjects[length, side] = setobj;
        massStatuses[length, side] = setobj.GetComponent<MassStatus>();
    }

    public List<GameObject> GetInstancePos(int playernum,IllustrationStatus status)
    {
        dataList.Clear();
        MoveData.Rate rate = status.GetRate();
        switch(rate)
        {
            case MoveData.Rate.FirstPorn:
            case MoveData.Rate.Porn:
                SumonPorn(playernum,status);
                break;
            case MoveData.Rate.Queen:
                SumonQueen(playernum,status);
                break;
            case MoveData.Rate.Night:
            case MoveData.Rate.Luke:
            case MoveData.Rate.Bishop:
                SumonOther(playernum,status);
                break;
        }
        return dataList;
    }

    void DataListAdd(int length,int side,int playernum)
    {
        int number = massStatuses[length, side].GetMaterialNumber();
        if (playernum == number && massStatuses[length, side].GetCharacterObj() == null)
        {
            dataList.Add(massObjects[length, side]);
        }
    }

    void SumonPorn(int playernum, IllustrationStatus status)
    {
        int length = 0;
        if (status.GetPlayerNumber() == 1)
        {
            length = 1;
        }
        else if (status.GetPlayerNumber() == 2)
        {
            length = 4;
        }
        for (int sidecount = 0; sidecount < 6; sidecount++)
        {
            DataListAdd(length, sidecount, playernum);
        }
    }

    void SumonQueen(int playernum, IllustrationStatus status)
    {
        if (status.GetPlayerNumber() == 1)
        {
            DataListAdd(0,2,playernum);
        }
        else if (status.GetPlayerNumber() == 2)
        {
            DataListAdd(5, 3, playernum);
        }

    }

    void SumonOther(int playernum, IllustrationStatus status)
    {
        int length = 0;
        if (status.GetPlayerNumber() == 1)
        {
            length = 0;
        }
        else if (status.GetPlayerNumber() == 2)
        {
            length = 5;
        }
        for (int sidecount = 0; sidecount < 6; sidecount++)
        {
            DataListAdd(length, sidecount, playernum);
        }
    }

    public void InstanceMovePos(MoveData.Rate rate, int playernum, int nowlengthmass, int nowsidemass)
    {
        moveDataScript.GetMoveData(rate, playernum, massObjects, massStatuses, nowlengthmass, nowsidemass, boardManagerScript.GetMovePosMassObj());
    }

    public void AddMoveDataList(GameObject target)
    {
       bool result = CheckMoveDataList(target);
        if (result)
        {
            movedatalist.Add(target);
        }
    }

    public bool CheckMoveDataList(GameObject target)
    {
        for (int count = 0; count < movedatalist.Count; count++)
        {
            if (movedatalist[count] == target)
            {
                return false;
            }
        }
        return true;
    }

    public void ClearMoveDataList()
    {
        movedatalist.Clear();
    }

    public void AddUpdateMoveAreaList(MassStatus mass)
    {
        updateMoveAreaList.Add(mass);
    }

    public void ClearUpdateMoveAreList()
    {
        for (int count = 0; count < updateMoveAreaList.Count; count++)
        {
            updateMoveAreaList[count].SetMassStatus(BoardManager.MassMoveStatus.Not);
        }
        updateMoveAreaList.Clear();
    }
}
