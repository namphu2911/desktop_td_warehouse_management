using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues
{
    public class FinishedProductIssueEntry
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string? Note { get; set; }
        public FinishedProductIssueEntry(string itemId, string itemName, string unit, double quantity, string purchaseOrderNumber, string? note)
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            Quantity = quantity;
            PurchaseOrderNumber = purchaseOrderNumber;
            Note = note;
        }
    }
}
