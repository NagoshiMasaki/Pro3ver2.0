////////////////////////////////////////
//製作者　名越大樹
//クラス　各種族の挙動に関するクラス
////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveData : MonoBehaviour
{
    [SerializeField]
    int maxLength;
    [SerializeField]
    int maxSide;
    [SerializeField]
    PlayerManager playerManagerScript;


    public enum Rate
    {
        Porn,
        Night,
        Luke,
        Bishop,
        Queen,
        King,
        FirstPorn
    }
    public enum Status
    {
        None,
        Enemy,
        Player,
        Nothing
    }
    [SerializeField]
    BoardManager boardManagerscript;

    public void GetMoveData(Rate rate, int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int nowlengthmass, int nowsidemass, GameObject[] instanceobj)
    {
        switch (rate)
        {
            case Rate.Porn:
                PornMove(playernum, massobjects, massstatuses, nowlengthmass, nowsidemass, instanceobj);
                break;
            case Rate.Queen:
                QueenMove(playernum, massobjects, massstatuses, nowlengthmass, nowsidemass, instanceobj);
                break;
            case Rate.Night:
                NightMove(playernum, massobjects, massstatuses, nowlengthmass, nowsidemass, instanceobj);
                break;
            case Rate.Luke:
                LukeMove(playernum, massobjects, massstatuses, nowlengthmass, nowsidemass, instanceobj);
                break;
            case Rate.Bishop:
                BishopMove(playernum, massobjects, massstatuses, nowlengthmass, nowsidemass, instanceobj);
                break;
            case Rate.King:
                KingMove(playernum, massobjects, massstatuses, nowlengthmass, nowsidemass, instanceobj);
                break;
            case Rate.FirstPorn:
                FirstPornMove(playernum, massobjects, massstatuses, nowlengthmass, nowsidemass, instanceobj);
                break;
        }
    }

    /// <summary>
    /// 2回目以降のポーンの動きの処理
    /// </summary>
    void PornMove(int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int length, int side, GameObject[] instanceobj)
    {
        if (playernum == 1)
        {
            length++;
        }
        else
        {
            length--;
        }
        for (int count = -1; count <= 1; count++)
        {
            int sidesum = side + count;
            bool result = OutSideLength(length, sidesum);
            if (result)
            {
                int num = 0;
                GameObject character = massstatuses[length, sidesum].GetCharacterObj();
                if (character != null)//キャラクターの所属先のプレイヤー番号の取得
                {
                    num = character.GetComponent<SummonStatus>().GetPlayer();
                }
                if (character == null && count == 0)
                {
                    MassOnNotCharacter(length, sidesum, massobjects, massstatuses, instanceobj);
                }

                else if (playernum != num)
                {
                    MassOnEnemyCharacter(length, sidesum, massobjects, massstatuses, instanceobj);
                }
            }
        }
    }

    void NightMove(int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int length, int side, GameObject[] instanceobj)
    {
        for (int lengthcount = -2; lengthcount <= 2; lengthcount++)
        {
            for (int sidecount = -2; sidecount <= 2; sidecount++)
            {
                switch (lengthcount)
                {
                    case -2:
                    case 2:
                        if (sidecount == -1 || sidecount == 1)
                        {
                            OnMassCheck(playernum, massobjects, massstatuses, length + lengthcount, side + sidecount, instanceobj);
                        }
                        break;
                    case -1:
                    case 1:
                        if (sidecount == -2 || sidecount == 2)
                        {
                            OnMassCheck(playernum, massobjects, massstatuses, length + lengthcount, side + sidecount, instanceobj);
                        }
                        break;
                }
            }
        }
    }

    void QueenMove(int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int length, int side, GameObject[] instanceobj)
    {
        LukeMove(playernum, massobjects, massstatuses, length, side, instanceobj);
        BishopMove(playernum, massobjects, massstatuses, length, side, instanceobj);
    }
    void KingMove(int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int length, int side, GameObject[] instanceobj)
    {
        for (int lengthcount = -1; lengthcount <= 1; lengthcount++)
        {
            for (int sidecount = -1; sidecount <= 1; sidecount++)
            {
                OnMassCheck(playernum, massobjects, massstatuses, length + lengthcount, side + sidecount, instanceobj);
            }
        }

    }

    void LukeMove(int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int length, int side, GameObject[] instanceobj)
    {
        for (int sidecount = 0; sidecount < maxSide; sidecount++)
        {
            Status result = OnMassCheck(playernum, massobjects, massstatuses, length, side + sidecount, instanceobj);
            if (result == Status.Enemy)
            {
                break;
            }
        }

        for (int sidecount = 0; sidecount > -maxSide; sidecount--)
        {
            Status result = OnMassCheck(playernum, massobjects, massstatuses, length, side + sidecount, instanceobj);
            if (result == Status.Enemy)
            {
                break;
            }
        }

        for (int lengthcount = 0; lengthcount < maxLength; lengthcount++)
        {
            Status result = OnMassCheck(playernum, massobjects, massstatuses, length + lengthcount, side, instanceobj);
            if (result == Status.Enemy)
            {
                break;
            }
        }

        for (int lengthcount = 0; lengthcount > -maxLength; lengthcount--)
        {
            Status result = OnMassCheck(playernum, massobjects, massstatuses, length + lengthcount, side, instanceobj);
            if (result == Status.Enemy)
            {
                break;
            }
        }
    }
    void BishopMove(int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int length, int side, GameObject[] instanceobj)
    {
        //左上の検索
        for (int lengthcount = 0; lengthcount < maxLength; lengthcount++)
        {
            int sidecount = -lengthcount;
            Status result = OnMassCheck(playernum, massobjects, massstatuses, length + lengthcount, side + sidecount, instanceobj);
            if (result == Status.Enemy)
            {
                break;
            }
        }

        //右上の検索
        for (int lengthcount = 0; lengthcount < maxLength; lengthcount++)
        {
            int sidecount = lengthcount;
            Status result = OnMassCheck(playernum, massobjects, massstatuses, length + lengthcount, side + sidecount, instanceobj);
            if (result == Status.Enemy)
            {
                break;
            }
        }

        //右下の検索
        for (int lengthcount = 0; lengthcount > maxLength; lengthcount--)
        {
            int sidecount = lengthcount;
            Status result = OnMassCheck(playernum, massobjects, massstatuses, length + lengthcount, side + sidecount, instanceobj);
            if (result == Status.Enemy)
            {
                break;
            }
        }

        //左下の検索
        for (int lengthcount = 0; lengthcount > maxLength; lengthcount--)
        {
            int sidecount = -lengthcount;
            Status result = OnMassCheck(playernum, massobjects, massstatuses, length + lengthcount, side + sidecount, instanceobj);
            if (result == Status.Enemy)
            {
                break;
            }
        }
    }

    Status OnMassCheck(int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int lengthsum, int sidesum, GameObject[] instanceobj)
    {
        bool result = OutSideLength(sidesum, lengthsum);
        int getplayernumber = 0;
        if (result)
        {
            GameObject character = massstatuses[lengthsum, sidesum].GetCharacterObj();
            if (character != null)//キャラクターの所属先のプレイヤー番号の取得
            {
                getplayernumber = character.GetComponent<SummonStatus>().GetPlayer();
            }
            else if (character == null)//マスにキャラクターがいなかった場合
            {
                MassOnNotCharacter(lengthsum, sidesum, massobjects, massstatuses, instanceobj);
                return Status.None;
            }

            if (getplayernumber != playernum)
            {
                MassOnEnemyCharacter(lengthsum, sidesum, massobjects, massstatuses, instanceobj);
                return Status.Enemy;
            }
        }
        return Status.Nothing;
    }
    /// <summary>
    /// 最初にポーンを動かす時の処理
    /// </summary>
    void FirstPornMove(int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int length, int side, GameObject[] instanceobj)
    {
        int num = 0;
        int value = 0;
        int lengthsum = 0;
        if (playernum == 1)
        {
            value = 1;
        }
        else
        {
            value = -1;
        }
        if (value == 1)
        {
            for (int count = 0; count <= 2; count += value)
            {
                lengthsum = count + length;
                bool result = OutSideLength(lengthsum, side);
                if (result)
                {
                    GameObject character = massstatuses[lengthsum, side].GetCharacterObj();
                    if (character != null)//キャラクターの所属先のプレイヤー番号の取得
                    {
                        num = character.GetComponent<SummonStatus>().GetPlayer();
                    }
                    if (character == null)//マスにキャラクターがいなかった場合
                    {
                        MassOnNotCharacter(lengthsum, side, massobjects, massstatuses, instanceobj);
                    }
                    if (playernum != num)//マスにエネミーがいた場合
                    {
                        lengthsum++;
                        break;
                    }
                }
            }
        }
        else if (value == -1)
        {
            for (int count = 0; count >= -2; count += value)
            {
                lengthsum = count + length;
                bool result = OutSideLength(lengthsum, side);
                if (result)
                {
                    GameObject character = massstatuses[lengthsum, side].GetCharacterObj();
                    if (character != null)//キャラクターの所属先のプレイヤー番号の取得
                    {
                        num = character.GetComponent<SummonStatus>().GetPlayer();
                    }
                    if (character == null)//マスにキャラクターがいなかった場合
                    {
                        MassOnNotCharacter(lengthsum, side, massobjects, massstatuses, instanceobj);
                    }
                    if (playernum != num)//マスにエネミーがいた場合
                    {
                        lengthsum++;
                        break;
                    }
                }
            }
        }

        for (int count = -1; count <= 1; count++)//ポーンの横の攻撃範囲索敵
        {
            int sidesum = count + side;
            bool result = OutSideLength(lengthsum, sidesum);
            if (result && count != 0)
            {
                GameObject character = massstatuses[lengthsum, sidesum].GetCharacterObj();
                if (character != null)
                {
                    int getplayernum = character.GetComponent<SummonStatus>().GetPlayer();
                    if (getplayernum != playernum)
                    {
                        MassOnEnemyCharacter(lengthsum, sidesum, massobjects, massstatuses, instanceobj);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 縦と横のマスの範囲外ではないかをチェックする処理
    /// </summary>
    bool OutSideLength(int length, int side)
    {
        if (length >= maxLength || length < 0)
        {
            return false;
        }

        else if (side >= maxLength || side < 0)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// マス上にキャラクターが存在しなかった時
    /// </summary>
    void MassOnNotCharacter(int length, int side, GameObject[,] massobjects, MassStatus[,] massstatuses, GameObject[] instanceobj)
    {
        Vector3 pos = massobjects[length, side].transform.position;
        pos.z -= 0.5f;
        massstatuses[length, side].SetMaterial(4);
        massstatuses[length, side].SetMassStatus(BoardManager.MassMoveStatus.None);
        AddUpdateMoveList(massstatuses[length, side]);
    }

    /// <summary>
    /// マス上に敵のキャラクターが存在した時の処理
    /// </summary>
    void MassOnEnemyCharacter(int length, int side, GameObject[,] massobjects, MassStatus[,] massstatuses, GameObject[] instanceobj)
    {
        Vector3 pos = massobjects[length, side].transform.position;
        pos.z -= 0.5f;
        massstatuses[length, side].SetMaterial(3);
        massstatuses[length, side].SetMassStatus(BoardManager.MassMoveStatus.Enemy);
        AddUpdateMoveList(massstatuses[length, side]);
    }

    /// <summary>
    /// 変更したマスをリストに追加
    /// </summary>
    void AddUpdateMoveList(MassStatus status)
    {
        boardManagerscript.AddUpdateMoveList(status);
    }
}
