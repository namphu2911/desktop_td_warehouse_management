using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Items
{
    public class ItemDto
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double MinimumStockLevel { get; set; }
        public double Price { get; set; }
        public string ItemClassId { get; set; }
        public ItemDto(string itemId, string itemName, string unit, double minimumStockLevel, double price, string itemClassId)
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            MinimumStockLevel = minimumStockLevel;
            Price = price;
            ItemClassId = itemClassId;
        }
    }
}
