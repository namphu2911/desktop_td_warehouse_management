using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Employees;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues
{
    public class GoodsIssueDb
    {
        public string GoodsIssueId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EmployeeId { get; set; }
        public string Receiver { get; set; }
        public bool IsSaved { get; set; }
        public List<GoodsIssueEntry> Entries { get; set; } = new();
        public GoodsIssueDb(string goodsIssueId, DateTime timestamp, string employeeId, string receiver, bool isSaved, List<GoodsIssueEntry> entries)
        {
            GoodsIssueId = goodsIssueId;
            Timestamp = timestamp;
            EmployeeId = employeeId;
            Receiver = receiver;
            IsSaved = isSaved;
            Entries = entries;
        }
    }
}
