using ClientCore.Services;
using MvvmCross.ViewModels;

namespace ClientCore.ViewModels
{
	public class StartViewModel : MvxViewModel
	{
		#region Сервисы

		IClientService _clientService;

		#endregion

		public StartViewModel(IClientService clientService)
		{
            #region Инициализация сервисов

            _clientService = clientService;

            #endregion
        }
	}
}