using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ini : MonoBehaviour
{
    [SerializeField]
    BoardAction boardAcrionScript;
    [SerializeField]
    ReadData readDataScript;
    int count = 10;
    int copycCount;
    [SerializeField]
    Timer timerScript;
    [SerializeField]
    BattleActionAnimation battleActionAnimationScript;
    [SerializeField]
    AttachCard attachCardScript;
    [SerializeField]
    SummonAnimation summonAnimationScript;
    [SerializeField]
    PhaseUIAnimation phaseUIAnimationScript;
    [SerializeField]
    PhaseUI phaseUIScript;
    [SerializeField]
    DrawCardAnimation drawCardAnimationScript;
    public enum Status
    {
        None,
        IniSetting,
        Board,
        Deck,
        CopyScirpts,
        End,
    }
    Status status = Status.IniSetting;
    public void IniStart()
    {
        copycCount = count;
        enabled = true;
    }

    void Update()
    {
        Count();
    }

    void Count()
    {
        count--;
        if (count <= 0)
        {
            Instance();
        }
    }

    void Instance()
    {
        switch (status)
        {
            case Status.IniSetting:
                IniSetting();
               // boardAcrionScript.Ini();
                status = Status.Deck;
                break;
            case Status.Deck:
                boardAcrionScript.Ini();
                readDataScript.Ini();
                status = Status.CopyScirpts;
                break;
            case Status.CopyScirpts:
                IniSetting();
                status = Status.End;
                break;
            case Status.End:
                Destroy(this);
                break;
        }
        ResetCount();
    }

    void IniSetting()
    {
        drawCardAnimationScript.Ini();
        attachCardScript.Ini();
        summonAnimationScript.Ini();
        phaseUIAnimationScript.Ini();
        phaseUIScript.Ini();
    }

    void ResetCount()
    {
        count = copycCount;
    }

}
