using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField]
    float time;
    float copyTime;
    [SerializeField]
    int speedValue;
    [SerializeField]
    Text timerText;
    [SerializeField]
    UImanager uiManagerScript;
    void Start()
    {
        copyTime = time;
    }

    void Update()
    {
        time -= Time.deltaTime * speedValue;
        timerText.text = time.ToString("F00");
        if (time <= 0.0f)
        {
            uiManagerScript.TurnChange();
        }
    }

    public void Reset()
    {
        time = copyTime;
    }
}
