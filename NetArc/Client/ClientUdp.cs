using System.Text;

namespace NetArc.Client;
using System.Net;
using System.Net.Sockets;


/// <summary>
/// Слушатель вещаний сервера
/// </summary>
internal class ClientUdp
{
    /// <summary>
    /// Создать слушатель
    /// </summary>
    /// <param name="port"> Порт для прослушки вещаний </param>
    public ClientUdp(int port)
    {
        _server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        var ip = new IPEndPoint(IPAddress.Any, port);
        _ep = ip;
        _server.Bind(ip);
    }

    /// <summary>
    /// Получить IP сервера, но только после завершения метода Start()
    /// </summary>
    /// <returns> IP сервера в виде текста </returns>
    public string GetServerIp()
    {
        byte[] buffer = new byte[1024];
        _server.ReceiveFrom(buffer, ref _ep);
        return Encoding.ASCII.GetString(buffer);
    }

    private EndPoint _ep;
    private readonly Socket _server;
}