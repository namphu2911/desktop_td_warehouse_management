using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class CreateFinishedProductReceiptEntryDto
    {
        public string purchaseOrderNumber { get; set; }
        public string itemId { get; set; }
        public string unit { get; set; }
        public double quantity { get; set; }
        public string? note { get; set; }
        public CreateFinishedProductReceiptEntryDto(string purchaseOrderNumber, string itemId, string unit, double quantity, string? note)
        {
            this.purchaseOrderNumber = purchaseOrderNumber;
            this.itemId = itemId;
            this.unit = unit;
            this.quantity = quantity;
            this.note = note;
        }
    }
}
