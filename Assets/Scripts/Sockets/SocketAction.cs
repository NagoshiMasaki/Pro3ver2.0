using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

public class SocketAction : MonoBehaviour
{
    Socket parentSock;
    [SerializeField]
    string ipaddress;
    [SerializeField]
    int port;
    Thread socketThread;
    public enum GameStatus
    {
        None,
        Ini,
        IniComplete,
        DeckRead,
        DeckReadComplete
    }
    GameStatus status;

    public enum SendStatus
    {
        None,
        Ini,
        Data,
    }

    public SendStatus sendstatus;

    static string sendData;
    static string readData;
    [SerializeField]
    GameMaster gameMasterScript;
    [SerializeField]
    SocketGameStatus socketGameStatusScript;

    bool hoge = true;
    void Start()
    {
        if (!gameMasterScript.GetIsNetWork())
        {
            return;
        }
            socketGameStatusScript.DeckData = socketGameStatusScript.CsvReadScript.ResourcesReadSocket();
            socketThread = new Thread(SocketIni);
            socketThread.Start();
        
    }

    void Update()
    {
        switch (status)
        {
            case GameStatus.Ini:
                gameMasterScript.Ini();
                Debug.Log("ゲームスタート");
                status = GameStatus.IniComplete;
                break;
            case GameStatus.DeckRead:
                status = GameStatus.DeckReadComplete;
                break;
            case GameStatus.DeckReadComplete:
                sendstatus = SendStatus.Ini;
                SocketStart();
                break;
        }
    }



    void Send()
    {
        string message = "Hello World";
        Debug.Log("メッセージを送るよ");
        SocketProduction.SockSend(parentSock, message);
        parentSock.Close();
        return;
    }


    void SocketIni()
    {
        parentSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("接続開始");
        SocketProduction.SockConnect(ref parentSock, IPAddress.Parse(ipaddress), port);
        Debug.Log("接続終了");
        if (parentSock.Connected)
        {
            Debug.Log("接続成功しました。");
        }
        else
        {
            Debug.Log("接続失敗しました。");
        }
        string data = SocketProduction.SockRecv(parentSock);
        RecvDataAnalysis(data);
        //        parentSock.Close();
        return;
    }

    public void SocketStart()
    {
        socketThread = new Thread(SendSocketData);
        socketThread.Start();
    }

    /// <summary>
    /// 送信処理
    /// </summary>
     void SendSocketData()
    {
        switch (sendstatus)
        {
            case SendStatus.Ini:
                sendData = "ini" + "/" + SocketGameStatus.inidata + SocketGameStatus.kingdata;
                break;
        }
        sendstatus = SendStatus.None;
        SocketProduction.SockSend(parentSock, sendData);
        return;
    }

    /// <summary>
    /// 受信処理
    /// </summary>
    void RecvSocket()
    {
        bool ret = true;

        while (ret)
        {

        }
    }

    void RecvDataAnalysis(string recvdata)
    {
        string[] spritdata = recvdata.Split('/');
        Debug.Log(spritdata[0] + "です");
        switch (spritdata[0])
        {
            case "pn":
                PlayerNumberSetting(int.Parse(spritdata[1]));
                break;
            case "deck":
                RecvDeckData(spritdata);
                status = GameStatus.Ini;
                break;
            case "ini":
                break;
        }
    }


    void SetCards(string[] data)
    {

    }

    void RecvDeckData(string[] recvData )
    {
        string data = "";
        for(int count = 1; count < recvData.Length; count++)
        {
            data += recvData[count] + "/";
        }
        socketGameStatusScript.CopyRecvData = data;
    }

    void PlayerNumberSetting(int num)
    {
        Debug.Log("プレイヤー番号を設定します");
        PlayerAction.PlayerNumber = num;
        SituationManager.TurnMasterNumber = num;
        DeckClass deckclassscipt = socketGameStatusScript.GetDeckClassScript();
        DeckClass playerdeckclassscipt = socketGameStatusScript.GetPlayerDeckClassScript();
        if (num == 1)
        {
            deckclassscipt.SetPlayerNumber(1);//相手のプレイヤー
        }
        else
        {
            deckclassscipt.SetPlayerNumber(2);//相手のプレイヤー
        }
        playerdeckclassscipt.SetPlayerNumber(num);
        RecvComplete();
        DeckSend();
    }


    /// <summary>
    /// デッキを読み取り送る処理
    /// </summary>
    void DeckSend()
    {
        Debug.Log("デッキのデータを送信します");
        string data = "";
        while (socketGameStatusScript.DeckData.Peek() >= 0)
        {
            string[] cols = socketGameStatusScript.DeckData.ReadLine().Split(',');
            data += cols[0] + "/";
        }
        Debug.Log(data);
        SocketProduction.SockSend(parentSock,data);
        Debug.Log("デッキのデータを送信しました");
        string deckdata = SocketProduction.SockRecv(parentSock);
        Debug.Log("デッキのデータを受信しました");
        RecvDataAnalysis(deckdata);
        RecvComplete();
        parentSock.Close();
    }

    public void DeckRead()
    {
        if(!hoge)
        {
            return;
        }
        hoge = false;
        Debug.Log("デッキを読み取ります");
        Debug.Log(socketGameStatusScript.CopyRecvData);
        string[] deckdata = socketGameStatusScript.CopyRecvData.Split('/');
        DeckClass deckclassscript = socketGameStatusScript.GetDeckClassScript();
        int playernumber = deckclassscript.GetPlayerNumber();
        int length = deckdata.Length - 2;
        for (int count = 0; count < length; count++)
        {
            if(deckdata[count] == "")
            {
                break;
            }
            Debug.Log(deckdata[count]);
            int col = int.Parse(deckdata[count]);
            socketGameStatusScript.DeckClassScript.SetCharacter(col, playernumber);
        }
        socketGameStatusScript.DeckClassScript.IniShaffle();
    }

    void RecvComplete()
    {
        Debug.Log("メッセージ返答を送信しました");
        string message = "ok";
        SocketProduction.SockSend(parentSock, message);
    }
}
