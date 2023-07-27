using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class FinishedProductIssueEntryDto
    {
        public string PurchaseOrderNumber { get; set; }
        public double Quantity { get; set; }
        public ItemDto Item { get; set; }
        public string? Note { get; set; }
        public FinishedProductIssueEntryDto(string purchaseOrderNumber, double quantity, string note, ItemDto item)
        {
            PurchaseOrderNumber = purchaseOrderNumber;
            Quantity = quantity;
            Note = note;
            Item = item;
        }
    }
}
