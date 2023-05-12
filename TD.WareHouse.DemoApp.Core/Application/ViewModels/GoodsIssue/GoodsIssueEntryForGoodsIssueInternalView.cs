using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueEntryForGoodsIssueInternalView
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public double RequestedQuantity { get; set; }
        public string Unit { get; set; }
        public ICommand DeleteEntryCommand { get; set; }
        public event Action? OnRemoved;
        public GoodsIssueEntryForGoodsIssueInternalView(string itemId, string itemName, double requestedQuantity, string unit)
        {
            ItemId = itemId;
            ItemName = itemName;
            RequestedQuantity = requestedQuantity;
            Unit = unit;
            DeleteEntryCommand = new RelayCommand(DeleteEntry);
        }
        private void DeleteEntry()
        {
            OnRemoved?.Invoke();
        }
    }
}
