using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Repositories;
public interface IGoodsReceiptRepository : IRepository
{
    void AddList(IEnumerable<GoodsReceipt> goodsReceipt);
    Task<IEnumerable<GoodsReceipt>> GetAll();
    void Clear();
}
