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
        public List<GoodsReceipt> GoodsReceiptsUnconfirmed { get; private set; }
        public ObservableCollection<string> Suppliers { get; private set; }
        public ObservableCollection<string> GoodsReceiptIds { get; private set; }
        public GoodsReceiptStore()
        {
            GoodsReceiptsAll = new List<GoodsReceipt>();
            GoodsReceiptsUnconfirmed = new List<GoodsReceipt>();
            GoodsReceiptIds = new ObservableCollection<string>();
            Suppliers = new ObservableCollection<string>();
        }
        public void SetGoodsReceipts(IEnumerable<GoodsReceipt> goodsReceipts)
        {
            GoodsReceiptsAll = goodsReceipts.ToList();
            Suppliers = new ObservableCollection<string>(GoodsReceiptsAll.Select(i => i.Supplier).OrderBy(s => s));
        }

        public void SetUnconfirmedGoodsReceipts(IEnumerable<GoodsReceipt> goodsReceipts)
        {
            GoodsReceiptsUnconfirmed = goodsReceipts.ToList();
            GoodsReceiptIds = new ObservableCollection<string>(GoodsReceiptsUnconfirmed.Select(i => i.GoodsReceiptId).OrderBy(s => s));
        }
    }
}
