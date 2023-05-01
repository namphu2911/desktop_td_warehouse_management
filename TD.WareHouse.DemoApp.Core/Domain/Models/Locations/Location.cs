using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.Locations
{
    public class Location
    { 
        public string LocationId { get; set; }
        public List<ItemLot> ItemLots { get; set; }
        public Location(string locationId, List<ItemLot> itemLots)
        {
            LocationId = locationId;
            ItemLots = itemLots;
        }
    }
}
