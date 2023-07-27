using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Items
{
    public class CreateItemDto
    {
        public string itemId { get; set; }
        public string itemName { get; set; }
        public double minimumStockLevel { get; set; }
        public decimal price { get; set; }
        public string itemClassId { get; set; }
        public string unit { get; set; }
        public CreateItemDto(string itemId, string itemName, double minimumStockLevel, decimal price, string itemClassId, string unit)
        {
            this.itemId = itemId;
            this.itemName = itemName;
            this.minimumStockLevel = minimumStockLevel;
            this.price = price;
            this.itemClassId = itemClassId;
            this.unit = unit;
        }
    }
}
