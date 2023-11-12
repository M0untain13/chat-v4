namespace NetArc.Interfaces;

public interface IClientFactory
{
    INetworkElement CreateClient(Action<string> callback);
}