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
        public GoodsReceiptDto(string goodsReceiptId, DateTime timestamp, EmployeeDto employee)
        {
            GoodsReceiptId = goodsReceiptId;
            Timestamp = timestamp;
            Employee = employee;
        }
    }
}
