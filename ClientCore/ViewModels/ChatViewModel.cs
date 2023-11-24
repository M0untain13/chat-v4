using System.Collections.ObjectModel;
using ClientCore.Models;
using ClientCore.Services; 
using MvvmCross.ViewModels;
using NetArc;
using MvvmCross.Commands;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace ClientCore.ViewModels
{
    public class ChatViewModel : MvxViewModel<(IClientWrapper, string)>
    {
        #region Коллекция пришедших сообщений

        private ObservableCollection<Message> _messages = new();
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        #endregion

        #region Поле ввода сообщения

        private string _text = string.Empty;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        #endregion

        #region Сообщение статус-бара

        private ushort _timer = 0;

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (SetProperty(ref _statusMessage, value))
                {
                    // Установка таймера для сброса сообщения в статус-баре
                    _timer = 3;
                }
            }
        }

        #endregion

        #region Команды

        public IMvxAsyncCommand SendCommand { get; }

        #endregion

        public ChatViewModel()
        {
            #region Инициализация команд

            SendCommand = new MvxAsyncCommand(
                () =>
                {
                    if (Text.Length == 0)
                        return Task.Run(() =>
                        {
                            StatusMessage = "Сообщение не было отправлено!";
                        });

                    return Task.Run(() =>
                    {
                        StatusMessage = "Отправка сообщения...";

                        StatusMessage = _client.Send(new WebMessage("client", "message", _name, Text))
                            ? "Сообщение отправлено."
                            : "Ошибка: Сообщение не отправилось.";
                        
                        Text = "";
                    });
                }
            );

            #endregion

            #region Таймер для сброса статус-бара

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    if (_timer > 0)
                        _timer--;
                    if (_timer == 0)
                        StatusMessage = "";
                }
            });

            #endregion
        }

        public override void Prepare((IClientWrapper, string) client)
        {
            _client = client.Item1;
            _name = client.Item2;
            _client.SetCallback(_Callback);
        }

        private IClientWrapper _client = null!;
        private string _name = string.Empty;

        private void _Callback(WebMessage webMessage)
        {
            AsyncDispatcher.ExecuteOnMainThreadAsync(() =>
            {
                switch (webMessage)
                {
                    case { sender: "server", type: "exit" }:
                        Messages.Add(new Message("Серверное сообщение", "Сервер отключился..."));
                        break;
                    case { sender: "client", type: "message" }:
                        var message = new Message(webMessage.name, webMessage.text);
                        if (message.Name == _name)
                            message.Name = "Вы";
                        Messages.Add(message);
                        break;
                    case { sender: "server", type: "message" }:
                        Messages.Add(new Message("Серверное сообщение", webMessage.text));
                        break;
                    default:
                        return;
                }
                StatusMessage = "Пришло сообщение.";
            });
        }
    }
}