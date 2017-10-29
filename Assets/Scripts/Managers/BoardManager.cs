using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    [SerializeField]
    MoveData movedataScript;
    [SerializeField]
    BoardStatus boardStatusScript;
    bool Ini = true;
    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    GameObject mathObject;
    [SerializeField]
    int playerturn;
    [SerializeField]
    List<GameObject> massList = new List<GameObject>();
    [SerializeField]
    GameObject[] movePosMassObj;
    public enum MassMoveStatus
    {
        None,
        Enemy,
        Not
    }
    MassMoveStatus status;

    public GameObject[] GetMovePosMassObj()
    {
        return movePosMassObj;
    }

    public bool GetIsGamePlay()
    {
        return gameMasterScript.GetIsGamePlay();
    }

    public GameObject GetInstanceMathObj()
    {
        return mathObject;
    }

    public List<GameObject> GetInstancePos(int playernum)
    {
        return boardStatusScript.GetInstancePos(playernum);
    }
    
    /// <summary>
    /// アタッチしたプレイヤーの移動できる場所の生成
    /// </summary>
    public void InstanceMoveData(MoveData.Rate rate, int playernum, int nowlengthmass, int nowsidemass)
    {
        boardStatusScript.InstanceMovePos(rate,playernum,nowlengthmass,nowsidemass);
    }

    public void AddMoveList(GameObject target)
    {
        boardStatusScript.AddMoveDataList(target);
    }

    public bool CheckMoveList(GameObject　target)
    {
        return boardStatusScript.CheckMoveDataList(target);
    }

    public void AddUpdateMoveList(MassStatus status)
    {
        boardStatusScript.AddUpdateMoveAreaList(status);
    }

    public void ClearUpdateMoveList()
    {
        boardStatusScript.ClearUpdateMoveAreList();
    }

    public void ClearMoveDataList()
    {
        boardStatusScript.ClearMoveDataList();
    }
}
