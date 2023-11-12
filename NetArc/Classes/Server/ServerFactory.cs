using NetArc.Interfaces;
using System.Net.Sockets;

namespace NetArc.Classes.Server;

internal class ServerFactory : IServerFactory
{
    public ServerFactory(int broadcastPort, int listenPort)
    {
        _broadcastPort = broadcastPort;
        _listenPort = listenPort;
    }

    public INetworkElement CreateServer(Action<string> callback)
    {
        return new Server(callback, this);
    }

    public INetworkElement CreateListener(Action<string> callback)
    {
        return new Listener(callback, this, _listenPort);
    }

    public INetworkElement CreateConnection(Socket socket, Action<string> callback)
    {
        return new Connection(socket, callback);
    }

    public INetworkElement CreateBroadcaster(int broadcastTimeout)
    {
        return new Broadcaster(broadcastTimeout, _broadcastPort);
    }

    private readonly int
        _broadcastPort,
        _listenPort;
}