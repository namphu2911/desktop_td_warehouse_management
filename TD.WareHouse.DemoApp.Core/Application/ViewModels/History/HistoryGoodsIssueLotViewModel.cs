using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.History
{
    public class HistoryGoodsIssueLotViewModel : BaseViewModel
    {
        public string ItemClassId { get; set; }
        public string Receiver { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string GoodsIssueLotId { get; set; }
        public double Quantity { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string Note { get; set; }
        public HistoryGoodsIssueLotViewModel(string receiver, string purchaseOrderNumber, string itemClassId,  string itemId, string itemName, string unit, string goodsIssueLotId, double quantity, string note)
        {
            ItemClassId = itemClassId;
            Receiver = receiver;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            GoodsIssueLotId = goodsIssueLotId;
            Quantity = quantity;
            PurchaseOrderNumber = purchaseOrderNumber;
            Note = note;
        }
    }
}
