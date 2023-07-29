using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Location
{
    public class CreateLocationDto
    {
        public string warehouseId { get; set; }
        public string locationId { get; set; }
        public CreateLocationDto(string warehouseId, string locationId)
        {
            this.warehouseId = warehouseId;
            this.locationId = locationId;
        }
    }
}
