using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Location;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Items
{
    public class ItemLotDto
    {
        public string LotId { get; set; }
        public ItemDto Item { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsIsolatedd { get; set; }
        public double Quantity { get; set; }
        public LocationDto Location { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<ItemLotLocationDto> ItemLotLocations { get; set; }
        public ItemLotDto(string lotId, ItemDto item, DateTime timestamp, bool isIsolatedd, double quantity, LocationDto location, DateTime productionDate, DateTime expirationDate, List<ItemLotLocationDto> itemLotLocations)
        {
            LotId = lotId;
            Item = item;
            Timestamp = timestamp;
            IsIsolatedd = isIsolatedd;
            Quantity = quantity;
            Location = location;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
            ItemLotLocations = itemLotLocations;
        }
    }
}
