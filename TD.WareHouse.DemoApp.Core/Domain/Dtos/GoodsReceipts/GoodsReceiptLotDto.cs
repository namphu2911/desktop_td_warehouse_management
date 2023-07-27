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
        public List<GoodsReceiptSublotDto> Sublots { get; set; }
        public double Quantity { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public ItemDto Item { get; set; }
        public EmployeeDto Employee { get; set; }
        public string? Note { get; set; }
        public bool IsExported { get; set; }
        public GoodsReceiptLotDto(string goodsReceiptLotId, List<GoodsReceiptSublotDto> sublots, double quantity, DateTime? productionDate, DateTime? expirationDate, ItemDto item, EmployeeDto employee, string? note, bool isExported)
        {
            GoodsReceiptLotId = goodsReceiptLotId;
            Sublots = sublots;
            Quantity = quantity;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
            Item = item;
            Employee = employee;
            Note = note;
            IsExported = isExported;
        }
    }
}
