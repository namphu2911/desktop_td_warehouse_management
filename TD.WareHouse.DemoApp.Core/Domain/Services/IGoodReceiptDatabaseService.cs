using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IGoodReceiptDatabaseService
    {
        Task<IEnumerable<GoodsReceipt>> FilterGoodsReceiptsByName(string goodsReceiptName);
        Task<IEnumerable<GoodsReceipt>> GetAllGoodsReceipts();
        Task Clear();
        Task Insert(IEnumerable<GoodsReceipt> goodsReceipts);
        Task OverrideAllGoodsReceipts(IEnumerable<GoodsReceipt> goodsReceipts);
    }
}
