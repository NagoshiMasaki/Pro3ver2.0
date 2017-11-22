///////////////////////////////////////////////////
//製作者　名越大樹
//クラス　ボード上の状態を管理するクラス
///////////////////////////////////////////////////

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

    public List<GameObject> GetInstancePos(int playernum, IllustrationStatus status)
    {
        dataList.Clear();
        MoveData.Rate rate = status.GetRate();
        switch (rate)
        {
            case MoveData.Rate.FirstPorn:
            case MoveData.Rate.Porn:
                SumonPorn(playernum, status);
                break;
            case MoveData.Rate.Queen:
                SumonQueen(playernum, status);
                break;
            case MoveData.Rate.Night:
            case MoveData.Rate.Luke:
            case MoveData.Rate.Bishop:
                SumonOther(playernum, status);
                break;
        }
        return dataList;
    }

    void DataListAdd(int length, int side, int playernum)
    {
        int number = massStatuses[length, side].GetMaterialNumber();
        if (playernum == number && massStatuses[length, side].GetCharacterObj() == null)
        {
            dataList.Add(massObjects[length, side]);
        }
    }

    /// <summary>
    /// ポーンを召喚できる場所を探す処理
    /// </summary>
    /// <param name="playernum"></param>
    /// <param name="status"></param>
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

    /// <summary>
    /// クイーンを召喚する場所を探す処理
    /// </summary>
    /// <param name="playernum"></param>
    /// <param name="status"></param>
    void SumonQueen(int playernum, IllustrationStatus status)
    {
        if (status.GetPlayerNumber() == 1)
        {
            DataListAdd(0, 2, playernum);
        }
        else if (status.GetPlayerNumber() == 2)
        {
            DataListAdd(5, 3, playernum);
        }

    }

    /// <summary>
    /// それ以外の種族の召喚場所を生成する処理
    /// </summary>
    /// <param name="playernum"></param>
    /// <param name="status"></param>
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

    /// <summary>
    /// ボード上で動いた召喚キャラクターを追加する
    /// </summary>
    /// <param name="target"></param>
    public void AddMoveDataList(GameObject target)
    {
        bool result = CheckMoveDataList(target);
        if (result)
        {
            movedatalist.Add(target);
        }
    }

    /// <summary>
    /// 同じキャラクターが重複していなかいを確認する処理
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
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

    public void RemoveMoveDataList(GameObject target)
    {

        for (int count = 0; count < movedatalist.Count; count++)
        {
            if (movedatalist[count] == target)
            {
                movedatalist.RemoveAt(count);
            }
        }

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

    public List<MassStatus> GetSearchMassAround(int length, int side)
    {
        List<MassStatus> masslist = new List<MassStatus>();
        for (int lengthcount = -1; lengthcount <= 1; lengthcount++)
        {
            for (int sidecount = -1; sidecount <= 1; sidecount++)
            {
                int lengthsum = lengthcount + length;
                int sidesum = side + sidecount;
                bool result = OutSideLength(lengthsum, sidesum);
                if (result)
                {
                    masslist.Add(massStatuses[lengthsum, sidesum]);
                }
            }
        }
        return masslist;
    }

    /// <summary>
    /// 縦と横のマスの範囲外ではないかをチェックする処理
    /// </summary>
    bool OutSideLength(int length, int side)
    {
        if (length >= lengthSize || length < 0)
        {
            return false;
        }

        else if (side >= sideSize || side < 0)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 自分の属しているマスを探す処理
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public List<MassStatus> SearchPlayerMass(int player)
    {
        int startlength = 0;
        int endlength = 0;
        int value = 0;
        switch (player)
        {
            case 1:
                value = 1;
                break;
            case 2:
                value = -1;
                startlength = lengthSize - 1;
                endlength = 2;
                break;
        }

        List<MassStatus> masslist = new List<MassStatus>();
        for (int lengthcount = startlength; lengthcount != endlength; lengthcount += value)
        {
            for (int sidecount = 0; sidecount < sideSize; sidecount++)
            {
                if (massStatuses[lengthcount, sidecount].GetMaterialNumber() == player)
                {
                    masslist.Add(massStatuses[lengthcount, sidecount]);
                }
            }
        }
        return masslist;
    }

    public void AllMyArea(int playernumber)
    {
        int startlength = 2;
        int endlength = 4;
        for (int countlength = startlength; countlength < endlength; countlength++)
        {
            for (int countside = 0; countside < sideSize; countside++)
            {
                massStatuses[countlength, countside].SetMaterial(playernumber);
            }
        }
    }

    public void AllDefaultArea()
    {
        int startlength = 2;
        for (int countlength = startlength; countlength < lengthSize; countlength++)
        {
            for (int countside = 0; countside < sideSize; countside++)
            {
                massStatuses[countlength, countside].SetMaterial(massStatuses[countlength,countside].GetDefaultNumber());
            }
        }
    }
}
