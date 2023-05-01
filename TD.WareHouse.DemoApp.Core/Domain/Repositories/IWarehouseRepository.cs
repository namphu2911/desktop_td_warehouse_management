using TD.WareHouse.DemoApp.Core.Domain.Models.Warehouses;

namespace TD.WareHouse.DemoApp.Core.Domain.Repositories;
public interface IWarehouseRepository: IRepository
{
    void AddList(IEnumerable<Warehouse> warehouses);
    Task<IEnumerable<Warehouse>> GetAll();
    void Clear();
}
