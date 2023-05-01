using IdentityModel.OidcClient.Browser;

namespace TD.WareHouse.DemoApp.Core.Domain.Services;
public interface IAuthenticationService
{
    Task<string> LoginAsync(IBrowser browser);
    event EventHandler<string> UserLoggedIn;
}
