////////////////////////////////////
//制作者　名越大樹
//クラス　キャラクターのスキルを実行を管理する処理
////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStatus : MonoBehaviour {

    [SerializeField]
    List<CharacterSkill> invokerList = new List<CharacterSkill>();
    public enum Status
    {
        None,
        Continuation,
        Finish
    }
    [SerializeField]
    List<CharacterSkill> skillList = new List<CharacterSkill>();
    Status status;
    CharacterSkill invorker;

    public void AddList(CharacterSkill set)
    {
        invokerList.Add(set);
    }

    public void RemoveatPhassiveSkillList(CharacterSkill target)
    {
        for(int count = 0; count < skillList.Count;count++ )
        {
            if(target == skillList[count])
            {
                skillList.RemoveAt(count);
            }
        }
    }

    public void AddSkillList(CharacterSkill set)
    {
        skillList.Add(set);
    }

    public List<CharacterSkill> GetSkillList()
    {
        return skillList;
    }
    public void RemoveList(CharacterSkill target)
    {
        for(int count = 0;count < invokerList.Count;count++)
        {
            if(invokerList[count] == target)
            {
                invokerList.RemoveAt(count);
            }
        }
    }

    public List<CharacterSkill> GetInvokerList()
    {
        return invokerList;
    }

    public Status GetStatus()
    {
        return status;
    }

    public void SetStatus(Status set)
    {
        status = set;
    }

    public CharacterSkill GetInvorker()
    {
        return invorker;
    }

}

