using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public event EventHandler<string>? UserLoggedIn;

        public async Task<string> LoginAsync(IBrowser browser)
        {
            var oidcClient = CreateOidcClient(browser);
            LoginResult loginResult;
            loginResult = await oidcClient.LoginAsync();

            string token = loginResult.AccessToken;
            UserLoggedIn?.Invoke(this, token);
            return token;
        }

        private OidcClient CreateOidcClient(IBrowser browser)
        {
            var options = new OidcClientOptions()
            {
                Authority = "https://authenticationserver20220822101419.azurewebsites.net",
                ClientId = "native-client",
                Scope = "openid native-client-scope profile",
                RedirectUri = "https://authenticationserver20220822101419.azurewebsites.net/account/login",
                Browser = browser,
                Policy = new Policy
                {
                    RequireIdentityTokenSignature = false
                }
            };

            return new OidcClient(options);
        }
    }
}
