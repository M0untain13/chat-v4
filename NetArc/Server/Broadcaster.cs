using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetArc.Server;

/// <summary>
/// Вещатель, который должен распространять по сети IP сервера для подключению к нему
/// </summary>
internal class Broadcaster
{
    /// <summary>
    /// Создать вещатель
    /// </summary>
    /// <param name="port"> Порт для вещания </param>
    /// <param name="broadcastTimeout"> Периодичность отправления вещаний </param>
    public Broadcaster(int port, int broadcastTimeout)
    {
        _server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
        _endPoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), port);
        
        _broadcastTimeout = broadcastTimeout;
        //fdfd::1aae:8e82
        var host = Dns.GetHostName();
        var addrList = Dns.GetHostByName(host).AddressList;
        var ip = addrList[10].ToString();

        _buffer = Encoding.UTF8.GetBytes(ip);
    }

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
                _server.SendTo(_buffer, _endPoint);
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

    private readonly Socket _server;
    private readonly int _broadcastTimeout;
    private readonly IPEndPoint _endPoint;
    private readonly byte[] _buffer;

    private bool _isStart;
}