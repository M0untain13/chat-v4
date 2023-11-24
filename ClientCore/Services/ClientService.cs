using ClientCore.Models;
using NetArc;

namespace ClientCore.Services;

internal class ClientService : IClientService
{
    public IClientWrapper GetClient(Action<WebMessage> callback, int connectPort, int listenPort)
    {
        return new NetArcClientWrapper(callback, connectPort, listenPort);
    }
}