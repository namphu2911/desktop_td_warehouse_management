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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Location() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Location(string locationId, List<ItemLot> itemLots)
        {
            LocationId = locationId;
            ItemLots = itemLots;
        }
    }
}
