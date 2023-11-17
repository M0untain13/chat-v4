using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ClientCore.ViewModels;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace ClientApp.Views
{
    [MvxViewFor(typeof(ChatViewModel))]
    public partial class ChatView : MvxWpfView
    {
        public ChatView() => InitializeComponent();
    }
}