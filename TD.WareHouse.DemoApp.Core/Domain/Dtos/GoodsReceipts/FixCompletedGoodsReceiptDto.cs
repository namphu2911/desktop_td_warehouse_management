using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class FixCompletedGoodsReceiptDto
    {
        public string itemId { get; set; }
        public string oldPurchaseOrderNumber { get; set; }
        public string newPurchaseOrderNumber { get; set; }
        public string unit { get; set; }
        public double quantity { get; set; }
        public FixCompletedGoodsReceiptDto(string itemId, string oldPurchaseOrderNumber, string newPurchaseOrderNumber, string unit, double quantity)
        {
            this.itemId = itemId;
            this.oldPurchaseOrderNumber = oldPurchaseOrderNumber;
            this.newPurchaseOrderNumber = newPurchaseOrderNumber;
            this.unit = unit;
            this.quantity = quantity;
        }
    }
}
