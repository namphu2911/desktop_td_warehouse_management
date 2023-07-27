using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Location;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class StockCardLotViewModel : BaseViewModel
    {
        public string ItemLotId { get; set; }
        public double? Quantity { get; set; }
        public string LocationId { get; set; }
        public double QuantityPerLocation { get; set; }
        public StockCardLotViewModel(string itemLotId, double? quantity, string locationId, double quantityPerLocation)
        {
            ItemLotId = itemLotId;
            Quantity = quantity; 
            LocationId = locationId;
            QuantityPerLocation = quantityPerLocation;
        }
    }
}
