using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.Warehouses
{
    public class Warehouse
    {
        public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public List<Location> Locations { get; set; }
        public Warehouse(string warehouseId, string warehouseName, List<Location> locations)
        {
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
            Locations = locations;
        }
    }
}
