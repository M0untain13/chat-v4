using ClientCore.Models;
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
                () =>
                {
                    _client = clientService.GetClient(_Callback, 8000, 8001);
                }
            );

            AuthCommand = new MvxCommand(
                () =>
                {
                    _client.Send("<|auth|>Dimon");
                    navigationService.Navigate<ChatViewModel>();
                }
            );

            #endregion
        }

        private IClientWrapper _client = null!;

        private void _Callback(string message)
        {
            // TODO: здесь обрабатывать сообщения сервера
        }
    }
}