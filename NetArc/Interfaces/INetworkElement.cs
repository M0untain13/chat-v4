namespace NetArc.Interfaces;

public interface INetworkElement
{
    void Start();
    void Stop();
    void Send(string message);
}