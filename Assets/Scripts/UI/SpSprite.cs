using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpSprite : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer spSprite;
    SPAPManager spapManagerScript;
    [SerializeField]
    bool isAnimation;
    [SerializeField]
    bool isAddValue;
    [SerializeField]
    float colorValue;
    [SerializeField]
    float addValue;
    [SerializeField]
    int addSpeed;
    public enum Status
    {
        None,
        Use,
        Used
    }
    Status status;


    void Update()
    {
        if (isAnimation)
        {
            ColorAnimation();
        }
    }

    void ColorAnimation()
    {

        colorValue += addValue * Time.deltaTime * addSpeed;
        if(colorValue <=0.0f || colorValue >= 1.0f)
        {
            addValue = -addValue;
        }
        spSprite.color = new Color(colorValue, colorValue, colorValue, 1.0f);
    }

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

    public void SetIsAnimation(bool set)
    {
        isAnimation = set;
        if (!set)
        {
            ClearColor();
        }
    }

    void ClearColor()
    {
        spSprite.color = Color.white;
    }
    void Spriteupdate()
    {
        Sprite sprite = spapManagerScript.GetSPSpriteList((int)status);
        spSprite.sprite = sprite;
    }
}
