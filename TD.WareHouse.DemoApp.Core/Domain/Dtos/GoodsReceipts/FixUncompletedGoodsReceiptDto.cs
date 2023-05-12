using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class FixUncompletedGoodsReceiptDto
    {
        public string goodsReceiptLotId { get; set; }
        public string? locationId { get; set; }
        public double quantity { get; set; }
        public double? sublotSize { get; set; }
        public string sublotUnit { get; set; }
        public string? purchaseOrderNumber { get; set; }
        public DateTime? productionDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public string? note { get; set; }
        public FixUncompletedGoodsReceiptDto(string goodsReceiptLotId, string? locationId, double quantity, double? sublotSize, string sublotUnit, string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, string? note)
        {
            this.goodsReceiptLotId = goodsReceiptLotId;
            this.locationId = locationId;
            this.quantity = quantity;
            this.sublotSize = sublotSize;
            this.sublotUnit = sublotUnit;
            this.purchaseOrderNumber = purchaseOrderNumber;
            this.productionDate = productionDate;
            this.expirationDate = expirationDate;
            this.note = note;
        }
    }
}
