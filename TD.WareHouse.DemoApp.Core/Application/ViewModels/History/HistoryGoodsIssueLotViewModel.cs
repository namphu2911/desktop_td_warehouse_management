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
        public string Quantity { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public string Note { get; set; }
        public HistoryGoodsIssueLotViewModel(string itemClassId, string receiver, string itemId, string itemName, string unit, string goodsIssueLotId, string quantity, string? purchaseOrderNumber, string note)
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
