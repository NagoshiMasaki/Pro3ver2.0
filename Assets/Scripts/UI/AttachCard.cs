using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachCard : MonoBehaviour {

    [SerializeField]
    SpriteRenderer mysprite;
    [SerializeField]
    SituationManager situationManagerScript;
    [SerializeField]
    Vector3 pos2;
    Vector3 defaultPos;
    void Start()
    {
        defaultPos = transform.position;
    }

    public void SetSprite(Sprite set)
    {
        if (situationManagerScript.GetPlayerTurn() == 1)
        {
            transform.position = defaultPos;
        }
        else
        {
            transform.position = pos2;
        }
        mysprite.sprite = set;

    }
}
