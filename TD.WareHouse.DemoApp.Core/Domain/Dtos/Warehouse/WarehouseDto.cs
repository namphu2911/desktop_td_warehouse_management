using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Location;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Warehouse
{
    public class WarehouseDto
    {
        public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public List<LocationDto> Locations { get; set; }
        public WarehouseDto(string warehouseId, string warehouseName, List<LocationDto> locations)
        {
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
            Locations = locations;
        }
    }
}
