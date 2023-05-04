using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Warehouses;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class WarehouseDatabaseService : IWarehouseDatabaseService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public WarehouseDatabaseService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Warehouse>> FilterWarehousesByName(string warehouseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Warehouse>> GetAllWarehouses()
        {
            using var scope = _scopeFactory.CreateScope();
            var warehouseRepository = CreateWarehouseRepository(scope);

            var warehouses = warehouseRepository.GetAll();

            return warehouses;
        }

        public Task Insert(IEnumerable<Warehouse> warehouses)
        {
            throw new NotImplementedException();
        }

        public async Task OverrideAllWarehouses(IEnumerable<Warehouse> warehouses)
        {
            using var scope = _scopeFactory.CreateScope();
            var warehouseRepository = CreateWarehouseRepository(scope);

            warehouseRepository.Clear();
            warehouseRepository.AddList(warehouses);

            await warehouseRepository.UnitOfWork.SaveChangesAsync();
        }

        private IWarehouseRepository CreateWarehouseRepository(IServiceScope scope)
        {
            var warehouseRepository = scope.ServiceProvider.GetService<IWarehouseRepository>();
            if (warehouseRepository is null)
            {
                throw new InvalidOperationException();
            }

            return warehouseRepository;
        }
    }
}
