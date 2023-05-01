using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Employees;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts
{
    public class GoodsReceipt
    {
        public string GoodsReceiptId { get; private set; }
        public string Supplier { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool IsConfirmed { get; private set; }
        public Employee Employee { get; private set; }
        public List<GoodsReceiptLot> Lots { get; private set; }

        public GoodsReceipt(string goodsReceiptId, string supplier, DateTime timestamp, bool isConfirmed, Employee employee, List<GoodsReceiptLot> lots)
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
