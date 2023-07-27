using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Location
{
    public class ItemLotLocationDto
    {
        public string LocationId { get; set; }
        public double QuantityPerLocation { get; set; }
        public ItemLotLocationDto(string locationId, double quantityPerLocation)
        {
            LocationId = locationId;
            QuantityPerLocation = quantityPerLocation;
        }
    }
}
