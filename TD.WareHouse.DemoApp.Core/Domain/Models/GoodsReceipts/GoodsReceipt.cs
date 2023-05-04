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
        public string GoodsReceiptId { get; set; }
        public string Supplier { get; set; }
       
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private GoodsReceipt() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public GoodsReceipt(string goodsReceiptId, string supplier)
        {
            GoodsReceiptId = goodsReceiptId;
            Supplier = supplier;
            
        }
    }
}
