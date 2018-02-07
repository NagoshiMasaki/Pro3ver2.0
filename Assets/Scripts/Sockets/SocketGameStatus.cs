using UnityEngine;
using System.IO;
using System.Collections.Generic;

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
    List<string> recvDataList;
    public static string inidata = "";//初期のカードなどの情報を渡す
    public static string kingdata = "";
    public static string recvinhanddata;
    public static string senddata;
    public static string recvdata;
    [SerializeField]
    List<SocketAction.GameStatus> gameStatusList = new List<SocketAction.GameStatus>();

    public List<SocketAction.GameStatus> GameStatusList { get { return gameStatusList; } set { gameStatusList = value; } }
    public List<string> RecvDataList { get { return recvDataList; } set { recvDataList = value; } }

    public DeckManager DeckManagerScript { get { return deckManagerScript; } }
    public SituationManager SituationManagerScript { get { return situationManagerScript; } }
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
