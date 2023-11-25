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
        _client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _ipep = new IPEndPoint(IPAddress.Any, port);
    }

    /// <summary>
    /// Получить IP сервера, но только после завершения метода Start()
    /// </summary>
    /// <returns> IP сервера в виде текста </returns>
    public string GetServerIp()
    {
        var buffer = new byte[1024];
        while (true)
        {
            try
            {
                _client.Bind(_ipep);
                break;
            }
            catch 
            {
                Thread.Sleep(1000);
            }
            
        }
        
        var recv = _client.ReceiveFrom(buffer, ref _ipep);
        _client.Close();
        return Encoding.UTF8.GetString(buffer, 0, recv);
    }

    private EndPoint _ipep;

    private readonly Socket _client;
}