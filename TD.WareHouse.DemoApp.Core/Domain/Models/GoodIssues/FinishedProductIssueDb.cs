using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues
{
    public class FinishedProductIssueDb
    {
        public string GoodsIssueId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EmployeeId { get; set; }
        public string Receiver { get; set; }
        public bool IsSaved { get; set; }
        public List<FinishedProductIssueEntry> Entries { get; set; } = new();
        public FinishedProductIssueDb(string goodsIssueId, DateTime timestamp, string employeeId, string receiver, bool isSaved, List<FinishedProductIssueEntry> entries)
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
