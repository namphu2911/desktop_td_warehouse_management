using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueViewModel : BaseViewModel
    {
        public GoodsIssueExternalViewModel GoodsIssueExternal { get; set; }
        public GoodsIssueInternalViewModel GoodsIssueInternal { get; set; }
        public GoodsIssueProgressViewModel GoodsIssueProgress { get; set; }
        public GoodsIssueViewModel(GoodsIssueExternalViewModel goodsIssueExternal, GoodsIssueInternalViewModel goodsIssueInternal, GoodsIssueProgressViewModel goodsIssueProgress)
        {
            GoodsIssueExternal = goodsIssueExternal;
            GoodsIssueInternal = goodsIssueInternal;
            GoodsIssueProgress = goodsIssueProgress;
        }
    }
}
