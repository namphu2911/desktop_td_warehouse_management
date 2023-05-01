using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Warehouses;

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IWarehouseDatabaseService
    {
        Task<IEnumerable<Warehouse>> FilterWarehousesByName(string warehouseName);
        Task<IEnumerable<Warehouse>> GetAllWarehouses();
        Task Clear();
        Task Insert(IEnumerable<Warehouse> warehouses);
        Task OverrideAllWarehouses(IEnumerable<Warehouse> warehouses);
    }
}
