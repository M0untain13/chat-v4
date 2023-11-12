using NetArc.Interfaces;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace NetArc.Classes.Server;

internal class Broadcaster : INetworkElement
{
    public Broadcaster(int port, int broadcastTimeout)
    {
        _server = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Dgram,
            ProtocolType.Udp
        );

        _server.SetSocketOption(
            SocketOptionLevel.Socket,
            SocketOptionName.Broadcast,
            1
        );

        _ipep = new IPEndPoint(IPAddress.Broadcast, port);

        var hostname = Dns.GetHostName();
        _data = Encoding.ASCII.GetBytes(hostname);

        _broadcastTimeout = broadcastTimeout;
    }

    public void Start()
    {
        _isWork = true;
        Task.Run(BroadcastMessage);
    }

    public void Stop()
    {
        _isWork = false;
    }

    // Этот метод вроде не должен пригодиться, но на всякий случай, я его сделал
    public void Send(string message)
    {
        if (!_isWork)
            return;

        _server.SendTo(
            Encoding.UTF8.GetBytes(message),
            _ipep
        );
    }

    private bool _isWork;
    private readonly int _broadcastTimeout;
    private readonly Socket _server;
    private readonly IPEndPoint _ipep;
    private readonly byte[] _data;

    private void BroadcastMessage()
    {
        while (_isWork)
        {
            _server.SendTo(_data, _ipep);
            Thread.Sleep(_broadcastTimeout);
        }
    }
}