using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueLotForGoodsIssueProgressViewModel : BaseViewModel
    {
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string GoodsIssueLotId { get; set; }
        public double Quantity { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public GoodsIssueLotForGoodsIssueProgressViewModel(string itemClassId, string itemId, string itemName, string unit, string goodsIssueLotId, double quantity, string? purchaseOrderNumber)
        {
            ItemClassId = itemClassId;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            GoodsIssueLotId = goodsIssueLotId;
            Quantity = quantity;
            PurchaseOrderNumber = purchaseOrderNumber;
        }
    }
}
