/////////////////////////////////////////
//制作者　名越大樹
//クラス　キャラクターのスキルの基底クラス
/////////////////////////////////////////
using UnityEngine;

public class CharacterSkill : MonoBehaviour {

    SummonStatus summonstatus;
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
    public virtual void AntecedentAttack() { }//先行攻撃
    public virtual void EnemyMoveEndSkill(SummonStatus character) {}
    public virtual SummonStatus GetCharacter() { return summonstatus; }
}
