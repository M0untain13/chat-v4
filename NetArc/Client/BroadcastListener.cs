﻿using System.Text;
using System.Net;
using System.Net.Sockets;

namespace NetArc.Client;

/// <summary>
/// Слушатель вещаний сервера
/// </summary>
internal class BroadcastListener
{
    /// <summary>
    /// Создать слушатель
    /// </summary>
    /// <param name="port"> Порт для прослушки вещаний </param>
    public BroadcastListener(int port)
    {
        _client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _ip = new IPEndPoint(IPAddress.Any, port);
        _client.Bind(_ip);
    }

    /// <summary>
    /// Получить IP сервера, но только после завершения метода Start()
    /// </summary>
    /// <returns> IP сервера в виде текста </returns>
    public string GetServerIp()
    {
        var buffer = new byte[1024];
        var recv = _client.ReceiveFrom(buffer, ref _ip);
        return Encoding.ASCII.GetString(buffer, 0, recv);
    }

    private EndPoint _ip;

    private readonly Socket _client;
}