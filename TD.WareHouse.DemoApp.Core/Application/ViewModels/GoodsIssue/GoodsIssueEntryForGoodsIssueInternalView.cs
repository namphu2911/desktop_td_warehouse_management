using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueEntryForGoodsIssueInternalView
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public double PlannedQuantity { get; set; }
        public string Unit { get; set; }
        public GoodsIssueEntryForGoodsIssueInternalView(string itemId, string itemName, double plannedQuantity, string unit)
        {
            ItemId = itemId;
            ItemName = itemName;
            PlannedQuantity = plannedQuantity;
            Unit = unit;
        }
    }
}
