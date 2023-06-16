using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class CreateGoodsReceiptDto
    {
        public string goodsReceiptId { get; set; }
        public DateTime timestamp { get; set; }
        public string? supplier { get; set; }
        public string employeeId { get; set; }
        public List<CreateGoodsReceiptLotDto> goodsReceiptLots { get; set; }
        public CreateGoodsReceiptDto(string goodsReceiptId, DateTime timestamp, string? supplier, string employeeId, List<CreateGoodsReceiptLotDto> goodsReceiptLots)
        {
            this.goodsReceiptId = goodsReceiptId;
            this.timestamp = timestamp;
            this.supplier = supplier;
            this.employeeId = employeeId;
            this.goodsReceiptLots = goodsReceiptLots;
        }
    }
}
