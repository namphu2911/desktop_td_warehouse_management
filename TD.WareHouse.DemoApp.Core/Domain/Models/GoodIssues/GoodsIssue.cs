using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Employees;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues
{
    public class GoodsIssue
    {
        public string GoodsIssueId { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string Receiver { get; set; }
        public Employee Employee { get; set; }        
        public List<GoodsIssueEntry> Entries { get; set; }
        public GoodsIssue(string goodsIssueId, string? purchaseOrderNumber, DateTime timestamp, string receiver, Employee employee, List<GoodsIssueEntry> entries)
        {
            GoodsIssueId = goodsIssueId;
            PurchaseOrderNumber = purchaseOrderNumber;
            Timestamp = timestamp;
            Receiver = receiver;
            Employee = employee;
            Entries = entries;
        }
    }
}
