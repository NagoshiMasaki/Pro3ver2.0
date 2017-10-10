using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPAPStatus : MonoBehaviour
{

    [SerializeField]
    int SP;
    [SerializeField]
    int AP;
    [SerializeField]
    GameObject SPObj;
    [SerializeField]
    GameObject APObj;
    [SerializeField]
    GameObject SPPos;
    [SerializeField]
    GameObject APPos;
    Vector3 defaultAPPos;
    Vector3 defaultSPPos;
    List<GameObject> APList = new List<GameObject>();
    List<GameObject> SPList = new List<GameObject>();

    void Start()
    {
        defaultSPPos = SPPos.transform.position;
        defaultAPPos = APPos.transform.position;
    }

    public void AddSP()
    {
        SP++;
    }

    public void AddAP()
    {
        AP++;
    }

    public int GetAP()
    {
        return AP;
    }

    public int GetSP()
    {
        return SP;
    }

    public GameObject GetAPObj()
    {
        return APObj;
    }

    public GameObject GetSPObj()
    {
        return SPObj;
    }

    public void InstanceAP()
    {
        GameObject instanceobj = Instantiate(APObj, APPos.transform.position, Quaternion.identity);
        APList.Add(instanceobj);
        MoveAP();
    }

    public void InstanceSP()
    {
        GameObject instanceobj = Instantiate(SPObj, SPPos.transform.position, Quaternion.identity);
        SPList.Add(instanceobj);
        MoveSP();
    }

    void MoveAP()
    {
        Vector3 pos = APPos.transform.position;
        pos.x++;
        APPos.transform.position = pos;
    }

    void MoveSP()
    {
        Vector3 pos = SPPos.transform.position;
        pos.x++;
        SPPos.transform.position = pos;
    }

    void ResetAPPos()
    {
        APPos.transform.position = defaultAPPos;
    }

    void ResetSPPos()
    {
        SPPos.transform.position = defaultSPPos;
    }

    public void AllDestroyAP()
    {
        for (int count = 0; count < APList.Count; count++)
        {
            Destroy(APList[count]);
        }
        APList.Clear();
    }

    public void SubtractionAP(int num)
    {
        for (int count = num; count >0;count--)
        {
            Destroy(APList[APList.Count-1]);
            APList.RemoveAt(APList.Count);
            Vector3 pos = APPos.transform.position;
            pos.x--;
            APPos.transform.position = pos;
        }
    }

    public void SubtractionSP(int num)
    {
        for (int count = num; count > 0; count--)
        {
            Destroy(SPList[SPList.Count - 1]);
            SPList.RemoveAt(SPList.Count);
            Vector3 pos = SPPos.transform.position;
            pos.x--;
            SPPos.transform.position = pos;
        }
    }

    public void IniInstanceSP()
    {
        for(int count = 0; count < SP;count++)
        {
            InstanceSP();
        }
    }

    public void ResetApInstance()
    {
        AllDestroyAP();
        ResetAPPos();
        for(int count = 0;count < AP;count++)
        {
            InstanceAP();
        }
    }

    public void ResetSpInstance()
    {
        AllDestroyAP();
        ResetAPPos();
        for (int count = 0; count < SP; count++)
        {
            InstanceSP();
        }
    }
}
