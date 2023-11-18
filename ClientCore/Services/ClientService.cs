﻿using ClientCore.Models;
using NetArc;

namespace ClientCore.Services;

// TODO: написать реализацию клиента

internal class ClientService : IClientService
{
    public IClientWrapper GetClient(Action<WebMessage> callback, int connectPort, int listenPort)
    {
        // TODO: вернуть return new NetArcClientWrapper(callback, connectPort, listenPort);
        return new TempClientWrapper(callback, connectPort, listenPort);
    }
}