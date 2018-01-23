using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseUIAnimation : MonoBehaviour
{

    Vector3 copyScale;
    [SerializeField]
    float addValueX;
    [SerializeField]
    float addValueY;
    [SerializeField]
    int animationCount;
    int copyAnimationCount;

    void Start()
    {
        copyAnimationCount = animationCount;
        copyScale = transform.localScale;
    }

    void Update ()
    {
        TextAnimation();
	}

    void TextAnimation()
    {
            Vector3 scale = transform.localScale;
            scale.x += Time.deltaTime * addValueX;
            scale.y += Time.deltaTime * addValueY;
            transform.localScale = scale;
            animationCount--;
        if(animationCount <= 0)
        {

        }
    }

    public void StartAnimation()
    {
        enabled = true;
        transform.localScale = copyScale;
    }
}
