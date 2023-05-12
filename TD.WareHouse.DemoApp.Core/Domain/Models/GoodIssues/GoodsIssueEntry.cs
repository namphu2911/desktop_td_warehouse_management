using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues
{
    public class GoodsIssueEntry
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double RequestedQuantity { get; set; }
        public GoodsIssueEntry(string itemId, string itemName, string unit, double requestedQuantity)
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            RequestedQuantity = requestedQuantity;
        }
    }
}
