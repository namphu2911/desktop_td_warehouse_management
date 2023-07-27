using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.History
{
    public class HistoryGoodsReceiptLotViewModel : BaseViewModel
    {
        public string ItemClassId { get; set; }
        public string? Supplier { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string GoodsReceiptLotId { get; set; }
        public double Quantity { get; set; }
        public string? Note  { get; set; }   
        public HistoryGoodsReceiptLotViewModel(string itemClassId, string? supplier, string itemId, string itemName, string unit, string goodsReceiptLotId, double quantity, string? note)
        {
            ItemClassId = itemClassId;
            Supplier = supplier;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            GoodsReceiptLotId = goodsReceiptLotId;
            Quantity = quantity;
            Note = note;
        }
    }
}
