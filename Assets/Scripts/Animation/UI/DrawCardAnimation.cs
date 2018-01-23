using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardAnimation : MonoBehaviour
{
    public enum Status
    {
        None,
        Move,
        Reverse,
    }
    Status status;
    GameObject drawCardObj;
    Vector3 targetPos;
    Vector3 copyScale;
    [SerializeField]
    float moveValue;
    [SerializeField]
    float scaleValue;
    bool isScaleAction;
    [SerializeField]
    Vector3 cardScale;
    void Update()
    {
        AniamtionStatus();
    }

    void AniamtionStatus()
    {
        switch (status)
        {
            case Status.Move:
                Move();
                break;
        }
    }

    public void StartAnimation(GameObject drawcardobj, Vector3 target, GameObject deckobj)
    {
        drawCardObj = drawcardobj;
        targetPos = target;
        copyScale = drawcardobj.transform.localScale;
        drawCardObj.transform.localScale = copyScale;
        status = Status.Move;
        isScaleAction = true;
        enabled = true;

    }

    private void Move()
    {
        Vector3 diff = (targetPos - drawCardObj.transform.position).normalized;
        drawCardObj.transform.position += diff * moveValue;
        Vector3 copyscale = drawCardObj.transform.localScale;
        if (isScaleAction)
        {
            copyScale.x += Time.deltaTime * scaleValue;
            copyScale.y += Time.deltaTime * scaleValue;
            drawCardObj.transform.localScale = copyScale;
            if(drawCardObj.transform.localScale.x <= copyScale.x)
            {
                drawCardObj.transform.localScale = copyScale;
                isScaleAction = false;
            }
        }
        if (drawCardObj.transform.position.x < targetPos.x + 0.1f && drawCardObj.transform.position.x > targetPos.x - 0.1f)
        {
            drawCardObj.GetComponent<IllustrationStatus>().ResetScale();
            status = Status.None;
            enabled = false;
        }
    }
}
