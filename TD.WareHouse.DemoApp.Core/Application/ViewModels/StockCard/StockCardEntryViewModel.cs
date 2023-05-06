using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class StockCardEntryViewModel : BaseViewModel
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double MinimumStockLevel { get; set; }
        public string ItemClassId { get; set; }
        public string LotId { get; set; }
        public DateTime Timestamp { get; set; }
        public double ChangedQuantity { get; set; }
        public double ReceiptQuantity => (ChangedQuantity > 0) ? ChangedQuantity : 0;
        public double IssueQuantity => (ChangedQuantity < 0) ? -ChangedQuantity : 0;
        
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StockCardEntryViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
            public StockCardEntryViewModel(string itemId, string itemName, string unit, string lotId, DateTime timestamp, double changedQuantity, double minimumStockLevel, string itemClassId)
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            LotId = lotId;
            Timestamp = timestamp;
            ChangedQuantity = changedQuantity;
            MinimumStockLevel = minimumStockLevel;
            ItemClassId = itemClassId;
        }
    }
}
