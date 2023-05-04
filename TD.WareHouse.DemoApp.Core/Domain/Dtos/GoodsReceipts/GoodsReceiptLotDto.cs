using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class GoodsReceiptLotDto
    {
        public string GoodsReceiptLotId { get; private set; }
        public string? LocationId { get; private set; }
        public double Quantity { get; private set; }
        public double? SublotSize { get; private set; }
        public string PurchaseOrderNumber { get; private set; }
        public DateTime? ProductionDate { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public string? Note { get; private set; }
        public EmployeeDto Employee { get; private set; }
        public ItemDto Item { get; private set; }
        public string Unit { get; private set; }
        public GoodsReceiptLotDto(string goodsReceiptLotId, string? locationId, double quantity, double? sublotSize, string purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, string note, EmployeeDto employee, ItemDto item, string unit)
        {
            GoodsReceiptLotId = goodsReceiptLotId;
            LocationId = locationId;
            Quantity = quantity;
            SublotSize = sublotSize;
            PurchaseOrderNumber = purchaseOrderNumber;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
            Note = note;
            Employee = employee;
            Item = item;
            Unit = unit;
        }
    }
}
