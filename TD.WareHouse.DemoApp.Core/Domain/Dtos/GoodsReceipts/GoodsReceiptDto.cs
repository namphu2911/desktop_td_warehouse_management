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
        public string GoodsReceiptId { get; private set; }
        public string Supplier { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool IsConfirmed { get; private set; }
        public EmployeeDto Employee { get; private set; }
        public List<GoodsReceiptLotDto> Lots { get; private set; }

        public GoodsReceiptDto(string goodsReceiptId, string supplier, DateTime timestamp, bool isConfirmed, EmployeeDto employee, List<GoodsReceiptLotDto> lots)
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
