using TD.WareHouse.DemoApp.Core.Domain.Models.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Repositories;
public interface IItemRepository: IRepository
{
    void AddList(IEnumerable<Item> items);
    Task<IEnumerable<Item>> GetAll();
    void Clear();
}
