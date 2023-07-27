using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueLotForGoodsIssueInternalProgressViewModel : BaseViewModel
    {
        public string GoodsIssueLotId { get; set; }
        public double Quantity { get; set; }
        public GoodsIssueLotForGoodsIssueInternalProgressViewModel(string goodsIssueLotId, double quantity)
        {
            GoodsIssueLotId = goodsIssueLotId;
            Quantity = quantity;
        }
    }
}
