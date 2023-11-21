﻿using System.Net.Sockets;

namespace NetArc.Client;

/// <summary>
/// Соединитель клиента с сервером
/// </summary>
internal class ClientTcp
{
    /// <summary>
    /// Создать соединитель
    /// </summary>
    /// <param name="server"> Сокет сервера </param>
    /// <param name="callback"> Метод для обработки сообщений сервера </param>
    /// <param name="port"> Порт для соединения </param>
    public ClientTcp(Action<WebMessage> callback, int port)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Сделать попытку соединения с сервером, если получилось, то начать прослушку входящих сообщений с сервера
    /// </summary>
    public bool Start(string ip)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Остановить попытку соединения или прослушку входящих соединений
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public bool Stop()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Отправить сообщение на сервер
    /// </summary>
    /// <param name="message"> Текст сообщения </param>
    public bool Send(string message)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Установить новый обработчик сообщений сервера
    /// </summary>
    /// <param name="callback"> Метод для обработки сообщений сервера </param>
    /// <returns> Удалось ли установить обработчик </returns>
    public bool SetCallback(Action<string> callback)
    {
        throw new NotImplementedException();
    }
}