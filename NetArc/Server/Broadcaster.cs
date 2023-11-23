using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace NetArc.Server;

/// <summary>
/// Вещатель, который должен распространять по сети IP сервера для подключению к нему
/// </summary>
internal class Broadcaster
{
    private Socket UPDServertSocket;
    private readonly int _broadcastTimeout = 2000;
    private IPEndPoint _endPoint;

    /// <summary>
    /// Создать вещатель
    /// </summary>
    /// <param name="port"> Порт для вещания </param>
    /// <param name="broadcastTimeout"> Периодичность отправления вещаний </param>
    public Broadcaster(int port, int broadcastTimeout)
    {
        UPDServertSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UPDServertSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
        _endPoint = new IPEndPoint(IPAddress.Broadcast, port);
        
        _broadcastTimeout = broadcastTimeout;
        UPDServertSocket.Bind(_endPoint);
    }



    private bool _isStart;
    private readonly byte[] buffer = new byte[1024];
    /// <summary>
    /// Запустить вещания IP сервера с определённой периодичностью
    /// </summary>
    public bool Start()
    {
        if (_isStart) { return false; }
        _isStart = true;
        Task.Run(() =>
        {
            while (_isStart)
            {
                UPDServertSocket.SendTo(buffer, _endPoint);
                Thread.Sleep(_broadcastTimeout);
            }
        });
        return true;
    }


    /// <summary>
    /// Остановить вещания
    /// </summary>
    public bool Stop()
    {
        if (!_isStart) { return false; }
        _isStart = false;
        return true;
    }
}