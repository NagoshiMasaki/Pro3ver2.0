/////////////////////////////
//製作者　名越大樹
//クラス　フォーマルハウトのスキルに関するクラス
/////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formalhout : CharacterSkill {

    [SerializeField]
    SummonStatus parentObj;
    public override void MoveEnd()
    {
        MassColorChange();
    }

    void MassColorChange()
    {
        Debug.Log("フォーマルハウトのスキル発動");
        MassStatus mass = parentObj.GetAttachMass();
        MassStatus attachmass = parentObj.GetCopyAttachMass();
        attachmass.SetMaterial(attachmass.GetDefaultNumber());
        int length = mass.GetLengthNumber();
        if (length == 2 || length == 3)
        {
            mass.SetMaterial(parentObj.GetPlayer());
        }
    }
}
