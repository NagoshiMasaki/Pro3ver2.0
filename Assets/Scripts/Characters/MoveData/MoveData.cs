using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveData : MonoBehaviour
{
    [SerializeField]
    int maxLength;
    [SerializeField]
    int maxSide;
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
                QueenMove();
                break;
            case Rate.Night:
                NightMove();
                break;
            case Rate.Luke:
                LukeMove(playernum, massobjects, massstatuses, nowlengthmass, nowsidemass, instanceobj);
                break;
            case Rate.Bishop:
                BishopMove();
                break;
            case Rate.King:
                KingMove();
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
        for(int count =-1; count <= 1;count++)
        {
            int sidesum = side + count;
           bool result = OutSideLength(length,sidesum);
            if(result)
            {
                int num = 0;
                GameObject character = massstatuses[length, sidesum].GetCharacterObj();
                if (character != null)//キャラクターの所属先のプレイヤー番号の取得
                {
                    num = character.GetComponent<SummonStatus>().GetPlayer();
                }
                if(character == null)
                {
                    MassOnNotCharacter(length,sidesum,massobjects,massstatuses,instanceobj);
                }

                else if(playernum != num)
                {
                    MassOnEnemyCharacter(length,sidesum,massobjects,massstatuses,instanceobj);
                }
            }
        }
    }

    void NightMove()
    {

    }

    void QueenMove()
    {

    }
    void KingMove()
    {

    }

    void LukeMove(int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int length, int side, GameObject[] instanceobj)
    {

    }
    void BishopMove()
    {

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
            for (int count = 1; count <= 2; count += value)
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
        Instantiate(instanceobj[0], pos, Quaternion.identity);
        massstatuses[length, side].SetMassStatus(BoardManager.MassMoveStatus.None);
        AddUpdateMoveList(massstatuses[length,side]);
    }

    /// <summary>
    /// マス上に敵のキャラクターが存在した時の処理
    /// </summary>
    void MassOnEnemyCharacter(int length, int side, GameObject[,] massobjects, MassStatus[,] massstatuses, GameObject[] instanceobj)
    {
        Vector3 pos = massobjects[length, side].transform.position;
        pos.z -= 0.5f;
        Instantiate(instanceobj[1], pos, Quaternion.identity);
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
