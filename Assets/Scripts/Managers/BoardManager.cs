//////////////////////////////////
//製作者　名越大樹
//クラス　ボード全体を監視するクラス
//////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

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
    [SerializeField]
    CSVRead csvReadScriptDeck1;
    [SerializeField]
    CSVRead csvReadScriptDeck2;
    [SerializeField]
    DeckHandManager deckHandManagerScript;
    [SerializeField]
    SituationManager situatitonManagerScript;
    [SerializeField]
    SPAPManager spapManagerScript;
    [SerializeField]
    BoardAction boardActionScript;
    [SerializeField]
    SocketAction socketActionScript;
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

    public List<GameObject> GetInstancePos(int playernum, IllustrationStatus status)
    {
        return boardStatusScript.GetInstancePos(playernum, status);
    }

    public void AddSummonCharacter(SummonStatus set)
    {
        boardStatusScript.AddSummonCharacterList(set);
    }

    public void DeckHandIni()
    {
        deckHandManagerScript.DeckHandIni();
    }

    public void AddSummonMassList(MassStatus set)
    {
        boardStatusScript.AddSumonnMassList(set);
    }

    public List<MassStatus> GetSummonMassList()
    {
        return boardStatusScript.GetSummonMassList();
    }

    public void ClearSummonMassList()
    {
        boardStatusScript.ClearSummonMassList();
    }

    /// <summary>
    /// アタッチしたプレイヤーの移動できる場所の生成
    /// </summary>
    public void InstanceMoveData(MoveData.Rate rate, int playernum, int nowlengthmass, int nowsidemass)
    {
        boardStatusScript.InstanceMovePos(rate, playernum, nowlengthmass, nowsidemass);
    }

    public void AddMoveList(GameObject target)
    {
        boardStatusScript.AddMoveDataList(target);
    }

    public bool CheckMoveList(GameObject target)
    {
        return boardStatusScript.CheckMoveDataList(target);
    }

    public void ReMoveMoveList(GameObject target)
    {
        boardStatusScript.RemoveMoveDataList(target);
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

    public void SummonCharacterRemoveat(GameObject target)
    {
        boardStatusScript.SummonCharacterRemoveat(target);
    }

    /// <summary>
    /// ボードの生成が完了したら
    /// </summary>
    public void DoneBoardMass()
    {
        if (!gameMasterScript.GetIsNetWork())
        {
            csvReadScriptDeck1.ResourcesRead();
        }
        else
        {
            socketActionScript.DeckRead();
        }
        csvReadScriptDeck2.ResourcesRead();
        situatitonManagerScript.Ini();
        spapManagerScript.Ini();
    }
    public GameObject GetMassObject(int length, int side)
    {
        return boardStatusScript.GetMass(length, side);
    }

    /// <summary>
    /// マス上に存在する移動できるマスのオブジェクトの削除
    /// </summary>
    public void DestroyInstancePosMass()
    {
        var clones = GameObject.FindGameObjectsWithTag("InstancePos");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    public List<MassStatus> SearchPlayerMass(int player)
    {
        return boardStatusScript.SearchPlayerMass(player);
    }
    public List<MassStatus> SearchMassAround(int length, int side)
    {
        return boardStatusScript.GetSearchMassAround(length, side);
    }

    public void AllMyArea(int player)
    {
        boardStatusScript.AllMyArea(player);
    }

    public void AllDefaultArea()
    {
        boardStatusScript.AllDefaultArea();
    }

    public List<MassStatus> GetMoveList()
    {
       return boardStatusScript.GetUpdateMoveAreaList();
    }

    public void SummonCharacterListColorClear()
    {
        boardStatusScript.SummonCharacterClear();
    }

    public void ClearColorSummonMassList()
    {
        boardActionScript.ClearColorSummonList();
    }

    public void ClearColorUpdateMoveAreaList()
    {
        boardActionScript.ClearColorUpdateMoveAreaList();
    }

    public int GetPlayerTurn()
    {
        return situatitonManagerScript.GetPlayerTurn();
    }

    public void EnemyMoveEndSkill(SummonStatus character)
    {
        boardActionScript.EnemyMoveEndSkill(character);
    }
}
