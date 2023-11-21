using System.Text;
using System.Net;
using System.Net.Sockets;

namespace NetArc.Client;

/// <summary>
/// Слушатель вещаний сервера
/// </summary>
internal class BroadcastListener
{
    /// <summary>
    /// Создать слушатель
    /// </summary>
    /// <param name="port"> Порт для прослушки вещаний </param>
    public BroadcastListener(int port)
    {
        _ip = new IPEndPoint(IPAddress.Any, port);
        _epClient = new UdpClient(_ip);
    }

    /// <summary>
    /// Получить IP сервера, но только после завершения метода Start()
    /// </summary>
    /// <returns> IP сервера в виде текста </returns>
    public string GetServerIp()
    {
        var buffer = _epClient.Receive(ref _ip);
        return Encoding.ASCII.GetString(buffer);
    }

    private IPEndPoint _ip;

    private readonly UdpClient _epClient;
}