using System.Collections.ObjectModel;
using ClientCore.Models;
using ClientCore.Services;
using MvvmCross.ViewModels;
using NetArc;
using System.Xml.Linq;

namespace ClientCore.ViewModels
{
    public class ChatViewModel : MvxViewModel<IClientWrapper>
    {
        private ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        public ChatViewModel()
        {
            
        }

        public override void Prepare(IClientWrapper client)
        {
            _client = client;
            _client.SetCallback(_Callback);
        }

        private IClientWrapper? _client;

        private void _Callback(string message)
        {
            if()
        }
    }
}