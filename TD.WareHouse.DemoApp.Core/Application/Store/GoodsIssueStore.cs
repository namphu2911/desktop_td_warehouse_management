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
        public ObservableCollection<string> Receivers { get; private set; }
        public ObservableCollection<string> GoodsIssueIds { get; private set; }
        public ObservableCollection<string> FinishedProductIssueIds { get; private set; } 
        public ObservableCollection<string> FinishedProductIssueReceivers { get; private set; }
        public GoodsIssueStore()
        {
            Receivers = new ObservableCollection<string>();
            GoodsIssueIds = new ObservableCollection<string>();
            FinishedProductIssueIds = new ObservableCollection<string>();
            FinishedProductIssueReceivers = new ObservableCollection<string>();
        }
        public void SetGoodsIssueReceivers(List<string> goodsIssueReceiver)
        {
            Receivers = new ObservableCollection<string>(goodsIssueReceiver.OrderBy(s => s));
        }

        public void SetGoodsIssueIds(List<string> goodsIssueId)
        {
            GoodsIssueIds = new ObservableCollection<string>(goodsIssueId.OrderBy(s => s));
        }

        public void SetFinishedProductIssueIds(List<string> finishedProductIssueId)
        {
            FinishedProductIssueIds = new ObservableCollection<string>(finishedProductIssueId.OrderBy(s => s));
        }
        public void SetFinishedProductIssueReceivers(List<string> finishedProductIssueReceiver)
        {
            FinishedProductIssueReceivers = new ObservableCollection<string>(finishedProductIssueReceiver.OrderBy(s => s));
        }
    }
}
