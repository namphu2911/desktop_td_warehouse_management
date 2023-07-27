using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueEntryForGoodsIssueInternalProgressViewModel
    {
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double RequestedQuantity { get; set; }
        public List<GoodsIssueLotForGoodsIssueInternalProgressViewModel> LotForGoodsIssueProgressViewModels { get; set; }
        public string State => (RequestedQuantity <= LotForGoodsIssueProgressViewModels.Sum(i => i.Quantity)) ? "Đã xuất đủ/dư" : "Chưa xuất hết";
        public GoodsIssueEntryForGoodsIssueInternalProgressViewModel(string itemClassId, string itemId, string itemName, string unit, double requestedQuantity, List<GoodsIssueLotForGoodsIssueInternalProgressViewModel> lotForGoodsIssueProgressViewModels)
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
