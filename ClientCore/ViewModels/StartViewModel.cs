using ClientCore.Models;
using ClientCore.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using NetArc;

namespace ClientCore.ViewModels
{
    public class StartViewModel : MvxViewModel
    {
        #region Сервисы

        private readonly IMvxNavigationService _navigationService;

        #endregion

        #region Поле для ввода имени пользователя

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        #endregion

        #region Команды

        public IMvxCommand ConnectToServerCommand { get; }

        public IMvxCommand DisconnectToServerCommand { get; }

        public IMvxCommand AuthCommand { get; }

        #endregion

        public StartViewModel(
            IClientService clientService,
            IMvxNavigationService navigationService
        )
        {
            #region Инициализация сервисов

            _navigationService = navigationService;

            #endregion

            #region Инициализация команд

            // TODO: переделать команды

            ConnectToServerCommand = new MvxCommand(
                () =>
                {
                    _client = clientService.GetClient(_Callback, 8000, 8001);
                    var isStart = _client.Start();
                    if (!isStart)
                        _client = null;
                },
                () => _client == null
            );

            DisconnectToServerCommand = new MvxCommand(
                () =>
                {
                    if (_client == null)
                        return;

                    _client.Stop();
                    _client = null;
                },
                () => _client != null
            );

            AuthCommand = new MvxCommand(
                () =>
                {
                    if (_client == null)
                        return;

                    _client.Send($"{Code.AUTH}{Name}");
                },
                () => _client != null
            );

            #endregion
        }

        private IClientWrapper? _client;

        private void _Callback(string message)
        {
            if (message.Contains(Code.Wrap(Name)) && message.Contains(Code.ACCEPT) && _client != null)
            {
                _navigationService.Navigate<ChatViewModel, IClientWrapper>(_client);
            }
        }
    }
}