using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

public class SocketAction : MonoBehaviour
{
    Socket parentSock;
    [SerializeField]
    string ipaddress;
    [SerializeField]
    int port;
    Thread socketThread;
    Thread closeThread;
    public enum GameStatus
    {
        None,
        Ini,
        IniComplete,
        DeckRead,
        DeckReadComplete,
        IniDeckHandIDRecv,
        DrawCardID,
    }
    [SerializeField]
    GameStatus status;
    public enum SendStatus
    {
        None,
        Ini,
        Data,
        InstanceIdDeckHand,
        DrawId,
    }
    public enum RecvStatus
    {
        IniDeckHandIDRecv,
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
        if(socketGameStatusScript.GameStatusList.Count == 0)
        {
            return;
        }
        status = socketGameStatusScript.GameStatusList[0];

        socketGameStatusScript.GameStatusList.RemoveAt(0);
        Debug.Log(status);
        switch (status)
        {
            case GameStatus.Ini:
                gameMasterScript.Ini();
                Debug.Log("ゲームスタート");
                GameStatus gamestatus = GameStatus.IniComplete;
                socketGameStatusScript.GameStatusList.Add(gamestatus);
                break;
            case GameStatus.DeckRead:
                GameStatus deckread = GameStatus.DeckReadComplete;
                socketGameStatusScript.GameStatusList.Add(deckread);
                break;
            case GameStatus.DeckReadComplete:
                sendstatus = SendStatus.InstanceIdDeckHand;
                GameStatus nonestatus = GameStatus.None;
                socketGameStatusScript.GameStatusList.Add(nonestatus);
                SocketStart();
                break;
            case GameStatus.IniDeckHandIDRecv://受け取った初期の手札のidを振り分ける返す
                RecvDataAnalysis(RecvStatus.IniDeckHandIDRecv);
                SocketCloseStart();
                break;
            case GameStatus.DrawCardID:
                sendstatus = SendStatus.DrawId;
                SocketStart();
                break;
        }
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
            case SendStatus.InstanceIdDeckHand://
                sendData = "i" + "/" + SocketGameStatus.inidata;
                SocketProduction.SockSend(parentSock, sendData);
                sendData = "";
                string recvdata = SocketProduction.SockRecv(parentSock);
                RecvDataAnalysis(recvdata);
                break;
            case SendStatus.DrawId:
                string senddata = "dc/" + SocketGameStatus.senddata;
                SocketProduction.SockSend(parentSock,senddata);
                break;
        }
        return;
    }


    void RecvDataAnalysis(string recvdata)
    {
        string copyrecvdata = recvdata;
        string[] spritdata = recvdata.Split('/');
        Debug.Log(spritdata[0] + "です");
        switch (spritdata[0])
        {
            case "pn":
                Debug.Log("pnの処理をします");
                PlayerNumberSetting(int.Parse(spritdata[1]));
                break;
            case "deck":
                Debug.Log("decxの処理をします");
                RecvDeckData(spritdata);
                GameStatus inistatus = GameStatus.Ini;
                socketGameStatusScript.GameStatusList.Add(inistatus);
                break;
            case "i":
                Debug.Log("iの処理をします");
                SocketGameStatus.recvinhanddata = copyrecvdata;
                GameStatus idrecv = GameStatus.IniDeckHandIDRecv;
                socketGameStatusScript.GameStatusList.Add(idrecv);
                RecvComplete();
                break;
            case "dc":
                Debug.Log("dcの処理をします");
                DrawCardSetting(spritdata);
                break;
            case "king":
                break;
        }
    }

    void DrawCardSetting(string[] data)
    {
        string[] carddata = data[1].Split(',');
        int id = int.Parse(carddata[1]);
        int dictionarnumber = int.Parse(carddata[0]);
        DeckClass deckclass = socketGameStatusScript.GetDeckClassScript();
        GameObject card = deckclass.InstanceCard(dictionarnumber,id);
        DeckHand deckhand = deckclass.GetDeckHand();
        deckhand.SetDrawObj(card);
    }



    /// <summary>
    /// メインスレッドから呼び出すときのスレッド
    /// </summary>
    void RecvDataAnalysis(RecvStatus recvstatus)
    {
        Debug.Log("Updateから呼び出されました。");

        switch (recvstatus)
        {
            case RecvStatus.IniDeckHandIDRecv:
                string[] spritdata = SocketGameStatus.recvinhanddata.Split('/');
                DeckHandInstanceIdSet(spritdata);
                break;
        }
    }

    /// <summary>
    /// 生成した初期手札にidを設定する処理
    /// </summary>
    /// <param name="data"></param>
    void DeckHandInstanceIdSet(string[] data)
    {
        Debug.Log("デッキのidを設定します。");
        List<int> dictionarylist = new List<int>();
        List<int> numberlist = new List<int>();
        //        Array.Clear(data, 0, 1);
        int length = data.Length;
        for (int count = 1; count < length - 2; count++)
        {
            string[] numbers = data[count].Split(',');

            dictionarylist.Add(int.Parse(numbers[0]));
            numberlist.Add(int.Parse(numbers[1]));
        }

        DeckClass deckclass = socketGameStatusScript.GetDeckClassScript();
        DeckHand deckhand = deckclass.GetDeckHand();
        deckhand.IniSetInstanceNumber(dictionarylist, numberlist);
        Debug.Log("デッキのidを設定完了");
        SocketCloseStart();//デバック用

    }

    /// <summary>
    ///デバック用
    /// </summary>
    void SocketCloseStart()
    {
        closeThread = new Thread(SocketClose);
        closeThread.Start();
    }

    /// <summary>
    ///デバック用
    /// </summary>
    void SocketClose()
    {
        parentSock.Close();
        Debug.Log("ソケットを閉じます");
    }

    void SetCards(string[] data)
    {

    }

    /// <summary>
    /// 受け取ったデッキのデータの処理
    /// </summary>
    /// <param name="recvData"></param>
    void RecvDeckData(string[] recvData)
    {
        string data = "";
        for (int count = 1; count < recvData.Length; count++)
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
        gameMasterScript.NetWorkPlayerNumber = num;
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
        SocketProduction.SockSend(parentSock, data);
        Debug.Log("デッキのデータを送信しました");
        string deckdata = SocketProduction.SockRecv(parentSock);
        Debug.Log("デッキのデータを受信しました");
        RecvDataAnalysis(deckdata);
        RecvComplete();
    }

    public void DeckRead()
    {
        if (!hoge)
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
            if (deckdata[count] == "")
            {
                break;
            }
            Debug.Log(deckdata[count]);
            int col = int.Parse(deckdata[count]);
            socketGameStatusScript.DeckClassScript.SetCharacter(col, playernumber);
        }
        socketGameStatusScript.CopyRecvData = "";
        socketGameStatusScript.DeckClassScript.IniShaffle(false);
        GameStatus gamestatus = GameStatus.DeckReadComplete;
        socketGameStatusScript.GameStatusList.Add(gamestatus);
    }

    void RecvComplete()
    {
        Debug.Log("メッセージ返答を送信しました");
        string message = "ok";
        SocketProduction.SockSend(parentSock, message);
    }
}
