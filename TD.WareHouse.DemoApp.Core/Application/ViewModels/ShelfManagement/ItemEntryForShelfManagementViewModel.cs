using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.ShelfManagement
{
    public class ItemEntryForShelfManagementViewModel
    {
        public string LotId { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string LocationId { get; set; }
        public string PurchaseOrderNumber { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ItemEntryForShelfManagementViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public ItemEntryForShelfManagementViewModel(string lotId, double quantity, string unit, string locationId, string purchaseOrderNumber)
        {
            LotId = lotId;
            Quantity = quantity;
            Unit = unit;
            LocationId = locationId;
            PurchaseOrderNumber = purchaseOrderNumber;
        }
    }
}
