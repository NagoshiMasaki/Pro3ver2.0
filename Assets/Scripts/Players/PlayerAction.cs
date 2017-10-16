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
    public enum AttachStatus
    {
        None,
        SummonChoise,
        Sumon,
        MoveChoose,
        MoveChoosed
    };
    AttachStatus attachStatus = AttachStatus.None;
    void Update()
    {
        Mouse();
    }

    void Mouse()
    {
        int turn = playerStatusScript.GetPlayerTurn();
        if (turn == playerNumber)
        {
            if (Input.GetMouseButtonDown(0))
                RayAction();
            else if (Input.GetMouseButtonUp(0))
                RayAction();
        }
    }

    void RayAction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask masslayer = playerStatusScript.GetMassLayer();

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
        if (hit2D.collider)
        {
            AtachIllustration(hit2D.collider.gameObject);
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
            case AttachStatus.MoveChoose:
                attachobj.GetComponent<MassStatus>().GetNumbers(ref length, ref side, ref number);
                break;
            case AttachStatus.SummonChoise:
                Sumon(attachobj);
                break;
            case AttachStatus.None:
                SummonMoveChoose(summoncharacter, attachobj);
                break;
            case AttachStatus.MoveChoosed:
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
    /// 手札のデッキをタッチした時の処理
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
            instanceobj.GetComponent<SummonStatus>().SetPlayerNumber(playerNumber);
            attachobj.GetComponent<MassStatus>().SetCharacterObj(instanceobj);
            DestroyInstancePosMass();
            playerManagerScript.ReMoveIllustCard(playernumber, attachobj);
            attachStatus = AttachStatus.None;
        }
    }

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
        int getplayernum = character.GetComponent<SummonStatus>().GetPlayer();
        MoveData.Rate getrate = character.GetComponent<SummonStatus>().GetRate();
        int length = 0;
        int side = 0;
        int massnum = 0;
        mass.GetComponent<MassStatus>().GetNumbers(ref length, ref side, ref massnum);
        if (getplayernum == playerNumber)
        {
            playerManagerScript.InstanceMovePos(getrate, getplayernum, length, side);
        }
        attachStatus = AttachStatus.MoveChoosed;
    }

    //移動先が決定した時の処理
    void SummonMoveChoosed(GameObject attachmass)
    {

    }
}
