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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Warehouse() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Warehouse(string warehouseId, string warehouseName, List<Location> locations)
        {
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
            Locations = locations;
        }
    }
}
