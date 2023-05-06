using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory
{
    public class ConfirmedLotAdjustmentViewModel : BaseViewModel
    {
        public string LotId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double BeforeQuantity { get; set; }
        public double AfterQuantity { get; set; }
        public string NewPurchaseOrderNumber { get; set; }
        public string OldPurchaseOrderNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Note { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ConfirmedLotAdjustmentViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
           
        }
        public ConfirmedLotAdjustmentViewModel(string lotId, string itemId, string itemName, string unit, double beforeQuantity, double afterQuantity, string newPurchaseOrderNumber, string oldPurchaseOrderNumber, string employeeName, string note)
        {
            LotId = lotId;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            BeforeQuantity = beforeQuantity;
            AfterQuantity = afterQuantity;
            NewPurchaseOrderNumber = newPurchaseOrderNumber;
            OldPurchaseOrderNumber = oldPurchaseOrderNumber;
            EmployeeName = employeeName;
            Note = note;
        }
    }
}
