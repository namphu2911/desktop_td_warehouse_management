using TD.WareHouse.DemoApp.Core.Domain.Models.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Repositories;
public interface IItemLotRepository: IRepository
{
    void AddList(IEnumerable<ItemLot> itemLots);
    Task<IEnumerable<ItemLot>> GetAll();
    void Clear();
}
