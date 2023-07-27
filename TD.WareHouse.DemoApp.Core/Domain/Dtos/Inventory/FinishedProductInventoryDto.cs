using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory
{
    public class FinishedProductInventoryDto
    {
        public string PurchaseOrderNumber { get; set; }
        public double Quantity { get; set; }
        public DateTime Timestamp { get; set; }
        public ItemDto Item { get; set; }
        public FinishedProductInventoryDto(string purchaseOrderNumber, double quantity, DateTime timestamp, ItemDto item)
        {
            PurchaseOrderNumber = purchaseOrderNumber;
            Quantity = quantity;
            Timestamp = timestamp;
            Item = item;
        }
    }
}
