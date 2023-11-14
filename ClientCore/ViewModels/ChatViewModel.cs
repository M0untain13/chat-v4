using System.Collections.ObjectModel;
using ClientCore.Models;
using ClientCore.Services;
using MvvmCross.ViewModels;
using NetArc;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ClientCore.ViewModels
{
    public class ChatViewModel : MvxViewModel<(IClientWrapper, string)>
    {
        private ObservableCollection<(string, string)> _messages = new();
        public ObservableCollection<(string, string)> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        public override void Prepare((IClientWrapper, string) client)
        {
            _client = client.Item1;
            _name = client.Item2;
            _client.SetCallback(_Callback);
        }

        private IClientWrapper? _client;
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

                _messages.Add((name, text));
            }
        }
    }
}