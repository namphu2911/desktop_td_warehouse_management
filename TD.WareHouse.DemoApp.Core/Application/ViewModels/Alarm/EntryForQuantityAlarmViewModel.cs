﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm
{
    public class EntryForQuantityAlarmViewModel
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
        public double TimeLeft { get; set; }
        public bool IsQuantityAlarmed => Quantity <= MinimumStockLevel;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private EntryForQuantityAlarmViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public EntryForQuantityAlarmViewModel(string itemId, string itemName, string unit, string lotId, double quantity, double minimumStockLevel, string locationId, string purchaseOrderNumber, string itemClassId, double timeLeft)
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
            TimeLeft = timeLeft;
        }
    }
}
    
