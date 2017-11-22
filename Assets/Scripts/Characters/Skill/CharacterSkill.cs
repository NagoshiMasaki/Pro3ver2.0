/////////////////////////////////////////
//制作者　名越大樹
//クラス　キャラクターのスキルの基底クラス
/////////////////////////////////////////
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
    public virtual void EnemyMoveEnd(SummonStatus character) {}
    public virtual void ActiveSkill(){}
    public virtual void EnemyTurnStart() { }
    public virtual void EnemyTurnEnd() { }
    public virtual void Passhive(SummonStatus enemy) { }
    public virtual void AttachSkillCharacter(SummonStatus target) {}
    public virtual void KingSkill() { }
    public virtual void TurnEnd() { }
}
