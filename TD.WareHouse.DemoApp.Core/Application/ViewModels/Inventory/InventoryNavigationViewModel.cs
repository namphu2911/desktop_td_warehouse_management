using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory
{
    public class InventoryNavigationViewModel
    {
        public InventoryViewModel Inventory { get; set; }
        public InventoryHistoryViewModel InventoryHistory { get; set; }
        public InventoryNavigationViewModel(InventoryViewModel inventory, InventoryHistoryViewModel inventoryHistory)
        {
            Inventory = inventory;
            InventoryHistory = inventoryHistory;
        }
    }
}
