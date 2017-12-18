using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPAPAction : MonoBehaviour
{

    [SerializeField]
    float spacex;
    [SerializeField]
    float spacey;
    [SerializeField]
    float startspacex;
    [SerializeField]
    SPAPStatus spapStatus;

    public void Ini(SPAPStatus spapStatusScript, int number, SPAPManager spapmanager)
    {
        spapStatus = spapStatusScript;
        spapStatus.Ini(spapmanager);
        spapStatus.SetSP();
        if (number == 2)
        {
            spacey = -spacey;
            spacex = -spacex;
            startspacex = -startspacex;
        }

        int max = spapStatusScript.GetMaxSP();
        var obj = spapStatusScript.GetLeverObj();
        var sp = spapStatusScript.GetSPObj();
        Vector3 pos = obj.transform.position;
        pos.y += spacey;
        pos.x += startspacex;
        GameObject instanceobj = null;
        for (int count = 0; count < max; count++)
        {
            instanceobj = Instantiate(sp, pos, Quaternion.identity);
            pos.x += spacex;
            SpSprite spsprite = instanceobj.GetComponent<SpSprite>();
            spapStatusScript.spListAdd(spsprite);
            int inisp = spapStatusScript.GetIniSp();
            SpSprite.Status status = SpSprite.Status.None;
            if (inisp > count)
            {
                status = SpSprite.Status.Use;
            }
            spsprite.Ini(spapmanager, status);
        }
    }

    public void UpdateSPSprite(int usecount)
    {
        int sp = spapStatus.GetSP();
        int max = spapStatus.GetMaxSP();
        List<SpSprite> list = spapStatus.GetSPSpriteList();
        int index = sp -1 ;
        for (int count = 0; count < usecount; count++)
        {
            list[index].SetStatsu(SpSprite.Status.Used);
            index--;
        }
    }

    public void ResetSP()
    {
        List<SpSprite> list = spapStatus.GetSPSpriteList();
        int sp = spapStatus.GetSP();
        Debug.Log(sp);
        for (int count = 0; count < sp; count++)
        {
            list[count].SetStatsu(SpSprite.Status.Use);
        }
    }

    public void SetIsAnimation(int usecount)
    {
        int sp = spapStatus.GetSP();
        int max = spapStatus.GetMaxSP();
        List<SpSprite> list = spapStatus.GetSPSpriteList();
        int index = sp - 1;
        for (int count = 0; count < usecount; count++)
        {
            list[index].SetIsAnimation(true);
            index--;
        }
    }

    public void ResetIsAnimation(int usecount)
    {
        int sp = spapStatus.GetSP();
        int max = spapStatus.GetMaxSP();
        List<SpSprite> list = spapStatus.GetSPSpriteList();
        for (int count = 0; count < list.Count; count++)
        {
            list[count].SetIsAnimation(false);
        }
    }
}
