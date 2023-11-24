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

        private void HelpButtonClick(object sender, System.Windows.RoutedEventArgs e) => MessageBox.Show(_text, _caption);

        private const string
            _text = "Введите в поле сообщение и нажмите на кнопку 'отправить', это сообщение отправится всем пользователям в чате.",
            _caption = "Помощь";
    }
}