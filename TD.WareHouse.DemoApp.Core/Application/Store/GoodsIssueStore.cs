using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;

namespace TD.WareHouse.DemoApp.Core.Application.Store
{
    public class GoodsIssueStore
    {
        public List<GoodsIssue> GoodsIssuesUnconfirmed { get; private set; }
        public ObservableCollection<string> Receivers { get; private set; }
        public ObservableCollection<string> GoodsIssueIds { get; private set; }
        public GoodsIssueStore()
        {
            GoodsIssuesUnconfirmed = new List<GoodsIssue>();
            Receivers = new ObservableCollection<string>();
            GoodsIssueIds = new ObservableCollection<string>();
        }
        public void SetGoodsIssueReceivers(List<string> goodsIssueDtos)
        {
            Receivers = new ObservableCollection<string>(goodsIssueDtos.OrderBy(s => s));
        }

        public void SetUnconfirmedGoodsIssues(IEnumerable<GoodsIssue> goodsIssues)
        {
            GoodsIssuesUnconfirmed = goodsIssues.ToList();
            GoodsIssueIds = new ObservableCollection<string>(GoodsIssuesUnconfirmed.Select(i => i.GoodsIssueId).OrderBy(s => s));
        }
    }
}
