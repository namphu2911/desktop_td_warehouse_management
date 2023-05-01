using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;

namespace TD.WareHouse.DemoApp.Core.Domain.Repositories;
public interface ILocationRepository: IRepository
{
    void AddList(IEnumerable<Location> locations);
    Task<IEnumerable<Location>> GetAll();
    void Clear();
}
