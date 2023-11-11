using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;

namespace ClientApp
{
    public partial class App : MvxApplication
    {
        public App() => this.RegisterSetupType<Setup>();
    }
}