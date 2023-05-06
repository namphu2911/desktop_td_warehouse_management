using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TD.WareHouse.DemoApp.Core.Application.ViewModels;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.History;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Home;
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
                //Login
                services.AddTransient<LoginViewModel>();
                services.AddTransient<HomeViewModel>();
               

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
                services.AddTransient<InventoryNavigationViewModel>();
                services.AddTransient<InventoryViewModel>();
                services.AddTransient<InventoryHistoryViewModel>();
                //Isolation
                services.AddTransient<IsolationViewModel>();
                //ShelfManagemrnt
                services.AddTransient<ShelfManagementViewModel>();
                //StockCard
                services.AddTransient<StockCardNavigationViewModel>();
                services.AddTransient<StockCardViewModel>();
                services.AddTransient<StockCardExtendedViewModel>();




                services.AddTransient<MainViewModel>();

                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
            });

            return host;
        }
    }
}
