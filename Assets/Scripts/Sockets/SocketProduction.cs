using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;

public class SocketProduction
{
    static　byte[] recvbyte = new byte[1000];

    public static void SockBind(ref Socket sock, EndPoint ep)
    {
        sock.Bind(ep);
    }

    public static void SockListen(ref Socket sock)
    {
        sock.Listen(10);
    }

    public static void SockAccept(Socket sock, ref Socket acceptsock)
    {
        acceptsock = sock.Accept();
    }

    public static void SockConnect(ref Socket sock, IPAddress ip, int port)
    {
        sock.Connect(ip, port);
    }

    public static void SockSend(Socket sock, string senddata)
    {
        byte[] sendbyte = Encoding.UTF8.GetBytes(senddata);
        sock.Send(sendbyte);
    }

    public static string SockRecv(Socket sock)
    {
        sock.Receive(recvbyte);
        string data = Encoding.UTF8.GetString(recvbyte);
        return data;
    }
}
