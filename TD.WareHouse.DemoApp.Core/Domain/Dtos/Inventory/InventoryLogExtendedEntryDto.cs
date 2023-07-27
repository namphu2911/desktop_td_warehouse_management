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
        public double BeforeQuantity { get; set; }
        public double ReceivedQuantity { get; set; }
        public double ShippedQuantity { get; set; }
        public double AfterQuantity { get; set; }
        public DateTime Timestamp { get; set; }
        public ItemDto Item { get; set; }
        public string ItemLotId { get; set; }
        public InventoryLogExtendedEntryDto(double beforeQuantity, double receivedQuantity, double shippedQuantity, double afterQuantity, DateTime timestamp, ItemDto item, string itemLotId)
        {
            BeforeQuantity = beforeQuantity;
            ReceivedQuantity = receivedQuantity;
            ShippedQuantity = shippedQuantity;
            AfterQuantity = afterQuantity;
            Timestamp = timestamp;
            Item = item;
            ItemLotId = itemLotId;
        }
    }
}
