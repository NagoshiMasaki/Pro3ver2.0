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

    public void GetMoveData(Rate rate, int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int nowlengthmass, int nowsidemass, GameObject[] instanceobj)
    {
        switch (rate)
        {
            case Rate.Porn:
                PornMove();
                break;
            case Rate.Queen:
                QueenMove();
                break;
            case Rate.Night:
                NightMove();
                break;
            case Rate.Luke:
                LukeMove();
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

    void PornMove()
    {

    }

    void PornsMove()
    {

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

    void LukeMove()
    {

    }
    void BishopMove()
    {

    }
    /// <summary>
    /// 最初にポーンを動かす時
    /// </summary>
    void FirstPornMove(int playernum, GameObject[,] massobjects, MassStatus[,] massstatuses, int length, int side, GameObject[] instanceobj)
    {
        int num = 0;
        int value = 0;
        if (playernum == 1)
        {
            value = 1;
        }
        else
        {
            value = -1;
        }
        for (int count = 0; count <= 2; count += value)
        {
            int lengthsum = count + length;
            bool result = OutSideLength(length + count, side);
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
                else if (playernum != num)//マスにエネミーがいた場合
                {
                    MassOnEnemyCharacter(lengthsum, side, massobjects, massstatuses, instanceobj);
                }
            }
        }
    }

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

    void MassOnNotCharacter(int length, int side, GameObject[,] massobjects, MassStatus[,] massstatuses, GameObject[] instanceobj)
    {
        Vector3 pos = massobjects[length,side].transform.position;
        pos.z -= 0.5f;
        Instantiate(instanceobj[0], pos, Quaternion.identity);
        massstatuses[length, side].SetMassStatus(BoardManager.MassMoveStatus.None);
    }

    void MassOnEnemyCharacter(int length, int side, GameObject[,] massobjects, MassStatus[,] massstatuses, GameObject[] instanceobj)
    {
        Vector3 pos = massobjects[length, side].transform.position;
        pos.z -= 0.5f;
        Instantiate(instanceobj[1], pos, Quaternion.identity);
        massstatuses[length, side].SetMassStatus(BoardManager.MassMoveStatus.Enemy);
    }
}
