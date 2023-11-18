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

        private void HelpButtonClick(object sender, System.Windows.RoutedEventArgs e) => MessageBox.Show("Бог поможет...", "No way!");
    }
}