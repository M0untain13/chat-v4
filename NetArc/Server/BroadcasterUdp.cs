using System.Net;
using System.Net.Sockets;

namespace NetArc.Server;

/// <summary>
/// Вещатель, который должен распространять по сети IP сервера для подключению к нему
/// </summary>
internal class BroadcasterUdp
{
    /// <summary>
    /// Создать вещатель
    /// </summary>
    /// <param name="port"> Порт для вещания </param>
    /// <param name="broadcastTimeout"> Периодичность отправления вещаний </param>

    private static int SERVERUDPPORT = 8000;
    private static int ListeningSOCKET;

    //static List<IPEndPoint> Clients = new List<IPEndPoint>(); // Список "подключенных" клиентов

    private Socket _UdpSocket;
    private void InitializeUdpSocket()
    {
        _UdpSocket = new Socket(AddressFamily.InterNetwork,
        SocketType.Dgram, ProtocolType.Udp);
    }


    public BroadcasterUdp(int port, int broadcastTimeout)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Запустить вещания IP сервера с определённой периодичностью
    /// </summary>
    public bool Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить вещания
    /// </summary>
    public bool Stop()
    {
        throw new NotImplementedException();
    }
}