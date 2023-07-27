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
        public GoodsIssueInternalProgressViewModel GoodsIssueInternalProgress { get; set; }
        public GoodsIssueExternalProgressViewModel GoodsIssueExternalProgress { get; set; }
        public GoodsIssueViewModel(GoodsIssueExternalViewModel goodsIssueExternal, GoodsIssueInternalViewModel goodsIssueInternal, GoodsIssueInternalProgressViewModel goodsIssueInternalProgress, GoodsIssueExternalProgressViewModel goodsIssueExternalProgress)
        {
            GoodsIssueExternal = goodsIssueExternal;
            GoodsIssueInternal = goodsIssueInternal;
            GoodsIssueInternalProgress = goodsIssueInternalProgress;
            GoodsIssueExternalProgress = goodsIssueExternalProgress;
        }
    }
}
