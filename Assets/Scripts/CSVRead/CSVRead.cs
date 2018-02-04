using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public class CSVRead : MonoBehaviour
{
    [SerializeField]
    string fileName;
    [SerializeField]
    DeckClass deckClassScript;
    [SerializeField]
    int playernumber;

    public void Read()
    {
        StreamReader sr = new StreamReader(Application.dataPath + fileName, Encoding.GetEncoding("shift_jis"));
        while (sr.Peek() >= 0)
        {
            string[] cols = sr.ReadLine().Split(',');
            int col = int.Parse(cols[0]);
            deckClassScript.SetCharacter(col, playernumber);
        }
        deckClassScript.IniShaffle();
    }

    public void ResourcesRead()
    {
       var csv = Resources.Load(fileName) as TextAsset;
        StringReader read = new StringReader(csv.text);
        while (read.Peek() >= 0)
        {
            string[] cols = read.ReadLine().Split(',');
            int col = int.Parse(cols[0]);
            deckClassScript.SetCharacter(col, deckClassScript.GetPlayerNumber());
        }
        deckClassScript.IniShaffle();
    }


    public StringReader ResourcesReadSocket()
    {
        var csv = Resources.Load(fileName) as TextAsset;
        StringReader read = new StringReader(csv.text);
        return read;
    }

}
