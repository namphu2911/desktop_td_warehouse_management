using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class RemovedFinishedProductReceiptEntryDto
    {
        public string itemId { get; set; }
        public string unit { get; set; }
        public string purchaseOrderNumber { get; set; }
        public RemovedFinishedProductReceiptEntryDto(string itemId, string unit, string purchaseOrderNumber)
        {
            this.itemId = itemId;
            this.unit = unit;
            this.purchaseOrderNumber = purchaseOrderNumber;
        }
    }
}
