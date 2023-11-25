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

					return Task.Run(() =>
					{
                        _client.Send(new WebMessage("client", "auth", "anon", Name));
                    });
				}
			);

            #endregion

            #region Подключение к серверу

            Task.Run(() =>
            {
                
                do
                {
                    StatusMessage = "Подключение к серверу";
					Thread.Sleep(300);
					if (_isStart)
						break;
                    StatusMessage = "Подключение к серверу.";
                    Thread.Sleep(300);
                    if (_isStart)
                        break;
                    StatusMessage = "Подключение к серверу..";
                    Thread.Sleep(300);
                    if (_isStart)
                        break;
                    StatusMessage = "Подключение к серверу...";
                    Thread.Sleep(400);
                } while (!_isStart);
            });

            Task.Run(() =>
			{
				do
				{
					_client = _clientService.GetClient(_Callback, 9001, 9002);
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
            AsyncDispatcher.ExecuteOnMainThreadAsync(() =>
            {
                if (message.sender == "server" && message.type == "auth" && message.name == Name)
                {
                    if (message.text == "accept")
                    {
                        StatusMessage = "Авторизация одобрена!";
                        _navigationService.Navigate<ChatViewModel, (IClientWrapper, string)>((_client, Name));
                    }
                    else
                    {
                        StatusMessage = "Авторизация отклонена.";
                    }
                }
            });
		}
	}
}