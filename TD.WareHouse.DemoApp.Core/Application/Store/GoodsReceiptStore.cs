using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;

namespace TD.WareHouse.DemoApp.Core.Application.Store
{
    public class GoodsReceiptStore
    {
        public List<GoodsReceipt> GoodsReceiptsAll { get; private set; }
        public List<GoodsReceipt> GoodsReceiptsUncompleted { get; private set; }
        public List<GoodsReceipt> GoodsReceiptsCompleted { get; private set; }
        public ObservableCollection<string> Suppliers { get; private set; }
        public ObservableCollection<string> UncompletedGoodsReceiptIds { get; private set; }
        public ObservableCollection<string> CompletedGoodsReceiptIds { get; private set; }

        public ObservableCollection<string> FinishedProductReceiptIds { get; private set; }
        public GoodsReceiptStore()
        {
            GoodsReceiptsAll = new List<GoodsReceipt>();
            GoodsReceiptsUncompleted = new List<GoodsReceipt>();
            GoodsReceiptsCompleted = new List<GoodsReceipt>();

            Suppliers = new ObservableCollection<string>();
            UncompletedGoodsReceiptIds = new ObservableCollection<string>();
            CompletedGoodsReceiptIds = new ObservableCollection<string>();

            FinishedProductReceiptIds = new ObservableCollection<string>();
        }
        public void SetGoodsIssueSuppliers(List<string> suppliersDtos)
        {
            Suppliers = new ObservableCollection<string>(suppliersDtos.OrderBy(s => s));
        }

        public void SetUncompletedGoodsReceipts(IEnumerable<GoodsReceipt> uncompletedGoodsReceipts)
        {
            GoodsReceiptsUncompleted = uncompletedGoodsReceipts.ToList();
            UncompletedGoodsReceiptIds = new ObservableCollection<string>(GoodsReceiptsUncompleted.Select(i => i.GoodsReceiptId).OrderBy(s => s));
        }

        public void SetCompletedGoodsReceipts(IEnumerable<GoodsReceipt> completedGoodsReceipts)
        {
            GoodsReceiptsCompleted = completedGoodsReceipts.ToList();
            CompletedGoodsReceiptIds = new ObservableCollection<string>(GoodsReceiptsCompleted.Select(i => i.GoodsReceiptId).OrderBy(s => s));
        }

        public void SetFinishedProductReceiptIds(List<string> finishedProductReceiptIdsDtos)
        {
            FinishedProductReceiptIds = new ObservableCollection<string>(finishedProductReceiptIdsDtos.OrderBy(s => s));
        }
    }
}
