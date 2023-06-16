using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts
{
    public class GoodsReceiptDb
    {
        public string GoodsReceiptId { get; set; }
        public string? Supplier { get; set; }
        public string EmployeeId { get; set; }
        public List<GoodsReceiptLot> Lots { get; set; }
        public GoodsReceiptDb(string goodsReceiptId, string? supplier, string employeeId, List<GoodsReceiptLot> lots)
        {
            GoodsReceiptId = goodsReceiptId;
            Supplier = supplier;
            EmployeeId = employeeId;
            Lots = lots;
        }
    }
}
