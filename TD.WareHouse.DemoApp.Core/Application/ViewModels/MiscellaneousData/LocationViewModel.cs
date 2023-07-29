using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.MiscellaneousData
{
    public class LocationViewModel
    {
        public string WarehouseId { get; set; }
        public string LocationId { get; set; }
        public LocationViewModel(string warehouseId, string locationId)
        {
            WarehouseId = warehouseId;
            LocationId = locationId;
        }
    }
}
