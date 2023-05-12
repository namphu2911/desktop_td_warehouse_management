using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptNavigationViewModel
    {
        public GoodsReceiptViewModel GoodsReceiptUncompleted { get; set; }
        public GoodsReceiptCompletedViewModel GoodsReceiptCompleted { get; set; }
        public GoodsReceiptNavigationViewModel(GoodsReceiptViewModel goodsReceiptUncompleted, GoodsReceiptCompletedViewModel goodsReceiptCompleted)
        {
            GoodsReceiptUncompleted = goodsReceiptUncompleted;
            GoodsReceiptCompleted = goodsReceiptCompleted;
        }
    }
}
