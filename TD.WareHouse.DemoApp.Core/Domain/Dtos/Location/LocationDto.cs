using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Location
{
    public class LocationDto
    { 
        public string LocationId { get; set; }
        public List<ItemLotDto> ItemLots { get; set; }
        public LocationDto(string locationId, List<ItemLotDto> itemLots)
        {
            LocationId = locationId;
            ItemLots = itemLots;
        }
    }
}
