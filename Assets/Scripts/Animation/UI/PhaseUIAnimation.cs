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

    public void Ini()
    {
        copyAnimationCount = animationCount;
        copyScale = transform.localScale;
        enabled = false;
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
    }

    public void StartAnimation()
    {
        enabled = true;
        transform.localScale = copyScale;
    }
}
