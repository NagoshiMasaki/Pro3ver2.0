using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAnimation : MonoBehaviour
{

    SummonStatus summonObj;
    MassStatus setMassStatus;
    IllustrationStatus summonIllustObj;
    [SerializeField]
    SpriteRenderer centerSprite;
    [SerializeField]
    float scaleTimer;
    float copyScaleTimer;
    Vector3 copyillustScale;
    int playerNumber;
    [SerializeField]
    AnimationManager animationManagerScript;
    GameObject illusttarget;
    [SerializeField]
    GameObject effectObj;
    GameObject instanceeffectobj;
    [SerializeField]
    float waitEffectTime;
    float copyWaitEffectTime;
    [SerializeField]
    float moveEffectValue;
    [SerializeField]
    GameObject scatterEffectObj;
    public enum Status
    {
        None,
        Scale,
        EffectWait,
        EffectMove,
        Move,
        InstanceEffect,
    }
    [SerializeField]
    Status status;

    void Start()
    {
        copyScaleTimer = scaleTimer;
        copyWaitEffectTime = waitEffectTime;
        enabled = false;
    }

    public void StartAnimation(SummonStatus summon, MassStatus mass, IllustrationStatus illust, int playernumber)
    {
        playerNumber = playernumber;
        status = Status.Scale;
        summonObj = summon;
        setMassStatus = mass;
        scaleTimer = copyScaleTimer;
        summonIllustObj = illust;
        copyillustScale = illust.transform.localScale;
        illusttarget = illust.gameObject;
        enabled = true;
        waitEffectTime = copyWaitEffectTime;
    }

    void Update()
    {
        switch (status)
        {
            case Status.Scale:
                IllustScale();
                break;
            case Status.EffectMove:
                EffectMove();
                break;
            case Status.EffectWait:
                EffectWait();
                break;
        }
    }

    void IllustScale()
    {
        Vector3 copyscale = summonIllustObj.transform.localScale;
        copyscale.x += Time.deltaTime;
        copyscale.y += Time.deltaTime;
        summonIllustObj.transform.localScale = copyscale;
        scaleTimer -= Time.deltaTime;
        if (scaleTimer <= 0.0f)
        {
            instanceeffectobj = Instantiate(effectObj, illusttarget.transform.position, Quaternion.identity);
            animationManagerScript.ReMoveIllustCard(playerNumber, illusttarget);
            status = Status.EffectWait;
        }
    }

    void EffectWait()
    {
        waitEffectTime -= Time.deltaTime;
        if (waitEffectTime <= 0.0f)
        {
            status = Status.EffectMove;
        }
    }

    void EffectMove()
    {
        Vector3 diff = (summonObj.transform.position - instanceeffectobj.transform.position).normalized;
        instanceeffectobj.transform.position += diff * moveEffectValue;
        if(instanceeffectobj.transform.position.x  < summonObj.transform.position.x + 1 && instanceeffectobj.transform.position.x > summonObj.transform.position.x - 1)
        {
            status = Status.None;
            summonObj.gameObject.SetActive(true);
            Instantiate(scatterEffectObj,summonObj.transform.position,Quaternion.identity);
            Destroy(instanceeffectobj);
            enabled = false;
        }
    }
}
