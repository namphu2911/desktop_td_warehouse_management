using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptEntryForGoodsReceiptCompletedView
    {
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string NewPurchaseOrderNumber { get; set; }
        public double Quantity { get; set; }
        public string Note { get; set; }
        public ICommand DeleteEntryCommand { get; set; }
        public event Action? OnRemoved;
        public GoodsReceiptEntryForGoodsReceiptCompletedView(string itemClassId, string itemId, string itemName, string unit, string purchaseOrderNumber, string newPurchaseOrderNumber, double quantity, string note)
        {
            ItemClassId = itemClassId;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            PurchaseOrderNumber = purchaseOrderNumber;
            NewPurchaseOrderNumber = newPurchaseOrderNumber;
            Quantity = quantity;
            Note = note;

            DeleteEntryCommand = new RelayCommand(DeleteEntry);
        }

        private void DeleteEntry()
        {
            OnRemoved?.Invoke();
        }
    }
}
