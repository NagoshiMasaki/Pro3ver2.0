using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachCard : MonoBehaviour {

    [SerializeField]
    SpriteRenderer mysprite;
    
    public void SetSprite(Sprite set)
    {
        mysprite.sprite = set;
    }
}
