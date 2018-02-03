using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class SocketAction : MonoBehaviour
{
    Socket parentSock;
    [SerializeField]
    string ipaddress;
    [SerializeField]
    int port;
    Thread socketThread;
    public enum Status
    {
        None,
        Ini,

    }
    static string sendData;
    static string readData;


    void Start()
    {
        socketThread = new Thread(SocketIni);
        socketThread.Start();
    }

    void SocketIni()
    {
        parentSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("接続開始");
        SocketProduction.SockConnect(ref parentSock,IPAddress.Parse(ipaddress),port);
        Debug.Log("接続終了");
        if(parentSock.Connected)
        {
            Debug.Log("接続成功しました。");
        }
        else
        {
            Debug.Log("接続失敗しました。");
        }
        string message = "Hello World";
        SocketProduction.SockSend(parentSock,message);
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
    public void SendSocketData()
    {
        SocketProduction.SockSend(parentSock,sendData);
        return;
    }

    /// <summary>
    /// 受信処理
    /// </summary>
    void RecvSocket()
    {
        bool ret = true;

        while(ret)
        {

        }
    }

}
