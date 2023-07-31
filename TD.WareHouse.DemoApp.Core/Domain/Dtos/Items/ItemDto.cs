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
        public decimal Price { get; set; }
        public double? PacketSize { get; set; }
        public string? PacketUnit { get; set; }
        public string ItemClassId { get; set; }
        public ItemDto(string itemId, string itemName, string unit, double minimumStockLevel, decimal price, double? packetSize, string? packetUnit, string itemClassId)
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            MinimumStockLevel = minimumStockLevel;
            Price = price;
            PacketSize = packetSize;
            PacketUnit = packetUnit;
            ItemClassId = itemClassId;
        }
    }
}
