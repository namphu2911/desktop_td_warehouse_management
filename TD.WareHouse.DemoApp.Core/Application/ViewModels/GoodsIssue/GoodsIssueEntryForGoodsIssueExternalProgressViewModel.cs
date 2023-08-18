using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueEntryForGoodsIssueExternalProgressViewModel
    {
        public string PurchaseOrderNumber { get; set; }
        public double Quantity { get; set; }
        public string? Note { get; set; }
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public ICommand DeleteEntryCommand { get; set; }
        public event Action? OnRemoved;
        public GoodsIssueEntryForGoodsIssueExternalProgressViewModel(string purchaseOrderNumber, double quantity, string? note, string itemClassId, string itemId, string itemName, string unit)
        {
            PurchaseOrderNumber = purchaseOrderNumber;
            Quantity = quantity;
            Note = note;
            ItemClassId = itemClassId;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;

            DeleteEntryCommand = new RelayCommand(DeleteEntry);
        }

        private void DeleteEntry()
        {
            OnRemoved?.Invoke();
        }
    }
}
