using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory
{
    public class InventoryLogEntryDto
    {
        public string ItemLotId { get; set; }
        public double BeforeQuantity { get; set; }
        public double ChangedQuantity { get; set; }
        public DateTime Timestamp { get; set; }
        public ItemDto Item { get; set; }
        public InventoryLogEntryDto(string itemLotId, double beforeQuantity, double changedQuantity, DateTime timestamp, ItemDto item)
        {
            ItemLotId = itemLotId;
            BeforeQuantity = beforeQuantity;
            ChangedQuantity = changedQuantity;
            Timestamp = timestamp;
            Item = item;
        }
    }
}
