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
using TD.WareHouse.DemoApp.Core.Application.ViewModels.MiscellaneousData;
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
                services.AddTransient<GoodsIssueInternalProgressViewModel>();
                services.AddTransient<GoodsIssueExternalProgressViewModel>();

                //GoodsReceipt
                services.AddTransient<GoodsReceiptMaterialsViewModel>();
                services.AddTransient<GoodsReceiptCompletedViewModel>();
                services.AddTransient<CreateGoodsReceiptCompletedViewModel>();
                services.AddTransient<GoodsReceiptNavigationViewModel>();

                //History
                services.AddTransient<HistoryViewModel>();
                services.AddTransient<HistoryGoodsReceiptViewModel>();
                services.AddTransient<HistoryGoodsIssueViewModel>();
                services.AddTransient<HistoryFinishedProductReceiptViewModel>();
                services.AddTransient<HistoryFinishedProductIssueViewModel>();

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
                services.AddTransient<FinishedProductStockCardExtendedViewModel>();
                //MiscellaneousData
                services.AddTransient<MiscellaneousDataViewModel>();
                services.AddTransient<ManageItemViewModel>();
                services.AddTransient<ManageEmployeeViewModel>();
                services.AddTransient<ManageLocationViewModel>();
                


                services.AddTransient<MainViewModel>();

                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
            });

            return host;
        }
    }
}
