using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptLotForGoodsReceiptCompletedView
    {
        public string GoodsReceiptLotId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string PurchaseOrderNumber { get; set; }

        public ICommand DeleteEntryCommand { get; set; }
        public event Action? OnRemoved;
        public GoodsReceiptLotForGoodsReceiptCompletedView(string goodsReceiptLotId, string itemId, string itemName, double quantity, string unit, string purchaseOrderNumber)
        {
            GoodsReceiptLotId = goodsReceiptLotId;
            ItemId = itemId;
            ItemName = itemName;
            Quantity = quantity;
            Unit = unit;
            PurchaseOrderNumber = purchaseOrderNumber;
            DeleteEntryCommand = new RelayCommand(DeleteEntry);
        }
        private void DeleteEntry()
        {
            OnRemoved?.Invoke();
        }
    }
}
