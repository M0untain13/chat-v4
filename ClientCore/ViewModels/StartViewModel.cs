using ClientCore.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ClientCore.ViewModels
{
    public class StartViewModel : MvxViewModel
	{
        #region Команды

        public IMvxCommand ConnectToServerCommand { get; }

        public IMvxCommand AuthCommand { get; }

        #endregion

        public StartViewModel(
            IClientService clientService,
            IMvxNavigationService navigationService
        )
        {
            #region Инициализация команд

            // TODO: переделать команды

            ConnectToServerCommand = new MvxCommand(
                () => {
                    clientService.Connect(_Callback);
                }
            );

            AuthCommand = new MvxCommand(
                () =>
                {
                    clientService.Send("<|auth|>Dimon");
                    navigationService.Navigate<ChatViewModel>();
                }
            );

            #endregion
        }

        private void _Callback(string message)
        {
            // TODO: здесь обрабатывать сообщения сервера
        }
    }
}