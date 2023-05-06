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
        public string GoodsReceiptLotId { get; set; }
        public string? LocationId { get; set; }
        public double Quantity { get; set; }
        public double? SublotSize { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public ItemDto Item { get; set; }
        public EmployeeDto Employee { get; set; }
        public string? Note { get; set; }
        public GoodsReceiptLotDto(string goodsReceiptLotId, string? locationId, double quantity, double? sublotSize, string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, string note, EmployeeDto employee, ItemDto item)
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
        }
    }
}
