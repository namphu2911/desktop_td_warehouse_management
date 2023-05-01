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
        public List<GoodsReceipt> GoodsReceipts { get; private set; }
        public ObservableCollection<string> Suppliers { get; private set; }
        public GoodsReceiptStore()
        {
            GoodsReceipts = new List<GoodsReceipt>();
            Suppliers = new ObservableCollection<string>();
        }
        public void SetGoodsReceipts(IEnumerable<GoodsReceipt> goodsReceipts)
        {
            GoodsReceipts = goodsReceipts.ToList();
            Suppliers = new ObservableCollection<string>(GoodsReceipts.Select(i => i.Supplier).OrderBy(s => s));
        }
    }
}
