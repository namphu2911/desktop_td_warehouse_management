using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueEntryForGoodsIssueExternalView
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string Note { get; set; }
        public ICommand DeleteEntryCommand { get; set; }
        public event Action? OnRemoved;
        public GoodsIssueEntryForGoodsIssueExternalView(string itemId, string itemName, double quantity, string unit, string purchaseOrderNumber, string note)
        {
            ItemId = itemId;
            ItemName = itemName;
            Quantity = quantity;
            Unit = unit;
            PurchaseOrderNumber = purchaseOrderNumber;
            Note = note;

            DeleteEntryCommand = new RelayCommand(DeleteEntry);
        }
        private void DeleteEntry()
        {
            OnRemoved?.Invoke();
        }
    }
}
