﻿namespace NetArc.Server;

/// <summary>
/// Вещатель, который должен распространять по сети IP сервера для подключению к нему
/// </summary>
internal class BroadcasterUdp
{
    /// <summary>
    /// Создать вещатель
    /// </summary>
    /// <param name="port"> Порт для вещания </param>
    /// <param name="broadcastTimeout"> Периодичность отправления вещаний </param>
    public BroadcasterUdp(int port, int broadcastTimeout)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Запустить вещания IP сервера с определённой периодичностью
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить вещания
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Stop()
    {
        throw new NotImplementedException();
    }
}