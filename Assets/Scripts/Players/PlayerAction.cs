/////////////////////////////
//制作者　名越大樹
//クラス　ユーザーが操作するクラス
/////////////////////////////

using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    /////////////////////////////////
    //グローバル変数開始
    /////////////////////////////////

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
    [SerializeField]
    bool isRayAcition;
    bool isLockOnRay = false;
    [SerializeField]
    float mouseCorrection;
    public enum AttachStatus
    {
        None,
        SummonChoise,
        Sumon,
        MoveChoose,
        MoveChoosed,
        SkillActive
    };

    public enum ButttonStatus
    {
        None,
        Down,
        Up,
        Continuous
    }
    ButttonStatus buttonStatus = ButttonStatus.None;
    [SerializeField]
    AttachStatus attachStatus = AttachStatus.None;
    [SerializeField]
    GameObject skillIcon;
    [SerializeField]
    GameObject moveIcon;
    Vector3 buttonDownPosition;
    bool isButtonDown;
    /////////////////////////////////
    //グローバル変数終了
    /////////////////////////////////

    void Start()
    {
        copyDelayTime = delayTime;
    }

    void Update()
    {
        if (playerManagerScript.GetIsGamePlay())
        {
            Mouse();
        }
    }

    /// <summary>
    /// マウス動作に関する処理
    /// </summary>
    void Mouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isLockOnRay = true;
            playerManagerScript.SePlay(SeNumbers.CLICK_DOWN);
            buttonStatus = ButttonStatus.Down;
            ButtonDownSetting();
            RayAction();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ButtonUpSetting();
            RayAction();
            if (playerStatusScript.GetLockOnAttachMass() != null)
            {
                playerStatusScript.GetLockOnAttachMass().SetDefaultMaterial();
            }
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseposition = Input.mousePosition;
            if (buttonDownPosition.x + mouseCorrection <= mouseposition.x || buttonDownPosition.x - mouseCorrection >= mouseposition.x)
            {
                playerManagerScript.SetSprite(null);
            }
            else if (buttonDownPosition.y + mouseCorrection <= mouseposition.y || buttonDownPosition.y - mouseCorrection >= mouseposition.y)
            {
                playerManagerScript.SetSprite(null);
            }
        }
        if(isLockOnRay && attachStatus != AttachStatus.None)
        {
            LockOnRay();
        }
        DelayCount();
    }

    /// <summary>
    /// マウスが押されたときの設定処理
    /// </summary>
    void ButtonDownSetting()
    {
        playerManagerScript.SetSprite(null);
        isButtonDown = true;
        buttonDownPosition = Input.mousePosition;
    }

    /// <summary>
    /// マウスを話したときの処理
    /// </summary>
    void ButtonUpSetting()
    {
        isLockOnRay = false;
        buttonStatus = ButttonStatus.Up;
        isRayAcition = false;

        playerManagerScript.ClearColorSummonMassList();
        DestroyInstancePosMass();

        int playerturn = playerManagerScript.GetPlayerTurn();
        int sp = playerManagerScript.GetSP(playerturn);
        if (sp > 0)
        {
            playerManagerScript.ResetIsAnimation(playerturn, playerManagerScript.GetSP(playerturn));
        }
        if (playerStatusScript.GetAttachIllustCard() != null)
        {
            ResetScale(playerStatusScript.GetAttachIllustCard());
        }
        if (playerStatusScript.GetAttachSumonCard() != null)
        {
            GameObject attachObj = playerStatusScript.GetAttachSumonCard();
            GameObject frame = attachObj.GetComponent<SummonStatus>().GetFrame();
            frame.GetComponent<SpriteRenderer>().color = Color.white;
        }
        isButtonDown = false;
        attachStatus = AttachStatus.None;

    }

    /// <summary>
    /// 例に関する処理
    /// </summary>
    void RayAction()
    {
        if (delayTime >= 0.0f)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Object3DRay(ray);
        IllustlationRay(ray);
        ResetDelayCount();
        buttonStatus = ButttonStatus.None;
        return;
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
            case AttachStatus.SkillActive:
                playerManagerScript.AttachSkillTarget(summoncharacter.GetComponent<SummonStatus>());
                break;
        }
    }

    /// <summary>
    /// デッキにタッチした時の処理
    /// </summary>
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
                playerStatusScript.SetPhase(SituationManager.Phase.Summon);
            }
        }
    }

    /// <summary>
    /// 次のフェーズのオブジェクトに当たったときの処理
    /// </summary>
    /// <param name="attachobj"></param>
    void AttachNextPhase(GameObject attachobj)
    {
        if (playerManagerScript.GetPhase() != SituationManager.Phase.Draw)
        {
            playerManagerScript.NextPhase();
        }
    }

    /// <summary>
    /// 手札をタッチした時の処理
    /// </summary>
    /// <param name="attachobj"></param>
    void AtachIllustration(GameObject attachobj)
    {
        playerManagerScript.SetSprite(attachobj.GetComponent<IllustrationStatus>().GetInfomationSprite());
        if (playerStatusScript.GetAttachIllustCard() != null)
        {
            GameObject card = playerStatusScript.GetAttachIllustCard();
            ResetScale(card);
        }
        if (playerStatusScript.GetPhase() == SituationManager.Phase.Summon)
        {
            int playernum = 0;
            int costnum = 0;
            int ratenum = 0;
            int dictionartnum = 0;
            attachStatus = AttachStatus.SummonChoise;
            IllustrationStatus status = attachobj.GetComponent<IllustrationStatus>();
            if (status.GetPlayerNumber() != playerManagerScript.GetPlayerTurn())
            {
                playerStatusScript.SetAttachIllustCard(null);
                return;
            }
            playerStatusScript.SetAttachIllustCard(attachobj);
            status.GetRate_Dictionary_Cost_Player_Number(ref ratenum, ref dictionartnum, ref costnum, ref playernum);
            int spcost = playerManagerScript.GetSP(status.GetPlayerNumber());
            if (spcost - costnum < 0)
            {
                return;
            }
            if (playernum == playerStatusScript.GetPlayerTurn() && costnum <= playerManagerScript.GetSP(playernum))
            {
                status.ZoomUp();
                data.Clear();
                data = playerManagerScript.GetInstancePos(playernum, status);
                playerManagerScript.SetisAnimation(playerManagerScript.GetPlayerTurn(), costnum);
                for (int count = 0; count < data.Count; count++)
                {
                    Vector3 pos = data[count].transform.position;
                    pos.z--;
                    MassStatus massstatus = data[count].GetComponent<MassStatus>();
                    massstatus.SetMaterial(3);
                    playerManagerScript.AddSummonMassList(massstatus);
                }
            }
        }
    }

    /// <summary>
    /// 召喚する時の関数
    /// </summary>
    void Sumon(GameObject attachobj)
    {
        string log = "";
        MassStatus massstatus = attachobj.GetComponent<MassStatus>();
        GameObject character = massstatus.GetCharacterObj();
        GameObject instanceobj = null;
        GameObject attachillustcard = playerStatusScript.GetAttachIllustCard();
        if (isRayAcition)
        {
            return;
        }
        int cost = attachillustcard.GetComponent<IllustrationStatus>().GetCost();
        int sp = playerManagerScript.GetSP(playerManagerScript.GetPlayerTurn());
        if (sp - cost < 0)
        {
            return;
        }
        playerManagerScript.SetSP(playerManagerScript.GetPlayerTurn(), sp - cost, cost);
        IllustrationStatus illuststatus = attachillustcard.GetComponent<IllustrationStatus>();
        int dictionarynum = illuststatus.GetDictionaryNumber();
        int playernumber = illuststatus.GetPlayerNumber();
        bool checkresult = false;
        List<MassStatus> masslist = playerManagerScript.GetSummonMassList();
        for (int count = 0; count < masslist.Count; count++)
        {
            if (masslist[count] == massstatus)
            {
                checkresult = true;
                break;
            }
        }
        for (int count = 0; count < masslist.Count; count++)
        {
            masslist[count].SetDefaultMaterial();
        }
        playerManagerScript.ClearSummonMassList();
        if (!checkresult)
        {
            return;
        }

        if (character == null)
        {
            GameObject sumonobj = playerManagerScript.GetSummonObj(dictionarynum);
            Vector3 pos = attachobj.transform.position;
            pos.z--;
            switch (playerManagerScript.GetPlayerTurn())
            {
                case 1:
                    instanceobj = Instantiate(sumonobj, pos, Quaternion.identity);
                    break;
                case 2:
                    instanceobj = Instantiate(sumonobj, pos, Quaternion.identity);
                    GameObject frame = instanceobj.GetComponent<SummonStatus>().GetFrame();
                    GameObject hpnumber = null, attacknumber = null;
                    instanceobj.GetComponent<SummonStatus>().GetIconObjects(ref hpnumber,ref attacknumber);
                    hpnumber.transform.rotation = Quaternion.Euler(0, 0, 180);
                    attacknumber.transform.rotation = Quaternion.Euler(0, 0, 180);
                    frame.transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
            }
            SummonLog(instanceobj);
            playerManagerScript.ResetIsAnimation(playerManagerScript.GetPlayerTurn(), sp - cost);
            instanceobj.GetComponent<SummonStatus>().SetPlayerNumber(playernumber);
            massstatus.GetComponent<MassStatus>().SetCharacterObj(instanceobj);
            massstatus.GetComponent<MassStatus>().SetMassStatus(BoardManager.MassMoveStatus.Not);
            GameObject status = playerStatusScript.GetAttachIllustCard();
            ResetScale(status);
            status.GetComponent<IllustrationStatus>().ResetScale();
            playerStatusScript.SetAttachIllustCard(null);
            SummonStatus summonstatus = instanceobj.GetComponent<SummonStatus>();
            summonstatus.SetSkillManager(playerManagerScript.GetSkillManager());
            summonstatus.SetAttachMass(massstatus);
            summonstatus.Ini();
            CharacterSkill skill = summonstatus.GetSkill();
            playerManagerScript.AddSkillList(skill);
            playerManagerScript.AddSummonCharacter(instanceobj.GetComponent<SummonStatus>());
            attachStatus = AttachStatus.None;
            playerManagerScript.SummonAnimation(summonstatus,massstatus,illuststatus,playernumber);
            DestroyInstancePosMass();
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
        if (character != null)
        {
            playerManagerScript.SetSprite(character.GetComponent<SummonStatus>().GetInfomationSptite());
        }

        if (character == null)
        {
            return;
        }
        else if (playerManagerScript.GetPhase() != SituationManager.Phase.Battle)
        {
            return;
        }

        if (result)
        {
            SummonStatus sumoncharacter = character.GetComponent<SummonStatus>();
            int getplayernum = sumoncharacter.GetPlayer();
            playerStatusScript.SetAttachSumonCard(character);
            playerManagerScript.SetSprite(sumoncharacter.GetInfomationSptite());
            MoveData.Rate getrate = sumoncharacter.GetRate();
            int length = 0;
            int side = 0;
            int massnum = 0;
            mass.GetComponent<MassStatus>().GetNumbers(ref length, ref side, ref massnum);
            if (getplayernum == playerManagerScript.GetPlayerTurn())
            {
                playerManagerScript.InstanceMovePos(getrate, getplayernum, length, side);
            }
            GameObject frame = sumoncharacter.GetFrame();
            frame.GetComponent<SpriteRenderer>().color = Color.green;
            attachStatus = AttachStatus.MoveChoosed;
        }
    }

    /// <summary>
    /// 移動先が決定した時の処理
    /// </summary>
    void SummonMoveChoosed(GameObject attachmass, MassStatus attachmassstatus)
    {
        bool result = false;
        List<MassStatus> masslist = playerManagerScript.GetMoveList();
        if (attachmassstatus.GetCharacterObj() == playerStatusScript.GetAttachSumonCard())
        {
            playerStatusScript.SetAttachMass(null);
            playerStatusScript.SetAttachSumonCard(null);
            playerManagerScript.ClearColorUpdateMoveAreaList();
            return;
        }
        for (int count = 0; count < masslist.Count; count++)
        {
            if (attachmassstatus == masslist[count])
            {
                result = true;
            }
        }
        if (!result)
        {
            playerManagerScript.ClearColorUpdateMoveAreaList();
            return;
        }

        GameObject attachmasscharcter = attachmassstatus.GetCharacterObj();
        if (attachmasscharcter == playerStatusScript.GetAttachSumonCard())
        {
            playerStatusScript.SetAttachSumonCard(null);
            attachStatus = AttachStatus.None;
            playerManagerScript.ClearColorUpdateMoveAreaList();
            return;
        }
        BoardManager.MassMoveStatus status = attachmassstatus.GetMoveStatus();
        for (int count = 0; count < masslist.Count; count++)
        {
            masslist[count].SetDefaultMaterial();
        }
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
        if (playerStatusScript.GetAttachSumonCard() != null && attachmassstatus.GetMoveStatus() == BoardManager.MassMoveStatus.None)
        {
            SummonStatus playerstatus = playerStatusScript.GetAttachSumonCard().GetComponent<SummonStatus>();
            playerManagerScript.PasshiveSkill(playerstatus);
            playerstatus.SetColor(false);
            playerManagerScript.MoveEndSkill(playerStatusScript.GetAttachSumonCard());
            playerManagerScript.EnemyMoveEndSkill(playerstatus);
        }

        playerStatusScript.SetAttachSumonCard(null);
        if (playerManagerScript.GetPhase() == SituationManager.Phase.Summon)
        {
            playerManagerScript.SetPhase(SituationManager.Phase.Battle);
        }
        isRayAcition = false;
    }

    /// <summary>
    /// 移動先のマスがキャラクターがいなかった時の処理
    /// </summary>
    void MoveMassNoneCharacter(GameObject attachmass, MassStatus attachmassstatus)
    {
        GameObject sumoncard = playerStatusScript.GetAttachSumonCard();
        Vector3 pos = attachmass.transform.position;
        pos.z = -1;
        //        sumoncard.transform.position = pos;
        playerManagerScript.MoveAnimation(sumoncard,pos);
        attachmassstatus.SetCharacterObj(sumoncard);
        sumoncard.GetComponent<SummonStatus>().SetMassNull();
        sumoncard.GetComponent<SummonStatus>().SetAttachMass(attachmassstatus);
        DestroyInstancePosMass();
        playerManagerScript.DecrementMoveCount();
        playerManagerScript.AddMoveList(sumoncard);
        FirstPornChangePorn(playerStatusScript.GetAttachSumonCard());
        MoveLog(sumoncard.GetComponent<SummonStatus>());
        attachStatus = AttachStatus.None;
    }

    /// <summary>
    /// 移動先のマスに敵がいた時の戦闘処理
    /// </summary>
    /// <param name="attachmass"></param>
    /// <param name="attachmassstatus"></param>
    void MoveMassEnemyCharacter(GameObject attachmass, MassStatus attachmassstatus)
    {
        string log = "「";
        GameObject attachcharacter = playerStatusScript.GetAttachSumonCard();
        SummonStatus playerstatus = attachcharacter.GetComponent<SummonStatus>();
        SummonStatus enemystatus = attachmassstatus.GetCharacterObj().GetComponent<SummonStatus>();
        ///////////////////////////////////
        //戦闘開始のログの開始
        ///////////////////////////////////
        log += playerstatus.GetName();
        log += "」";
        log += "VS";
        log += "「";
        log += "」";
        log += enemystatus.GetName();
        ///////////////////////////////////
        //戦闘開始のログの終了
        ///////////////////////////////////

        GameObject enemy = enemystatus.GetSumonObj();
        GameObject player = playerstatus.GetSumonObj();
        playerManagerScript.AddMoveList(player);
        SkillStatus.Status skillresult = playerManagerScript.BattleStart(player, enemy);
        if (skillresult == SkillStatus.Status.Finish)
        {
            return;
        }
        playerManagerScript.SetCardAnimation(playerstatus,enemystatus);
        playerManagerScript.Battle(playerstatus, enemystatus, attachmass, attachmassstatus, playerStatusScript.GetAttachSumonCard(), attachmassstatus.GetCharacterObj());//戦闘開始
        enabled = false;
    }
    //////////////////////
    /// 戦闘に関する関数開始
    //////////////////////

    /// <summary>
    /// 戦闘した結果の処理
    /// </summary>
    public void BattleResult(GameObject player, GameObject enemy, BattleStatus.ResultStatus result, GameObject attachmass, MassStatus attachmassstatus, GameObject playercharacter, GameObject enemycharacter)
    {
        enabled = true;
        if (result == BattleStatus.ResultStatus.Draw)
        {
            playerManagerScript.BattaleEnd(player, enemy);
        }
        else if (result == BattleStatus.ResultStatus.Lose)
        {
            playerManagerScript.BattaleEnd(enemy);
        }
        else if (result == BattleStatus.ResultStatus.Win)
        {
            playerManagerScript.BattaleEnd(player);
        }
        playerManagerScript.DecrementMoveCount();

        switch (result)
        {
            case BattleStatus.ResultStatus.Win:
                BattleWin(attachmass, attachmassstatus, playercharacter, enemycharacter);
                break;
            case BattleStatus.ResultStatus.Draw:
                BattleDraw();
                break;
            case BattleStatus.ResultStatus.Lose:
                BattleLose(attachmass, attachmassstatus, playercharacter, enemycharacter);
                break;
        }
        if (result != BattleStatus.ResultStatus.Lose)
        {
            FirstPornChangePorn(playercharacter);
        }

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

        SummonStatus status = enemycharacter.GetComponent<SummonStatus>();
        MoveData.Rate rate = status.GetRate();
        int number = status.GetPlayer();
        if (rate == MoveData.Rate.King)
        {
            playerManagerScript.GameFinish(number);
            return;
        }
        status.DestoryThisObj();
        Vector3 pos = attachmass.transform.position;
        pos.z--;
        playercharacter.transform.position = pos;
        attachmassstatus.SetCharacterObj(playercharacter);
        playercharacter.GetComponent<SummonStatus>().SetAttachMass(attachmassstatus);
        FirstPornChangePorn(playercharacter);
    }

    void BattleLose(GameObject attachmass, MassStatus attachmassstatus, GameObject playercharacter, GameObject enemycharacter)
    {
        SummonStatus status = playercharacter.GetComponent<SummonStatus>();
        status.DestoryThisObj();
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

    /// <summary>
    /// 動かしたコマがポーンだった場合
    /// </summary>
    /// <param name="playerobj"></param>
    void FirstPornChangePorn(GameObject playerobj)
    {
        MoveData.Rate rate = playerobj.GetComponent<SummonStatus>().GetRate();
        if (rate == MoveData.Rate.FirstPorn)
        {
            playerobj.GetComponent<SummonStatus>().SetRate(MoveData.Rate.Porn);
        }
    }

    /// <summary>
    /// もとの大きさに戻す処理
    /// </summary>
    /// <param name="obj"></param>
    void ResetScale(GameObject obj)
    {
        obj.GetComponent<IllustrationStatus>().ResetScale();
    }

    /// <summary>
    /// 3Dのオブジェクトにさわった場合
    /// </summary>
    /// <param name="ray"></param>
    void Object3DRay(Ray ray)
    {
        RaycastHit hit;
        LayerMask masslayer = playerStatusScript.GetMassLayer();
        LayerMask decklayer = playerStatusScript.GetDeckLayer();
        LayerMask nextphaselayer = playerStatusScript.GetNextPhaseLayer();
        //マスをクリックしたら
        RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, masslayer);
        if (hit2D.collider)
        {
            AtachMass(hit2D.collider.gameObject);
            return;
        }

        //デッキをクリックしたら
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, decklayer))
        {
            AtachDeck(hit.collider.gameObject);
            return;
        }

        //次のフェイズをクリックしたら
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, nextphaselayer))
        {
            AttachNextPhase(hit.collider.gameObject);
            return;
        }
    }

    void IllustlationRay(Ray ray)
    {
        //手札のカードをクリックしたら
        if (!isButtonDown)
        {
            return;
        }
        LayerMask illustrationlayer = playerStatusScript.GetIllustrationLayer();
        RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, illustrationlayer);
        if (hit2D.collider)
        {
            AtachIllustration(hit2D.collider.gameObject);
        }
    }

    public void SetAttachStatus(AttachStatus set)
    {
        attachStatus = set;
    }

    /// <summary>
    /// 召喚したときのログの処理
    /// </summary>
    /// <param name="instanceobj"></param>
    void SummonLog(GameObject instanceobj)
    {
        string log = "P";
        log += playerManagerScript.GetPlayerTurn() + ": 「";
        log += instanceobj.GetComponent<SummonStatus>().GetName();
        log += "」を召喚";
        playerManagerScript.LogUpdate(log);
    }

    void KingSkill(SummonStatus status)
    {
        int playerap = playerManagerScript.GetAP(status.GetPlayer());
        int kingap = status.GetAP();
        if (playerap - kingap < 0)
        {
            return;
        }
        int sum = playerap - kingap;
        playerManagerScript.SetAP(status.GetPlayer(), sum);
        CharacterSkill skill = status.GetSkill();
        skill.KingSkill();
    }

    void MoveLog(SummonStatus character)
    {
        string log = "";
        log += "「";
        log += character.GetName();
        log += "」";
        log += "が移動しました";
        playerManagerScript.LogUpdate(log);
    }

    void LockOnRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask masslayer = playerStatusScript.GetMassLayer();
        RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, masslayer);
        if (hit2D.collider)
        {
            LockOnMass(hit2D.collider.gameObject);
        }
    }

    void LockOnMass(GameObject hitobj)
    {
        MassStatus status = hitobj.GetComponent<MassStatus>();
        MassStatus copystatus = playerStatusScript.GetCopyLockOnAttachMass();
        if (copystatus != null)
        {
            copystatus.SetCopyColor();
        }
        if (status.GetIsSetColor())
        {
            if(status == playerStatusScript.GetLockOnAttachMass())
            {
                return;
            }
            playerManagerScript.SePlay(SeNumbers.LOCK_ON);
            status.SetMaterial(5);
            copystatus = status;
            playerStatusScript.SetLockOnAttachMass(status);
        }
    }
}
