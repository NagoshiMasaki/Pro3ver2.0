using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogUI : MonoBehaviour
{
    [SerializeField]
    int maxlogsize;
    List<string> logList = new List<string>();
    List<GameObject> textList = new List<GameObject>();
    [SerializeField]
    GameObject textObj;
    [SerializeField]
    GameObject ParentText;
    [SerializeField]
    GameObject targetObj;
    int num = 0;

    public void LogUpdate(string set)
    {
        logList.Add(set);
        GameObject instanceobj = Instantiate(textObj, targetObj.transform.position, Quaternion.identity);
        instanceobj.GetComponent<Text>().text = set;
        instanceobj.transform.parent = targetObj.transform;
        textList.Add(instanceobj);
        if (logList.Count > maxlogsize)
        {
            logList.RemoveAt(0);
            textList.RemoveAt(0);
            Destroy(textList[0]);
        }
    }


}
