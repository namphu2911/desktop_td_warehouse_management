using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts
{
    public class FinishedProductReceiptEntry
    {
        public string PurchaseOrderNumber { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public string Note { get; set; }

        public FinishedProductReceiptEntry(string purchaseOrderNumber, string itemId, string itemName, string unit, double quantity, string note)
        {
            PurchaseOrderNumber = purchaseOrderNumber;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            Quantity = quantity;
            Note = note;
        }
    }
}
