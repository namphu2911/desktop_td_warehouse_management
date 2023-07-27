using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.History
{
    public class HistoryViewModel : BaseViewModel
    {
        public HistoryGoodsIssueViewModel HistoryGoodsIssue { get; set; }
        public HistoryGoodsReceiptViewModel HistoryGoodsReceipt { get; set; }   
        public HistoryFinishedProductReceiptViewModel HistoryFinishedProductReceipt { get; set; }
        public HistoryFinishedProductIssueViewModel HistoryFinishedProductIssue { get; set; }
        public HistoryViewModel(HistoryGoodsIssueViewModel historyGoodsIssue, HistoryGoodsReceiptViewModel historyGoodsReceipt, HistoryFinishedProductReceiptViewModel historyFinishedProductReceipt, HistoryFinishedProductIssueViewModel historyFinishedProductIssue)
        {
            HistoryGoodsIssue = historyGoodsIssue;
            HistoryGoodsReceipt = historyGoodsReceipt;
            HistoryFinishedProductReceipt = historyFinishedProductReceipt;
            HistoryFinishedProductIssue = historyFinishedProductIssue;
        }
    }
}
