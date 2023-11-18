namespace NetArc.Client;
using System.Net;
using System.Net.Sockets;


/// <summary>
/// Слушатель вещаний сервера
/// </summary>
internal class ListenerUdp
{
    /// <summary>
    /// Создать слушатель
    /// </summary>
    /// <param name="port"> Порт для прослушки вещаний </param>

    private const int listenPort = 8000;

    public ListenerUdp(int port)
    {
        UdpClient listener = new UdpClient(listenPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

        throw new NotImplementedException();
    }

    /// <summary>
    /// Начать прослушку. True должен вернуть в том случае, когда удалось получить IP сервера.
    /// </summary>
    public bool Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить прослушку
    /// </summary>
    public bool Stop()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получить IP сервера, но только после завершения метода Start()
    /// </summary>
    /// <returns> IP сервера в виде текста </returns>
    public string GetServerIp()
    {
        throw new NotImplementedException();
    }
}