using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Employees;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts
{
    public class GoodsReceiptLot
    {
        public string GoodsReceiptLotId { get; private set; }
        public string? LocationId { get; private set; }
        public double Quantity { get; private set; }
        public double? SublotSize { get; private set; }
        public string? PurchaseOrderNumber { get; private set; }
        public DateTime? ProductionDate { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public string? Note { get; private set; }
        public Employee Employee { get; private set; }
        public Item Item { get; private set; }
        public string Unit { get; private set; }
        public GoodsReceiptLot(string goodsReceiptLotId, string? locationId, double quantity, double? sublotSize, string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, string? note, Employee employee, Item item, string unit)
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
