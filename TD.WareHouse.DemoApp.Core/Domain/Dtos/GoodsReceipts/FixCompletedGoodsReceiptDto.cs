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
        public string? locationId { get; set; }
        public double quantity { get; set; }
        public string? purchaseOrderNumber { get; set; }
        public DateTime? productionDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public FixCompletedGoodsReceiptDto(string goodsReceiptLotId, string? locationId, double quantity, string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate)
        {
            this.goodsReceiptLotId = goodsReceiptLotId;
            this.locationId = locationId;
            this.quantity = quantity;
            this.purchaseOrderNumber = purchaseOrderNumber;
            this.productionDate = productionDate;
            this.expirationDate = expirationDate;
        }
    }
}
