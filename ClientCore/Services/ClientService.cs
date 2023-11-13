using ClientCore.Models;

namespace ClientCore.Services;

// TODO: написать реализацию клиента

internal class ClientService : IClientService
{
    public IClientWrapper GetClient(Action<string> callback, int connectPort, int listenPort)
    {
        return new NetArcClientWrapper(callback, connectPort, listenPort);
    }
}