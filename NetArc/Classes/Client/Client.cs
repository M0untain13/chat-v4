using NetArc.Interfaces;

namespace NetArc.Classes.Client;

// TODO: написать реализацию клиента

internal class Client : INetworkElement
{
    public Client(Action<string> callback, IClientFactory networkFactory)
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