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
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double TotalQuantity { get; set; }
        public List<StockCardLotViewModel> StockCardLots { get; set; }
        //public string PurchaseOrderNumber { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StockCardEntryViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public StockCardEntryViewModel(string itemClassId, string itemId, string itemName, string unit, double totalQuantity, List<StockCardLotViewModel> stockCardLots)
        {
            ItemClassId = itemClassId;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            StockCardLots = stockCardLots;
            TotalQuantity = totalQuantity;
        }
    }
}
