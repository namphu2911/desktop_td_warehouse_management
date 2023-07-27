using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class CreateExternalGoodsIssueEntryDto
    {
        public string purchaseOrderNumber { get; set; }
        public double quantity { get; set; }
        public string itemId { get; set; }
        public string unit { get; set; }
        public string? note { get; set; }
        public CreateExternalGoodsIssueEntryDto(string purchaseOrderNumber, double quantity, string itemId, string unit, string? note)
        {
            this.purchaseOrderNumber = purchaseOrderNumber;
            this.quantity = quantity;
            this.itemId = itemId;
            this.unit = unit;
            this.note = note;
        }
    }
}
