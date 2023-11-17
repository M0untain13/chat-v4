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

        private readonly IClientService _clientService;
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

        #region Поле заргрузки

        private string _loadMessage = "Подождите, идёт подключение к серверу...";
        public string LoadMessage
        {
            get => _loadMessage;
            set => SetProperty(ref _loadMessage, value);
        }

        #endregion

        #region Команды

        public IMvxCommand AuthCommand { get; }

        #endregion

        public StartViewModel(
            IClientService clientService,
            IMvxNavigationService navigationService
        )
        {
            #region Инициализация сервисов

            _clientService = clientService;
            _navigationService = navigationService;

            #endregion

            #region Инициализация команд

            AuthCommand = new MvxCommand(
                () =>
                {
                    if (!_isStart || _name == "")
                        return;

                    // TODO: вернуть _client.Send($"{Tag.AUTH}{Name}");
                    // TODO: убрать строку внизу
                    _Callback($"{Tag.ACCEPT}{Tag.Wrap(_name)}");
                }
            );

            #endregion

            #region Подключение к серверу

            Task.Run(() =>
            {
                //TODO: убрать эту строку
                Thread.Sleep(1000);

                do
                {
                    _client = _clientService.GetClient(_Callback, 8000, 8001);
                    _isStart = _client.Start();
                } while (!_isStart);

                LoadMessage = "Клиент подключился к серверу!";
            });

            #endregion
        }

        private IClientWrapper _client = null!;
        private bool _isStart;

        private void _Callback(string message)
        {
            if (message.Contains(Tag.Wrap(Name)) && message.Contains(Tag.ACCEPT) && _isStart)
            {
                _navigationService.Navigate<ChatViewModel, (IClientWrapper, string)>((_client, _name));
            }
        }
    }
}