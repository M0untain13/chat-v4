using NetArc.Classes.Client;
using NetArc.Classes.Server;
using NetArc.Interfaces;

namespace NetArc;

public static class Facade
{
    public static IServerFactory CreateServerFactory(int broadcastPort, int listenPort)
    {
        return new ServerFactory(broadcastPort, listenPort);
    }

    public static IClientFactory CreateClientFactory(int broadcastPort, int listenPort)
    {
        return new ClientFactory(broadcastPort, listenPort);
    }
}