using ClientCore.ViewModels;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System.Windows;

namespace ClientApp.Views
{
    [MvxViewFor(typeof(StartViewModel))]
    public partial class StartView : MvxWpfView
    {
        public StartView() => InitializeComponent();

        private void HelpButtonClick(object sender, System.Windows.RoutedEventArgs e) => MessageBox.Show(_text, _caption);

        private const string
            _text = "Введите своё имя в поле и нажмите кнопку 'отправить'. Возможно имя будет занято, тогда авторизация не получится, просто введите другое имя.",
            _caption = "Помощь";
    }
}