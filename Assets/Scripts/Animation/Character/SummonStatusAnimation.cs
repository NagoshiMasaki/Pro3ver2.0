////////////////////////////////////////////////////
//制作者　名越大樹
//戦闘シーンでアニメーションするカードのクラス
////////////////////////////////////////////////////

using UnityEngine;

public class SummonStatusAnimation : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer characterSprite;
    [SerializeField]
    SpriteRenderer hpNumberSprite;
    [SerializeField]
    SpriteRenderer attackNumberSprite;
    [SerializeField]
    SpriteRenderer frameSprite;

    public void IniSetColor(Color inicolor)
    {
        characterSprite.color = inicolor;
        hpNumberSprite.color = inicolor;
        attackNumberSprite.color = inicolor;
        frameSprite.color = inicolor;
    }
    public void SetHpNumberSprite(Sprite set)
    {
        hpNumberSprite.sprite = set;
    }

    public Color GetCharacterColor()
    {
        return characterSprite.color;
    }

    public void UpdateColor(Color set)
    {
        characterSprite.color = set;
        hpNumberSprite.color = set;
        attackNumberSprite.color = set;
        frameSprite.color = set;
    }

    public void CopySetSprite(Sprite character, Sprite hp, Sprite attack,Sprite frame)
    {
        characterSprite.sprite = character;
        hpNumberSprite.sprite = hp;
        attackNumberSprite.sprite = attack;
        frameSprite.sprite = frame;
    }
}
