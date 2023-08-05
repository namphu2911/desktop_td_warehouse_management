using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm
{
    public class EntryForAlarmViewModel
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string LotId { get; set; }
        public double Quantity { get; set; }
        public double MinimumStockLevel { get; set; }
        public List<LocationsForAlarmViewModel> LocationsForAlarms { get; set; }
        public string ItemClassId { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public TimeSpan? Difference => ExpirationDate - DateTime.Today;
        public double TimeLeft => Difference.Value.TotalDays / 30;
        public bool IsQuantityAlarmed => Quantity <= MinimumStockLevel;
        public bool IsExpirationDateAlarmed => TimeLeft <= 6;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private EntryForAlarmViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public EntryForAlarmViewModel(string itemId, string itemName, string unit, string lotId, double quantity, double minimumStockLevel, List<LocationsForAlarmViewModel> locationsForAlarms, string itemClassId, DateTime? productionDate, DateTime? expirationDate)
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            LotId = lotId;
            Quantity = quantity;
            MinimumStockLevel = minimumStockLevel;
            LocationsForAlarms = locationsForAlarms;
            ItemClassId = itemClassId;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
        }
    }
}
    
