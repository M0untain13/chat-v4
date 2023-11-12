using NetArc.Interfaces;
using System.Net.Sockets;

namespace NetArc.Classes.Server;

// TODO: написать реализацию соединения с клиентом

internal class Connection : INetworkElement
{
    public Connection(Socket socket, Action<string> callback)
    {

    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public void Send(string message)
    {
        throw new NotImplementedException();
    }
}