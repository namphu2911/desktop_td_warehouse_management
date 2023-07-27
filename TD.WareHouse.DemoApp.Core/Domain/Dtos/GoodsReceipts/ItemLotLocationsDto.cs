using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class ItemLotLocationsDto
    {
        public string locationId { get; set; }
        public double quantityPerLocation { get; set; }
        public ItemLotLocationsDto(string locationId, double quantityPerLocation)
        {
            this.locationId = locationId;
            this.quantityPerLocation = quantityPerLocation;
        }
    }
}
