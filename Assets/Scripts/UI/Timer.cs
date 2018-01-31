using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float z;
    [SerializeField]
    SpriteRenderer spriteRender;
    [SerializeField]
    GameObject maskObj;
    Vector3 defaultScale;
    [SerializeField]
    float time;
    float copyTime;
    [SerializeField]
    int speedValue;
    [SerializeField]
    Text timerText;
    [SerializeField]
    UImanager uiManagerScript;
    [SerializeField]
    GameObject timerObj;
    int frameCount = 0;

    public void Ini()
    {
        defaultScale = maskObj.transform.localScale;
        copyTime = time;
    }

    void Update()
    {
        time -= Time.deltaTime * speedValue;
        frameCount++;
        timerText.text = time.ToString("F00");
        if (time <= 0.0f)
        {
            uiManagerScript.TurnChange();
        }
        if (frameCount >= 3)
        {
            frameCount = 0;
            z += rotationSpeed;
            timerObj.transform.rotation = Quaternion.Euler(0, 0, z);
            if (z <= 0)
            {
                spriteRender.color = Color.blue;
                Vector3 save = maskObj.transform.localScale;
                save.x = -save.x;
                save.y = defaultScale.y;
                save.z = defaultScale.z;
                maskObj.transform.localScale = save;
                rotationSpeed = -rotationSpeed;
            }

            else if (z >= 180.0f)
            {
                spriteRender.color = Color.red;
                Vector3 save = maskObj.transform.localScale;
                save.x = -save.x;
                save.y = defaultScale.y;
                save.z = defaultScale.z;
                maskObj.transform.localScale = save;
                rotationSpeed = -rotationSpeed;
            }
        }
    }
    public void Reset()
    {
        time = copyTime;
    }
}
