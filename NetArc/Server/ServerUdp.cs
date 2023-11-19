using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace NetArc.Server;

/// <summary>
/// Вещатель, который должен распространять по сети IP сервера для подключению к нему
/// </summary>
internal class ServerUdp
{
    /// <summary>
    /// Создать вещатель
    /// </summary>
    /// <param name="port"> Порт для вещания </param>
    /// <param name="broadcastTimeout"> Периодичность отправления вещаний </param>
    
   
    //UdpClient udpServer = new UdpClient(SERVERUDPPORT);


    //static List<IPEndPoint> Clients = new List<IPEndPoint>(); // Список "подключенных" клиентов
    private static int SERVERUDPPORT = 8000;
    private Socket UPDServertSocket;
    private const int broadcastTimeout = 2000;
    public ServerUdp(int port, int broadcastTimeout)
    {
        try
        {
            UPDServertSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UPDServertSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, SERVERUDPPORT);

            Timer broadcastTimer = new Timer(BroadcastIP, null, 0, broadcastTimeout);
        }
        catch (Exception ex) { };
    }

    private string GetLocaleIPAddress()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return
    }


    /// <summary>
    /// Запустить вещания IP сервера с определённой периодичностью
    /// </summary>
    public bool Start()
    {
        
    }

    /// <summary>
    /// Остановить вещания
    /// </summary>
    public bool Stop()
    {
        throw new NotImplementedException();
    }
}