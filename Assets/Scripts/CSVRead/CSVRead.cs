using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public class CSVRead : MonoBehaviour {

    [SerializeField]
    string fileName;
    [SerializeField]
    DeckClass deckClassScript;
     void Start()
    {
        StreamReader sr = new StreamReader(Application.dataPath + fileName, Encoding.GetEncoding("shift_jis"));
        while (sr.Peek() >= 0)
        {
            string[] cols = sr.ReadLine().Split(',');
            int col = int.Parse(cols[0]);
            Debug.Log(col);
            deckClassScript.SetCharacter(col);
        }
        deckClassScript.IniShaffle();
    }


}
