using UnityEngine;
using System.IO;

public class SocketGameStatus : MonoBehaviour
{
    [SerializeField]
    DeckClass deckclassScript;//敵
    [SerializeField]
    DeckClass playerDeckClassScript;
    [SerializeField]
    string fileName;
    StringReader deckdData;
    [SerializeField]
    CSVRead csvreadScript;
    string copyRecvData;
    [SerializeField]
    SituationManager situationManagerScript;
    [SerializeField]
    DeckManager deckManagerScript;
    public static string inidata = "";//初期のカードなどの情報を渡す
    public static string kingdata = "";
    public DeckManager DeckManagerScript { get { return deckManagerScript; } }
    public SituationManager SituationManagerScropt { get { return situationManagerScript; } }
    public string CopyRecvData { get { return copyRecvData; } set { copyRecvData = value; } }
    public DeckClass DeckClassScript { get { return deckclassScript; }}
    public CSVRead CsvReadScript { get { return csvreadScript; } }
    public StringReader DeckData { get { return deckdData; } set { deckdData = value; } }
    public string FileName { get { return fileName; } }


    public DeckClass GetDeckClassScript()
    {
        return deckclassScript;
    }

    public DeckClass GetPlayerDeckClassScript()
    {
        return playerDeckClassScript;
    }
}
