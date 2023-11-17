using System.Collections.ObjectModel;
using ClientCore.Models;
using ClientCore.Services;
using MvvmCross.ViewModels;
using NetArc;
using System.Xml.Linq;
using MvvmCross.Commands;
using static System.Net.Mime.MediaTypeNames;

namespace ClientCore.ViewModels
{
    public class ChatViewModel : MvxViewModel<(IClientWrapper, string)>
    {
        #region Коллекция пришедших сообщений

        private ObservableCollection<Messages> _messages = new();
        public ObservableCollection<Messages> Messages
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

        #region Команды

        public IMvxCommand SendCommand { get; }

        #endregion

        public ChatViewModel()
        {
            #region Инициализация команд

            SendCommand = new MvxCommand(
                () =>
                {
                    if (_text.Length == 0)
                        return;

                    _client.Send($"{Tag.MESSAGE}{Tag.Wrap(_name, Tag.NAME_S, Tag.NAME_E)}{Tag.Wrap(_text, Tag.TEXT_S, Tag.TEXT_E)}");
                }
            );

            _messages.Add(new Messages { Name = "Валера", Text = "Го бухать" });
            _messages.Add(new Messages { Name = "Димон", Text = "Го)))" });

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

                _messages.Add(new Messages{Name=name, Text=text});
            }
        }
    }
}