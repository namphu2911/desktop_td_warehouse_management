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
        public string GoodsIssueLotId { get; set; }
        public double Quantity { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public GoodsIssueLotForGoodsIssueProgressViewModel(string goodsIssueLotId, double quantity, string? purchaseOrderNumber)
        {
            GoodsIssueLotId = goodsIssueLotId;
            Quantity = quantity;
            PurchaseOrderNumber = purchaseOrderNumber;
        }
    }
}
