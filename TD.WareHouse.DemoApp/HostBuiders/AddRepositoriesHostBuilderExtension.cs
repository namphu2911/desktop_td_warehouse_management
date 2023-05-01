using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Infrastructure.Context;
using TD.WareHouse.DemoApp.Core.Infrastructure.Repositories;

namespace TD.WareHouse.DemoApp.HostBuiders
{
    public static class AddRepositoriesHostBuilderExtension
    {
        public static IHostBuilder AddRepositories(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite("Data Source=WarehouseDatabase.db", b => b.MigrationsAssembly("ChaWarehouseDesktopApplication"));
                });

                services.AddScoped<IItemRepository, ItemRepository>();
                services.AddScoped<IItemLotRepository, ItemLotRepository>();
                services.AddScoped<ILocationRepository, LocationRepository>();
                services.AddScoped<IWarehouseRepository, WarehouseRepository>();
                services.AddScoped<IGoodsReceiptRepository, GoodsReceiptRepository>();
                services.AddScoped<IGoodsIssueRepository, GoodsIssueRepository>();
                
            });

            return host;
        }
    }
}
