using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts
{
    public class GoodsReceiptLot
    {
        public string GoodsReceiptLotId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public GoodsReceiptLot(string goodsReceiptLotId, string itemId, string itemName, double quantity, string unit, string purchaseOrderNumber)
        {
            GoodsReceiptLotId = goodsReceiptLotId;
            ItemId = itemId;
            ItemName = itemName;
            Quantity = quantity;
            Unit = unit;
            PurchaseOrderNumber = purchaseOrderNumber;
        }
    }
}
