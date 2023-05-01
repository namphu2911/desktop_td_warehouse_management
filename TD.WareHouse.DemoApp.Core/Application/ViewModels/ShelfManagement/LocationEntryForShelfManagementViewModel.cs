﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.ShelfManagement
{
    public class LocationEntryForShelfManagementViewModel
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string LotId { get; set; }
        public double Quantity { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public LocationEntryForShelfManagementViewModel(string itemId, string itemName, string unit, string lotId, double quantity, string purchaseOrderNumber)
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            LotId = lotId;
            Quantity = quantity;
            PurchaseOrderNumber = purchaseOrderNumber;
        }
    }
}
