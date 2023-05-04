 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm
{
    public class EntryForExpirationDateAlarmViewModel
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }    
        public string LotId { get; set; }
        public double Quantity { get; set; }
        public double MinimumStockLevel { get; set; }
        public string LocationId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ItemClassId { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TimeSpan Difference => ExpirationDate - DateTime.Today;
        public double TimeLeft => Difference.TotalDays/30;
        public bool IsExpirationDateAlarmed => TimeLeft <= 6;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private EntryForExpirationDateAlarmViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public EntryForExpirationDateAlarmViewModel(string itemId, string itemName, string unit, string lotId, double quantity, double minimumStockLevel, string locationId, string purchaseOrderNumber, string itemClassId, DateTime productionDate, DateTime expirationDate)
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            LotId = lotId;
            Quantity = quantity;
            MinimumStockLevel = minimumStockLevel;
            LocationId = locationId;
            PurchaseOrderNumber = purchaseOrderNumber;
            ItemClassId = itemClassId;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
        }
    }
}
