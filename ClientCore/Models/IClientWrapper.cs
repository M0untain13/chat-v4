using NetArc.Client;

namespace ClientCore.Models;

public interface IClientWrapper
{
    bool Start();

    bool Stop();

    bool Send(string message);

    bool SetCallback(Action<string> callback);
}