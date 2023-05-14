using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class StockCardExtendedEntryViewModel : BaseViewModel
    {
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double BeforeQuantity { get; set; }
        public double AfterQuantity { get; set; }
        public double ReceivedQuantity { get; set; }
        public double ShippedQuantity { get; set; } 

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StockCardExtendedEntryViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
        public StockCardExtendedEntryViewModel(string itemClassId, string itemId, string itemName, string unit, double beforeQuantity, double afterQuantity, double receivedQuantity, double shippedQuantity)
        {
            ItemClassId = itemClassId;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            BeforeQuantity = beforeQuantity;
            AfterQuantity = afterQuantity;
            ReceivedQuantity = receivedQuantity;
            ShippedQuantity = shippedQuantity;

        }
    }
}
