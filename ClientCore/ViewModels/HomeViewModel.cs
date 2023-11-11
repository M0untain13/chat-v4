using ClientCore.Services;
using MvvmCross.ViewModels;

namespace ClientCore.ViewModels
{
	public class HomeViewModel : MvxViewModel
	{
		#region Сервисы

		IClientService _clientService;

		#endregion

		public HomeViewModel(IClientService clientService)
		{
            #region Инициализация сервисов

            _clientService = clientService;

            #endregion
        }
	}
}