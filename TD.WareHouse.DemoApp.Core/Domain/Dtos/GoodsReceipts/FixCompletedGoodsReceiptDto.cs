using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class FixCompletedGoodsReceiptDto
    {
        public string goodsReceiptLotId { get; set; }
        public double quantity { get; set; }
        public FixCompletedGoodsReceiptDto(string goodsReceiptLotId, double quantity)
        {
            this.goodsReceiptLotId = goodsReceiptLotId;
            this.quantity = quantity;
        }
    }
}
