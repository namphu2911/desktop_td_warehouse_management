using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory
{
    public class InventoryLogEntryItemLotDto
    {
        public ItemDto Item { get; set; }
        public double TotalQuantity { get; set; }
        public List<LotForInventoryLogEntryItemLotDto> Lots { get; set; }
        public InventoryLogEntryItemLotDto(ItemDto item, double totalQuantity, List<LotForInventoryLogEntryItemLotDto> lots)
        {
            Item = item;
            TotalQuantity = totalQuantity;
            Lots = lots;
        }
    }
}
