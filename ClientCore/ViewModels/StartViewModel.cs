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
                    _client.Send($"{Tag.AUTH}{Name}");
                },
                () => _isStart && Name != ""
            );

            #endregion
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            do
            {
                _client = _clientService.GetClient(_Callback, 8000, 8001);
                _isStart = _client.Start();
            } while (!_isStart);
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