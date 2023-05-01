using CommunityToolkit.Mvvm.Input;
using IdentityModel.OidcClient.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Home
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;

        public IBrowser? Browser { get; set; }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(IApiService apiService, IAuthenticationService authenticationService, IDatabaseSynchronizationService databaseSynchronizationService)
        {
            _apiService = apiService;
            _authenticationService = authenticationService;
            _databaseSynchronizationService = databaseSynchronizationService;

            LoginCommand = new RelayCommand(Login)
            ;
        }

        private async void Login()
        {
            if (Browser is null)
            {
                throw new InvalidOperationException();
            }

            string token = await _authenticationService.LoginAsync(Browser);

            _apiService.SetToken(token);

            await Task.WhenAll(
                _databaseSynchronizationService.SynchronizeItemsData()
                //_databaseSynchronizationService.SynchronizeEmployeesData()
                );
        }
    }
}
