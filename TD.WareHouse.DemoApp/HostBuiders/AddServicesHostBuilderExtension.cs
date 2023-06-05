using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TD.WareHouse.DemoApp.Core.Application.Services;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using TD.WareHouse.DemoApp.Core.Infrastructure.Context;

namespace TD.WareHouse.DemoApp.HostBuiders
{
    public static class AddServicesHostBuilderExtension
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                object value = services.AddAutoMapper(typeof(ApplicationDbContext));

                services.AddSingleton<ItemStore>();
                services.AddSingleton<ItemLotStore>();
                services.AddSingleton<WarehouseStore>();
                services.AddSingleton<GoodsReceiptStore>();
                services.AddSingleton<GoodsIssueStore>();
                services.AddSingleton<DepartmentStore>();

                services.AddSingleton<IApiService, ApiService>();
                services.AddSingleton<IDatabaseSynchronizationService, DatabaseSynchronizeService>();
                services.AddSingleton<IExcelReader, ExcelReader>();

                services.AddSingleton<IItemDatabaseService, ItemDatabaseService>();
                services.AddSingleton<IItemLotDatabaseService, ItemLotDatabaseService>();
                services.AddSingleton<IWarehouseDatabaseService, WarehouseDatabaseService>();
                services.AddSingleton<ILocationDatabaseService, LocationDatabaseService>();
                services.AddSingleton<IGoodReceiptDatabaseService, GoodReceiptDatabaseService>();
                services.AddSingleton<IGoodIssueDatabaseService, GoodIssueDatabaseService>();

            });
            return host;
        }
    }
}
