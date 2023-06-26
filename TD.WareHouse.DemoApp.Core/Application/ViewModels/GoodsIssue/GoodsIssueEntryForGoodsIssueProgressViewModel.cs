using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueEntryForGoodsIssueProgressViewModel
    {
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double RequestedQuantity { get; set; }
        public List<GoodsIssueLotForGoodsIssueProgressViewModel> LotForGoodsIssueProgressViewModels { get; set; }
        public GoodsIssueEntryForGoodsIssueProgressViewModel(string itemClassId, string itemId, string itemName, string unit, double requestedQuantity, List<GoodsIssueLotForGoodsIssueProgressViewModel> lotForGoodsIssueProgressViewModels)
        {
            ItemClassId = itemClassId;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            RequestedQuantity = requestedQuantity;
            LotForGoodsIssueProgressViewModels = lotForGoodsIssueProgressViewModels;
        }
    }
}
