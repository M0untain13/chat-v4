using ClientCore.Models;
using ClientCore.Services;
using MvvmCross.ViewModels;
using NetArc;
using System.Xml.Linq;

namespace ClientCore.ViewModels
{
    public class ChatViewModel : MvxViewModel<IClientWrapper>
    {
        public ChatViewModel()
        {
            
        }

        public override void Prepare(IClientWrapper client)
        {
            _client = client;
        }

        private IClientWrapper? _client;

        private void _Callback(string message)
        {
            
        }
    }
}