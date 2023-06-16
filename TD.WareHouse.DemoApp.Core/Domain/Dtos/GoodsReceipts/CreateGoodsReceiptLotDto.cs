using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class CreateGoodsReceiptLotDto
    {
        public string goodsReceiptLotId { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
        public double? sublotSize { get; set; }
        public string sublotUnit { get; set; }
        public string itemId { get; set; }
        public string? purchaseOrderNumber { get; set; }
        public string employeeId { get; set; }
        public string? note { get; set; }
        public CreateGoodsReceiptLotDto(string goodsReceiptLotId, double quantity, string unit, double? sublotSize, string sublotUnit, string itemId, string? purchaseOrderNumber, string employeeId, string? note)
        {
            this.goodsReceiptLotId = goodsReceiptLotId;
            this.quantity = quantity;
            this.unit = unit;
            this.sublotSize = sublotSize;
            this.sublotUnit = sublotUnit;
            this.itemId = itemId;
            this.purchaseOrderNumber = purchaseOrderNumber;
            this.employeeId = employeeId;
            this.note = note;
        }
    }
}
