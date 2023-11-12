using NetArc.Interfaces;
using System.Net.Sockets;
using System.Net;

namespace NetArc.Classes.Server;

internal class Listener : INetworkElement
{
    public Listener(Action<string> callback, IServerFactory networkFactory, int port)
    {
        _callback = callback;
        _networkFactory = networkFactory;
        _connections = new List<INetworkElement>();

        var ipep = new IPEndPoint(IPAddress.Any, port);
        _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _server.Bind(ipep);
    }

    public void Start()
    {
        _isWork = true;

        _server.Listen();

        Task.Run(_Listen);
    }

    public void Stop()
    {
        _isWork = false;

        // TODO: почему-то выбрасывает исключение
        _server.Shutdown(SocketShutdown.Both);

        foreach (var connection in _connections)
            connection.Stop();

        _connections.Clear();
    }

    public void Send(string message)
    {
        if (!_isWork)
            return;

        foreach (var connection in _connections)
            connection.Send(message);
    }

    private bool _isWork;

    private readonly Socket _server;
    private readonly Action<string> _callback;
    private readonly IServerFactory _networkFactory;
    private readonly List<INetworkElement> _connections;

    private void _Listen()
    {
        while (_isWork)
        {
            var client = _server.Accept();

            if (_isWork)
            {
                var connection = _networkFactory.CreateConnection(client, _callback);
                connection.Start();
                _connections.Add(connection);
            }
        }
    }
}