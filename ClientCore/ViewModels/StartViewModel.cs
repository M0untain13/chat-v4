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

        #region Сообщение статус-бара

        private ushort _timer = 0;

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (SetProperty(ref _statusMessage, value))
                {
                    // Установка таймера для сброса сообщения в статус-баре
                    _timer = 3;
                }
            }
        }

        #endregion

        #region Команды

        public IMvxAsyncCommand AuthCommand { get; }

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

            AuthCommand = new MvxAsyncCommand(
                () =>
                {
                    if (!_isStart)
                        return Task.Run(() =>
                        {
                            StatusMessage = "Пока что нет соединения.";
                        });

                    if (_name == "")
                        return Task.Run(() =>
                        {
                            StatusMessage = "Введено пустое имя.";
                        });

                    // TODO: вернуть _client.Send($"{Tag.AUTH}{Name}");
                    // TODO: убрать строку внизу
                    return Task.Run(() =>
                    {
                        _Callback(Name is "Валера" or "Димон"
                            ? new WebMessage("server", "auth", Name, "denied")
                            : new WebMessage("server", "auth", Name, "accept"));
                    });
                }
            );

            #endregion

            #region Подключение к серверу

            Task.Run(() =>
            {
                //TODO: убрать эту строку
                StatusMessage = "Подключение к серверу...";
                Thread.Sleep(3000);

                do
                {
                    _client = _clientService.GetClient(_Callback, 8000, 8001);
                    _isStart = _client.Start();
                } while (!_isStart);

                StatusMessage = "Клиент подключился к серверу!";
            });

            #endregion

            #region Таймер для сброса статус-бара

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    if (_timer > 0)
                        _timer--;
                    if (_timer == 0)
                        StatusMessage = "";
                }
            });

            #endregion
        }

        private IClientWrapper _client = null!;
        private bool _isStart;

        private void _Callback(WebMessage message)
        {
            // TODO: наверное надо будет ещё сравнивать имя, а то вдруг сервер одобрит авторизацию не тому
            if (message.sender == "server" && message.type == "auth" && message.name == Name)
            {
                if(message.text == "accept")
                {
                    StatusMessage = "Авторизация одобрена!";
                    Thread.Sleep(1000);
                    _navigationService.Navigate<ChatViewModel, (IClientWrapper, string)>((_client, Name));
                }
                else
                {
                    StatusMessage = "Авторизация отклонена.";
                }
            }
        }
    }
}