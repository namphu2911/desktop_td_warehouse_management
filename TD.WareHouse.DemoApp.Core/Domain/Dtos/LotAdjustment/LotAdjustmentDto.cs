using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.LotAdjustment
{
    public class LotAdjustmentDto
    {
        public string LotId { get; set; }
        public ItemDto Item { get; set; }
        public double BeforeQuantity { get; set; }
        public double AfterQuantity { get; set; }
        public string NewPurchaseOrderNumber { get; set; }
        public string OldPurchaseOrderNumber { get; set; }
        public EmployeeDto Employee { get; set; }
        public string Note { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsConfirmed { get; set; }
        public string Unit { get; set; }
        public LotAdjustmentDto(string lotId, ItemDto item, double beforeQuantity, double afterQuantity, string newPurchaseOrderNumber, string oldPurchaseOrderNumber, EmployeeDto employee, string note, DateTime timestamp, bool isConfirmed, string unit)
        {
            LotId = lotId;
            Item = item;
            BeforeQuantity = beforeQuantity;
            AfterQuantity = afterQuantity;
            NewPurchaseOrderNumber = newPurchaseOrderNumber;
            OldPurchaseOrderNumber = oldPurchaseOrderNumber;
            Employee = employee;
            Note = note;
            Timestamp = timestamp;
            IsConfirmed = isConfirmed;
            Unit = unit;
        }
    }
}
