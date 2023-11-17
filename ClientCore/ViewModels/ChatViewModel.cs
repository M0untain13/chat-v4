using System.Collections.ObjectModel;
using ClientCore.Models;
using ClientCore.Services; 
using MvvmCross.ViewModels;
using NetArc;
using MvvmCross.Commands;

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

        public IMvxCommand SendCommand { get; }

        #endregion

        public ChatViewModel()
        {
            #region Инициализация команд

            SendCommand = new MvxCommand(
                () =>
                {
                    if (Text.Length == 0)
                        return;

                    StatusMessage = "Отправка сообщения...";

                    /* TODO: вернуть
                    StatusMessage = _client.Send(
                        $"{Tag.MESSAGE}{Tag.Wrap(_name, Tag.NAME_S, Tag.NAME_E)}{Tag.Wrap(Text, Tag.TEXT_S, Tag.TEXT_E)}") 
                        ? "Сообщение отправлено." 
                        : "Ошибка: Сообщение не отправилось.";
                    */

                    // TODO: убрать
                    
                }
            );

            #endregion

            #region Временная заглушка

            Messages.Add(new Message { Name = "Валера", Text = "Го бухать" });
            Messages.Add(new Message { Name = "Димон", Text = "Го)))" });

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

        private void _Callback(string message)
        {
            if (message.Contains(Tag.MESSAGE))
            {
                message = message.Replace(Tag.MESSAGE, "");

                
                var name = Tag.ParseWrap(message, Tag.NAME_S, Tag.NAME_E);
                var text = Tag.ParseWrap(message, Tag.TEXT_S, Tag.TEXT_E);

                if (name == _name)
                    name = "Вы";

                Messages.Add(new Message{Name=name, Text=text});
            }
        }
    }
}