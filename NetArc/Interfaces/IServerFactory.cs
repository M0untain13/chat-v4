using System.Net.Sockets;

namespace NetArc.Interfaces;

public interface IServerFactory
{
    INetworkElement CreateServer(Action<string> callback);
    INetworkElement CreateListener(Action<string> callback);
    INetworkElement CreateConnection(Socket socket, Action<string> callback);
    INetworkElement CreateBroadcaster(int broadcastTimeout);
}