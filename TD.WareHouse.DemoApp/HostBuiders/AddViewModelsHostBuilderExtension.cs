using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TD.WareHouse.DemoApp.Core.Application.ViewModels;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.History;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Isolation;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.ShelfManagement;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard;

namespace TD.WareHouse.DemoApp.HostBuiders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                ////Login
                //services.AddTransient<LoginViewModel>();
                //services.AddTransient<DashboardViewModel>();
                //services.AddTransient<HomeViewModel>((IServiceProvider serviceProvider) =>
                //{
                //    using var scope = serviceProvider.CreateScope();

                //    var navigationStore = scope.ServiceProvider.GetService<NavigationStore>();
                //    var loginViewNavigationService = scope.ServiceProvider.GetService<INavigationService<LoginViewModel>>();
                //    var dashboardNavigationService = scope.ServiceProvider.GetService<INavigationService<DashboardViewModel>>();

                //    var authenticationService = serviceProvider.GetService<IAuthenticationService>();

                //    return new HomeViewModel(
                //        navigationStore,
                //        loginViewNavigationService,
                //        dashboardNavigationService,
                //        authenticationService);
                //});
                
                //Alarm
                services.AddTransient<AlarmViewModel>();
                services.AddTransient<ExpirationDateAlarmViewModel>();
                services.AddTransient<QuantityAlarmViewModel>();

                //GoodsIssue
                services.AddTransient<GoodsIssueViewModel>();
                services.AddTransient<GoodsIssueExternalViewModel>();
                services.AddTransient<GoodsIssueInternalViewModel>();
                services.AddTransient<GoodsIssueProgressViewModel>();

                //GoodsReceipt
                services.AddTransient<GoodsReceiptViewModel>();

                //History
                services.AddTransient<HistoryViewModel>();
                services.AddTransient<HistoryGoodsReceiptViewModel>();
                services.AddTransient<HistoryGoodsIssueViewModel>();

                //Inventory
                services.AddTransient<InventoryViewModel>();
                //Isolation
                services.AddTransient<IsolationViewModel>();
                //ShelfManagemrnt
                services.AddTransient<ShelfManagementViewModel>();
                //StockCard
                services.AddTransient<StockCardViewModel>();



                services.AddTransient<MainViewModel>();

                //services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
            });

            return host;
        }
    }
}
