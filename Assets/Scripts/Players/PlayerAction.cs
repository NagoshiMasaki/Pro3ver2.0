using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    [SerializeField]
    PlayerStatus playerStatusScript;
    int playerNumber = 1;
    [SerializeField]
    BoardStatus boardStatusScript;
    [SerializeField]
    PlayerManager playerManagerScript;
    List<GameObject> data = new List<GameObject>();
    [SerializeField]
    GameObject InstanceMass;
    [SerializeField]
    float delayTime;
    float copyDelayTime;
    GameObject attachmassobject;//現在どこのマスを刺しいるのかの確認
    public enum AttachStatus
    {
        None,
        SummonChoise,
        Sumon,
        MoveChoose,
        MoveChoosed
    };
    [SerializeField]
    AttachStatus attachStatus = AttachStatus.None;

    void Start()
    {
        copyDelayTime = delayTime;
    }

    void Update()
    {
        Mouse();
    }

    void Mouse()
    {
        //int turn = playerStatusScript.GetPlayerTurn();
        //        if (turn == playerNumber)
        //       {
        if (Input.GetMouseButtonDown(0))
        {
            RayAction();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            RayAction();
        }
        //        }
        DelayCount();
    }

    void RayAction()
    {
        if (delayTime >= 0.0f)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask masslayer = playerStatusScript.GetMassLayer();
        DestroyInstancePosMass();
        //マスをクリックしたら
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, masslayer))
        {
            AtachMass(hit.collider.gameObject);
            return;
        }

        //デッキをクリックしたら
        LayerMask decklayer = playerStatusScript.GetDeckLayer();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, decklayer))
        {
            AtachDeck(hit.collider.gameObject);
            return;
        }

        //手札のカードをクリックしたら
        LayerMask illustrationlayer = playerStatusScript.GetIllustrationLayer();
        RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, illustrationlayer);
        if (hit2D.collider && playerStatusScript.GetAttachSumonCard() == null)
        {
            AtachIllustration(hit2D.collider.gameObject);
        }
        ResetDelayCount();
        playerManagerScript.ClearUpdateMoveList();
    }

    /// <summary>
    /// マスにタッチした時の処理
    /// </summary>
    void AtachMass(GameObject attachobj)
    {
        int length = 0, side = 0, number = 0;
        GameObject summoncharacter = attachobj.GetComponent<MassStatus>().GetCharacterObj();
        switch (attachStatus)
        {
            case AttachStatus.SummonChoise:
                Sumon(attachobj);
                break;

            case AttachStatus.None:
                SummonMoveChoose(summoncharacter, attachobj);
                break;

            case AttachStatus.MoveChoose:
                attachobj.GetComponent<MassStatus>().GetNumbers(ref length, ref side, ref number);
                break;

            case AttachStatus.MoveChoosed:
                MassStatus status = attachobj.GetComponent<MassStatus>();
                SummonMoveChoosed(attachobj, status);
                break;
        }
    }

    /// <summary>
    /// デッキにタッチした時の処理
    /// </summary>
    /// <param name="attachdeck"></param>
    void AtachDeck(GameObject attachdeck)
    {
        SituationManager.Phase phase = playerStatusScript.GetPhase();
        if (phase == SituationManager.Phase.Draw)
        {
            int decknum = attachdeck.GetComponent<DeckClass>().GetPlayerNumber();
            int turnnum = playerStatusScript.GetPlayerTurn();
            if (turnnum == decknum)//ドローに関する処理
            {
                GameObject drawobj = playerStatusScript.GetDrawObj(decknum);
                playerManagerScript.InstanceDrawCard(decknum, drawobj);
                playerStatusScript.SetPhase(SituationManager.Phase.Main1);
            }
        }
    }

    /// <summary>
    /// 手札をタッチした時の処理
    /// </summary>
    /// <param name="attachobj"></param>
    void AtachIllustration(GameObject attachobj)
    {
        playerStatusScript.SetAttachIllustCard(attachobj);
        if (playerStatusScript.GetPhase() == SituationManager.Phase.Main1 || playerStatusScript.GetPhase() == SituationManager.Phase.Main2)
        {
            int playernum = 0;
            int costnum = 0;
            int ratenum = 0;
            int dictionartnum = 0;
            attachStatus = AttachStatus.SummonChoise;
            attachobj.GetComponent<IllustrationStatus>().GetRate_Dictionary_Cost_Player_Number(ref ratenum, ref dictionartnum, ref costnum, ref playernum);
            if (playernum == playerStatusScript.GetPlayerTurn() && costnum <= playerManagerScript.GetSP(playernum))
            {
                data.Clear();
                data = playerManagerScript.GetInstancePos(playernum);
                for (int count = 0; count < data.Count; count++)
                {
                    Vector3 pos = data[count].transform.position;
                    pos.z--;
                    Instantiate(InstanceMass, pos, Quaternion.identity);
                }
            }
        }
    }

    /// <summary>
    /// 召喚する時の関数
    /// </summary>
    void Sumon(GameObject attachobj)
    {
        GameObject character = attachobj.GetComponent<MassStatus>().GetCharacterObj();
        GameObject attachillustcard = playerStatusScript.GetAttachIllustCard();
        int dictionarynum = attachillustcard.GetComponent<IllustrationStatus>().GetDictionaryNumber();
        int playernumber = attachillustcard.GetComponent<IllustrationStatus>().GetPlayerNumber();
        if (character == null)
        {
            GameObject sumonobj = playerManagerScript.GetSummonObj(dictionarynum);
            Vector3 pos = attachobj.transform.position;
            pos.z--;
            GameObject instanceobj = Instantiate(sumonobj, pos, Quaternion.identity);
            instanceobj.GetComponent<SummonStatus>().SetPlayerNumber(playernumber);
            attachobj.GetComponent<MassStatus>().SetCharacterObj(instanceobj);
            playerManagerScript.ReMoveIllustCard(playernumber, attachillustcard);
            playerStatusScript.SetAttachIllustCard(null);
            attachStatus = AttachStatus.None;
        }
    }

    /// <summary>
    /// マス上に存在する移動できるマスのオブジェクトの削除
    /// </summary>
    void DestroyInstancePosMass()
    {
        var clones = GameObject.FindGameObjectsWithTag("InstancePos");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    /// <summary>
    /// マス上のキャラクターを動かす時のマスを表示させる処理
    /// </summary>
    /// <param name="character"></param>
    void SummonMoveChoose(GameObject character, GameObject mass)
    {
        bool result = playerManagerScript.CheckMoveList(character);
        if (character == null)
        {
            return;
        }
        if (result)
        {
            int getplayernum = character.GetComponent<SummonStatus>().GetPlayer();
            playerStatusScript.SetAttachSumonCard(character);
            MoveData.Rate getrate = character.GetComponent<SummonStatus>().GetRate();
            int length = 0;
            int side = 0;
            int massnum = 0;
            mass.GetComponent<MassStatus>().GetNumbers(ref length, ref side, ref massnum);
            if (getplayernum == playerManagerScript.GetPlayerTurn())
            {
                playerManagerScript.InstanceMovePos(getrate, getplayernum, length, side);
            }
            attachStatus = AttachStatus.MoveChoosed;
        }
    }

    /// <summary>
    /// 移動先が決定した時の処理
    /// </summary>
    void SummonMoveChoosed(GameObject attachmass, MassStatus attachmassstatus)
    {
        GameObject attachmasscharcter = attachmassstatus.GetCharacterObj();
        if(attachmasscharcter == playerStatusScript.GetAttachSumonCard())
        {
            playerStatusScript.SetAttachSumonCard(null);
            attachStatus = AttachStatus.None;
            return;
        }
        BoardManager.MassMoveStatus status = attachmassstatus.GetMoveStatus();
        switch (status)
        {
            case BoardManager.MassMoveStatus.None:
                MoveMassNoneCharacter(attachmass, attachmassstatus);
                attachStatus = AttachStatus.None;
                break;
            case BoardManager.MassMoveStatus.Enemy:
                MoveMassEnemyCharacter(attachmass, attachmassstatus);
                attachStatus = AttachStatus.None;
                break;
        }
    }

    /// <summary>
    /// 移動先のマスがキャラクターがいなかった時の処理
    /// </summary>
    void MoveMassNoneCharacter(GameObject attachmass, MassStatus attachmassstatus)
    {
        GameObject sumoncard = playerStatusScript.GetAttachSumonCard();
        Vector3 pos = attachmass.transform.position;
        pos.z = -1;
        sumoncard.transform.position = pos;
        attachmassstatus.SetCharacterObj(sumoncard);
        DestroyInstancePosMass();
        playerManagerScript.DecrementMoveCount();
        playerManagerScript.AddMoveList(sumoncard);
        FirstPornChangePorn(playerStatusScript.GetAttachSumonCard());
        playerStatusScript.SetAttachSumonCard(null);
        attachStatus = AttachStatus.None;
    }

    /// <summary>
    /// 移動先のマスに敵がいた時の戦闘処理
    /// </summary>
    /// <param name="attachmass"></param>
    /// <param name="attachmassstatus"></param>
    void MoveMassEnemyCharacter(GameObject attachmass, MassStatus attachmassstatus)
    {
        GameObject attachcharacter = playerStatusScript.GetAttachSumonCard();
        SummonStatus playerstatus = attachcharacter.GetComponent<SummonStatus>();
        SummonStatus enemystatus = attachmassstatus.GetCharacterObj().GetComponent<SummonStatus>();
        BattleStatus.ResultStatus result = playerManagerScript.Battle(playerstatus, enemystatus);
        BattleResult(result,attachmass,attachmassstatus,playerStatusScript.GetAttachSumonCard(),attachmassstatus.GetCharacterObj());
        playerManagerScript.DecrementMoveCount();
        playerStatusScript.SetAttachSumonCard(null);
    }
    //////////////////////
    /// 戦闘に関する関数開始
    //////////////////////

    /// <summary>
    /// 戦闘した結果の処理
    /// </summary>
    void BattleResult(BattleStatus.ResultStatus result, GameObject attachmass, MassStatus attachmassstatus, GameObject playercharacter, GameObject enemycharacter)
    {
        switch (result)
        {
            case BattleStatus.ResultStatus.Win:
                BattleWin(attachmass, attachmassstatus, playercharacter, enemycharacter);
                break;
            case BattleStatus.ResultStatus.Draw:
                BattleDraw();
                break;
            case BattleStatus.ResultStatus.Lose:
                break;
        }
        if(result != BattleStatus.ResultStatus.Lose)
        {
            FirstPornChangePorn(playercharacter);
        }
        playerManagerScript.AddMoveList(playercharacter);
    }

    /// <summary>
    /// 戦闘に引き分けになったときの処理
    /// </summary>
    void BattleDraw()
    {
        Debug.Log("ドロー");
    }

    /// <summary>
    /// 戦闘に勝った時の処理
    /// </summary>
    void BattleWin(GameObject attachmass, MassStatus attachmassstatus, GameObject playercharacter, GameObject enemycharacter)
    {
        Destroy(enemycharacter);
        Vector3 pos = attachmass.transform.position;
        pos.z--;
        playercharacter.transform.position = pos;
        attachmassstatus.SetCharacterObj(playercharacter);
        FirstPornChangePorn(playercharacter);
    }

    void BattleLose(GameObject attachmass, MassStatus attachmassstatus, GameObject playercharacter, GameObject enemycharacter)
    {
        Destroy(playercharacter);
        attachmassstatus.SetMassStatus(BoardManager.MassMoveStatus.None);
    }
    /////////////////////////
    //戦闘に関する関数終了
    /////////////////////////

    void DelayCount()
    {
        delayTime -= Time.deltaTime;
    }

    void ResetDelayCount()
    {
        delayTime = copyDelayTime;
    }

    void FirstPornChangePorn(GameObject playerobj)
    {
      MoveData.Rate rate =  playerobj.GetComponent<SummonStatus>().GetRate();
        if(rate == MoveData.Rate.FirstPorn)
        {
            playerobj.GetComponent<SummonStatus>().SetRate(MoveData.Rate.Porn);
        }
    }
}
