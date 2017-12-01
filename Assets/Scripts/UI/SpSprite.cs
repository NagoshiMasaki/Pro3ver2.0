using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpSprite : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer spSprite;
    SPAPManager spapManagerScript;
    public enum Status
    {
        None,
        Use,
        Used
    }
    Status status;

    public void Ini(SPAPManager set, Status statusset)
    {
        spapManagerScript = set;
        status = statusset;
        Spriteupdate();
    }

    public Status GetStatus()
    {
        return status;
    }

    public void SetStatsu(Status set)
    {
        status = set;
        Spriteupdate();
    }

    void Spriteupdate()
    {
        Sprite sprite = spapManagerScript.GetSPSpriteList((int)status);
        spSprite.sprite = sprite;
    }
}
