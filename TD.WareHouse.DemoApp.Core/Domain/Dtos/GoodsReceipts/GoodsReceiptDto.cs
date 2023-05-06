using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class GoodsReceiptDto
    {
        public string GoodsReceiptId { get; set; }
        public DateTime Timestamp { get; set; }
        public EmployeeDto Employee { get; set; }
        public bool IsConfirmed { get; set; }
        public string? Supplier { get; set; }
        public List<GoodsReceiptLotDto> Lots { get; set; }

        public GoodsReceiptDto(string goodsReceiptId, string? supplier, DateTime timestamp, bool isConfirmed, EmployeeDto employee, List<GoodsReceiptLotDto> lots)
        {
            GoodsReceiptId = goodsReceiptId;
            Supplier = supplier;
            Timestamp = timestamp;
            IsConfirmed = isConfirmed;
            Employee = employee;
            Lots = lots;
        }
    }
}
