/////////////////////////////
//制作者　名越大樹
//クラス　ユーザーが操作するクラス
/////////////////////////////

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
    [SerializeField]
    float icponPosX;
    [SerializeField]
    float icponPosY;
    [SerializeField]
    bool isRayAcition;
    [SerializeField]
    GameObject normalIcon;
    public enum AttachStatus
    {
        None,
        SummonChoise,
        IconChose,
        MoveIcon,
        SkillIcon,
        Sumon,
        MoveChoose,
        MoveChoosed,
        SkillActive
    };
    public enum IconStatus
    {
        None,
        Choose,
        Normal,
        Move,
        Skill
    }
    public enum ButttonStatus
    {
        None,
        Down,
        Up,
        Continuous
    }
    ButttonStatus buttonStatus = ButttonStatus.None;
    [SerializeField]
    IconStatus iconStatus = IconStatus.None;
    [SerializeField]
    AttachStatus attachStatus = AttachStatus.None;
    [SerializeField]
    GameObject skillIcon;
    [SerializeField]
    GameObject moveIcon;

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

    void Mouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            buttonStatus = ButttonStatus.Down;
            RayAction();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            buttonStatus = ButttonStatus.Up;
            iconStatus = IconStatus.None;
            isRayAcition = false;
            RayAction();
            ButtonUpSetting();
            return;
        }
        else if (isRayAcition)
        {
            buttonStatus = ButttonStatus.Continuous;
            RayAction();
        }
        DelayCount();
    }

    void ButtonUpSetting()
    {
        playerManagerScript.ClearColorSummonMassList();
        DestroyInstancePosMass();
        attachStatus = AttachStatus.None;
        SetActiveIcon(false);
        int playerturn = playerManagerScript.GetPlayerTurn();
        playerManagerScript.ResetIsAnimation(playerturn,playerManagerScript.GetSP(playerturn));
        if (playerStatusScript.GetAttachIllustCard() != null)
        {
            ResetScale(playerStatusScript.GetAttachIllustCard());
        }
    }
    void RayAction()
    {
        if (delayTime >= 0.0f)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        switch (iconStatus)
        {
            case IconStatus.Choose:
            case IconStatus.Normal:
                IconRay(ray);
                break;
        }
        if (iconStatus == IconStatus.Move || iconStatus == IconStatus.Skill)
        {
            IconRay(ray);
            return;
        }
        else if (iconStatus == IconStatus.None && isRayAcition == false)
        {
            Object3DRay(ray);
            IllustlationRay(ray);
            ResetDelayCount();
            playerManagerScript.ClearUpdateMoveList();
            buttonStatus = ButttonStatus.None;
            return;
        }
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
        Debug.Log("カードをアタッチしました");
        playerManagerScript.SetSprite(attachobj.GetComponent<SpriteRenderer>().sprite);
        if (playerStatusScript.GetAttachIllustCard() != null)
        {
            GameObject card = playerStatusScript.GetAttachIllustCard();
            ResetScale(card);
        }
        if (playerStatusScript.GetPhase() == SituationManager.Phase.Summon && playerStatusScript.GetAttachIllustCard() == null)
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
        playerManagerScript.SetSP(playerManagerScript.GetPlayerTurn(), sp - cost, cost);
        int dictionarynum = attachillustcard.GetComponent<IllustrationStatus>().GetDictionaryNumber();
        int playernumber = attachillustcard.GetComponent<IllustrationStatus>().GetPlayerNumber();
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
                    frame.transform.rotation = Quaternion.Euler(0, 0, 180);

                    break;
            }
            SummonLog(instanceobj);
            playerManagerScript.ResetIsAnimation(playerManagerScript.GetPlayerTurn(), sp - cost);
            instanceobj.GetComponent<SummonStatus>().SetPlayerNumber(playernumber);
            massstatus.GetComponent<MassStatus>().SetCharacterObj(instanceobj);
            massstatus.GetComponent<MassStatus>().SetMassStatus(BoardManager.MassMoveStatus.Not);
            playerManagerScript.ReMoveIllustCard(playernumber, attachillustcard);
            GameObject status = playerStatusScript.GetAttachIllustCard();
            ResetScale(status);
            status.GetComponent<IllustrationStatus>().ResetScale();
            playerStatusScript.SetAttachIllustCard(null);
            instanceobj.GetComponent<SummonStatus>().SetSkillManager(playerManagerScript.GetSkillManager());
            instanceobj.GetComponent<SummonStatus>().SetAttachMass(massstatus);
            CharacterSkill skill = instanceobj.GetComponent<SummonStatus>().GetSkill();
            playerManagerScript.AddSkillList(skill);
            playerManagerScript.AddSummonCharacter(instanceobj.GetComponent<SummonStatus>());
            attachStatus = AttachStatus.None;
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
        if (character.GetComponent<SpriteRenderer>() != null)
        {
            playerManagerScript.SetSprite(character.GetComponent<SpriteRenderer>().sprite);
        }

        if (character == null)
        {
            return;
        }
        if (attachStatus == AttachStatus.None && result)
        {
            iconStatus = IconStatus.Choose;
            buttonStatus = ButttonStatus.Continuous;
            playerStatusScript.SetAttachMass(mass);
            int playernum = character.GetComponent<SummonStatus>().GetPlayer();
            if (playernum != playerManagerScript.GetPlayerTurn())
            {
                return;
            }
            SetActiveIcon(true);
            isRayAcition = true;
            playerStatusScript.SetAttachSumonCard(character);
            IconChoose(character);
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
        bool result = false;
        List<MassStatus> masslist = playerManagerScript.GetMoveList();
        if (attachmassstatus.GetCharacterObj() == playerStatusScript.GetAttachSumonCard())
        {
            playerStatusScript.SetAttachMass(null);
            playerStatusScript.SetAttachSumonCard(null);
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
            return;
        }

        GameObject attachmasscharcter = attachmassstatus.GetCharacterObj();
        if (attachmasscharcter == playerStatusScript.GetAttachSumonCard())
        {
            playerStatusScript.SetAttachSumonCard(null);
            attachStatus = AttachStatus.None;
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
        sumoncard.transform.position = pos;
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
        SkillStatus.Status skillresult = playerManagerScript.BattleStart(player, enemy);
        Debug.Log(skillresult);
        if (skillresult == SkillStatus.Status.Finish)
        {
            return;
        }
        BattleStatus.ResultStatus result = playerManagerScript.Battle(playerstatus, enemystatus);//戦闘開始
        BattleResult(result, attachmass, attachmassstatus, playerStatusScript.GetAttachSumonCard(), attachmassstatus.GetCharacterObj());
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
                BattleLose(attachmass, attachmassstatus, playercharacter, enemycharacter);
                break;
        }
        if (result != BattleStatus.ResultStatus.Lose)
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
        SummonStatus status = enemycharacter.GetComponent<SummonStatus>();
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

    void FirstPornChangePorn(GameObject playerobj)
    {
        MoveData.Rate rate = playerobj.GetComponent<SummonStatus>().GetRate();
        if (rate == MoveData.Rate.FirstPorn)
        {
            playerobj.GetComponent<SummonStatus>().SetRate(MoveData.Rate.Porn);
        }
    }

    void ResetScale(GameObject obj)
    {
        obj.GetComponent<IllustrationStatus>().ResetScale();
    }
    void IconChoose(GameObject character)
    {
        Vector3 pos = character.transform.position;
        pos.x -= icponPosX;
        pos.y += icponPosY;
        moveIcon.transform.position = pos;
        normalIcon.transform.position = character.transform.position;
        pos = character.transform.position;
        pos.x += icponPosX;
        pos.y -= icponPosY;
        skillIcon.transform.position = pos;
    }

    void SetActiveIcon(bool set)
    {
        skillIcon.SetActive(set);
        moveIcon.SetActive(set);
    }

    void SkillIcon()
    {

    }

    void NoramlIcon()
    {
        iconStatus = IconStatus.Normal;
        isRayAcition = true;
        playerManagerScript.ClearColorUpdateMoveAreaList();
        SetActiveIcon(true);
        DestroyInstancePosMass();
    }

    void Object2DRay()
    {

    }

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

    void IconRay(Ray ray)
    {
        LayerMask moveiconLayer = playerStatusScript.GetMoveIconLayer();
        RaycastHit2D hit2DmoveIcon = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, moveiconLayer);
        if (hit2DmoveIcon.collider)
        {
            if (hit2DmoveIcon.collider.gameObject.tag == "MoveIcon")
            {

                Debug.Log("moveIcon");
                iconStatus = IconStatus.Move;
                attachStatus = AttachStatus.MoveChoosed;
                SetActiveIcon(false);
                SummonMoveChoose(playerStatusScript.GetAttachSumonCard(), playerStatusScript.GetAttachMass());
            }
            else if (hit2DmoveIcon.collider.gameObject.tag == "SkillIcon")
            {
                Debug.Log("skillIcon");
                SummonStatus status = playerStatusScript.GetAttachSumonCard().GetComponent<SummonStatus>();
                MoveData.Rate rate = status.GetRate();
                if (rate == MoveData.Rate.King)
                {
                    KingSkill(status);
                    SetActiveIcon(false);
                    isRayAcition = false;
                    if (playerStatusScript.GetAttachIllustCard() != null)
                    {
                        GameObject card = playerStatusScript.GetAttachIllustCard();
                        ResetScale(card);
                    }
                    playerStatusScript.SetAttachIllustCard(null);
                    return;
                }
                status.SetSkillEfeect();
                SetActiveIcon(false);
                isRayAcition = false;
            }

            else if (hit2DmoveIcon.collider.gameObject.tag == "NormalIcon")
            {
                Debug.Log("NormalIcon");
                NoramlIcon();
            }
        }

    }

    void IllustlationRay(Ray ray)
    {
        //手札のカードをクリックしたら
        LayerMask illustrationlayer = playerStatusScript.GetIllustrationLayer();
        RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, illustrationlayer);
        if (hit2D.collider && playerStatusScript.GetAttachSumonCard() == null)
        {
            AtachIllustration(hit2D.collider.gameObject);
        }
    }

    public void SetAttachStatus(AttachStatus set)
    {
        attachStatus = set;
    }

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
}
