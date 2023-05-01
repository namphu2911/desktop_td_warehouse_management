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
        public Item Item { get; set; }
        public double RequestedQuantity { get; set; }
        public GoodsIssueEntry(Item item, double requestedQuantity)
        {
            Item = item;
            RequestedQuantity = requestedQuantity;
        }
    }
}
