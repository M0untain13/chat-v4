using ClientCore.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace ClientCore
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<HomeViewModel>();
        }
    }
}