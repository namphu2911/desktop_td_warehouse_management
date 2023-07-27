using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.History
{
    public class HistoryFinishedProductReceiptEntryViewModel
    {
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public double Quantity { get; set; }
        public string Note { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public HistoryFinishedProductReceiptEntryViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
        public HistoryFinishedProductReceiptEntryViewModel(string itemClassId, string itemId, string itemName, string unit, string purchaseOrderNumber, double quantity, string note)
        {
            ItemClassId = itemClassId;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            PurchaseOrderNumber = purchaseOrderNumber;
            Quantity = quantity;
            Note = note;
        }
    }
}
