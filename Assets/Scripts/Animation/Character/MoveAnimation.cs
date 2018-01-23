////////////////////////////////
////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour {

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float scaleSpeed;
    float copyScaleSpeed;
    [SerializeField]
    int scaleanimationCount;
    int copyScaleAnimationCount;
    GameObject targetObject;
    Vector3 diff;
    Vector3 targetPoint;
    [SerializeField]
    AnimationManager animationManagerScript;
    bool isAnimation = false;
    public enum Status
    {
        None,
        LargeScaleAnimaiton,
        MoveAnimation,
        SmallAnimation
    }
    Status status;

    public bool isGetAnimation()
    {
        return isAnimation;
    }

    public void AnimationComplete()
    {
        animationManagerScript.SePlay(SeNumbers.SUMMON_MOVE);
        targetObject.transform.position = targetPoint;
        enabled = false;
    }

    public void SetTarget(GameObject targetobj,Vector3 targetpoint)
    {
        targetObject = targetobj;
        targetPoint = targetpoint;
        scaleanimationCount = copyScaleAnimationCount;
        status = Status.LargeScaleAnimaiton;
        isAnimation = true;
        enabled = true;
    }

	void Start ()
    {
        copyScaleSpeed = scaleSpeed;
        copyScaleAnimationCount = scaleanimationCount;
        enabled = false;
	}
	
	void Update ()
    {
        switch (status)
        {
            case Status.LargeScaleAnimaiton:
            case Status.SmallAnimation:
                Scale();
                break;
            case Status.MoveAnimation:
                Move();
                break;
        }
	}

    void Scale()
    {
        Vector3 copyscale = targetObject.transform.localScale;
        copyscale.x += scaleSpeed;
        copyscale.y += scaleSpeed;
        targetObject.transform.localScale = copyscale;
        scaleanimationCount--;
        if(scaleanimationCount <= 0)
        {
            if(status == Status.LargeScaleAnimaiton)
            {
                scaleanimationCount = copyScaleAnimationCount;
                status = Status.MoveAnimation;
            }

            else if(status == Status.SmallAnimation)
            {
                isAnimation = false;
                animationManagerScript.SePlay(SeNumbers.SUMMON_MOVE);
                targetObject.transform.position = targetPoint;
                enabled = false;
            }
            scaleSpeed = -scaleSpeed;
        }
    }

    void Move()
    {
        Vector3 diff = (targetPoint - targetObject.transform.position).normalized;
        targetObject.transform.position += diff * moveSpeed;
        if (targetObject.transform.position.x <targetPoint.x + 0.5f && targetObject.transform.position.x > targetPoint.x - 0.05f)
        {
            if (targetObject.transform.position.y < targetPoint.y + 0.5f && targetObject.transform.position.y > targetPoint.y - 0.05f)
            {
                status = Status.SmallAnimation;
            }
        }
    }
}
