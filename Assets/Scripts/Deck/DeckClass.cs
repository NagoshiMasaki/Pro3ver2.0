//////////////////////////////
//製作者　名越大樹
//クラス　各プレイヤーにあるデッキを管理するクラス
//////////////////////////////

using System.Collections.Generic;
using UnityEngine;

public class DeckClass : MonoBehaviour
{
    [SerializeField]
    SkillManager skillmanager;
    [SerializeField]
    DictionaryManager dictopnaryManagerScript;
    [SerializeField]
    List<GameObject> characterList = new List<GameObject>();
    [SerializeField]
    int playerNumber;
    [SerializeField]
    DeckHand deckHandScript;
    [SerializeField]
    PlayerStatus playerStatusScript;
    [SerializeField]
    BoardManager boardManagerScritpt;
    string inisendData;

    public string IniSendData { get { return inisendData; } set { inisendData = value; } }

    public void SetPlayerNumber(int set)
    {
        playerNumber = set;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public void SetCharacter(int num, int playernum)
    {
        GameObject character = dictopnaryManagerScript.GetIllustCharacter(num);
        IllustrationStatus status = character.GetComponent<IllustrationStatus>();
        if (status.GetRate() != MoveData.Rate.King)
        {
            characterList.Add(character);
            return;
        }
        SumonKing(playernum, status);
    }

    public GameObject GetCharacter()
    {
        GameObject character = characterList[0];
        characterList.RemoveAt(0);
        return character;
    }

    /// <summary>
    /// 読み込んだデッキをシャッフル処理
    /// </summary>
    public void IniShaffle()
    {
        IniDeckDraw();
    }

    void IniDeckDraw()
    {
        if (characterList.Count == 0)
        {
            deckHandScript.GameFinish(playerNumber);
        }
        int num = playerStatusScript.GetIniDeckHandCount();
        for (int count = 0; num > count; count++)
        {
            deckHandScript.SetDrawObj(characterList[0]);//デッキの一番先頭のカード
            characterList.RemoveAt(0);//デッキの一番最初のカードを削除
        }

        deckHandScript.SetIsIniDraw(true);
    }

    /// <summary>
    /// キングを読み込んだときにボード上に生成する処理
    /// </summary>
    /// <param name="playernum"></param>
    /// <param name="status"></param>
    void SumonKing(int playernum, IllustrationStatus status)
    {
        GameObject sumonobj = dictopnaryManagerScript.GetSummonCharacter(status.GetDictionaryNumber());
        GameObject mass = null;
        MassStatus massstatus;
        switch (playernum)
        {
            case 1:
                mass = boardManagerScritpt.GetMassObject(0, 3);
                massstatus = mass.GetComponent<MassStatus>();
                break;
            case 2:
                mass = boardManagerScritpt.GetMassObject(5, 2);
                massstatus = mass.GetComponent<MassStatus>();
                break;
        }
        Vector3 instancepos = mass.transform.position;
        massstatus = mass.GetComponent<MassStatus>();
        instancepos.z--;
        GameObject instance = Instantiate(sumonobj, instancepos, Quaternion.identity);
        if (playernum == 2)
        {
            GameObject frame = instance.GetComponent<SummonStatus>().GetFrame();
            GameObject hpnumberobj = null, attacknumberobj = null;
            instance.GetComponent<SummonStatus>().GetIconObjects(ref hpnumberobj, ref attacknumberobj);
            frame.transform.rotation = Quaternion.Euler(0, 0, 180);
            hpnumberobj.transform.rotation = Quaternion.Euler(0, 0, -180);
            attacknumberobj.transform.rotation = Quaternion.Euler(0, 0, -180);
        }
        SummonStatus instancesummon = instance.GetComponent<SummonStatus>();
        instancesummon.SetSkillManager(skillmanager);
        instancesummon.Ini();
        instancesummon.SetPlayerNumber(playernum);
        instancesummon.SetAttachMass(massstatus);
        boardManagerScritpt.AddSummonCharacter(instancesummon);
        int id = instancesummon.InstanceID;
        SocketGameStatus.kingdata = "summon" + " / " + id.ToString();
        SkillManager skillmamnager = instancesummon.GetSkillManager();
        CharacterSkill skill = instancesummon.GetSkill();
        skillmamnager.AddSkillList(skill);
        massstatus.SetMassStatus(BoardManager.MassMoveStatus.Not);
        massstatus.SetCharacterObj(instance);
    }
}
