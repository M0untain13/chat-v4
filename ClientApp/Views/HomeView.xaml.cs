using ClientCore.ViewModels;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace ClientApp.Views
{
    [MvxViewFor(typeof(StartViewModel))]
    public partial class HomeView : MvxWpfView
    {
        public HomeView() => InitializeComponent();
    }
}