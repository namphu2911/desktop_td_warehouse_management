using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory
{
    public class InventoryLogExtendedEntryDto
    {
        public string ItemLotId { get; set; }
        public double BeforeQuantity { get; set; }
        public double AfterQuantity { get; set; }
        public double ReceivedQuantity { get; set; }
        public double ShippedQuantity { get; set; }
        public DateTime Timestamp { get; set; }
        public ItemDto Item { get; set; }
        public string Unit { get; set; }
        public InventoryLogExtendedEntryDto(string itemLotId, double beforeQuantity, double afterQuantity, double receivedQuantity, double shippedQuantity, DateTime timestamp, ItemDto item, string unit)
        {
            ItemLotId = itemLotId;
            BeforeQuantity = beforeQuantity;
            AfterQuantity = afterQuantity;
            ReceivedQuantity = receivedQuantity;
            ShippedQuantity = shippedQuantity;
            Timestamp = timestamp;
            Item = item;
            Unit = unit;
        }
    }
}
