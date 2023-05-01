using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueEntryLotForGoodsIssueProgressView
    {
        public string GoodsIssueLotId { get; set; }
        public GoodsIssueEntryLotForGoodsIssueProgressView(string goodsIssueLotId)
        {
            GoodsIssueLotId = goodsIssueLotId;
        }
    }
}
