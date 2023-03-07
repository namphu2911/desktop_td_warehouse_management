using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts;

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IApiService
    {
        Task<QueryResult<GoodsReceiptDto>> GetGoodsReceiptsAsync(DateTime startDate, DateTime endDate);
        Task DeleteGoodsReceiptAsync(string goodsReceiptId);
        Task ConfirmGoodsReceiptAsync(string goodsReceiptId);
    }
}
