using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Items
{
    public class FixItemDto
    {
        public string itemId { get; set; }
        public string itemName { get; set; }
        public double minimumStockLevel { get; set; }
        public decimal price { get; set; }
        public string unit { get; set; }
        public string itemClassId { get; set; }
        public double? packetSize { get; set; }
        public string? packetUnit { get; set; }
        public FixItemDto(string itemId, string itemName, double minimumStockLevel, decimal price, string unit, string itemClassId, double? packetSize, string? packetUnit)
        {
            this.itemId = itemId;
            this.itemName = itemName;
            this.minimumStockLevel = minimumStockLevel;
            this.price = price;
            this.unit = unit;
            this.itemClassId = itemClassId;
            this.packetSize = packetSize;
            this.packetUnit = packetUnit;
        }
    }
}
