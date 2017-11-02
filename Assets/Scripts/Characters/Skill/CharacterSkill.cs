using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : MonoBehaviour {

    public virtual void MyTurnStart() { }
    public virtual void MyTurnEnd() { }
    public virtual void BattleStart() { }
    public virtual void BattleEnd() { }
    public virtual void MoveStart() { }
    public virtual void MoveEnd() { }

}
