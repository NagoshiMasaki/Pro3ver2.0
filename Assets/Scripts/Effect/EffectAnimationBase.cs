using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimationBase : MonoBehaviour
{
    public virtual void UpdateAnimation(){ }
    public virtual void Ini() { }
    public virtual void Ini(BattleManagerAnimation set) { }
}
