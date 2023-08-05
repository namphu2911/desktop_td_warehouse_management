using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts
{
    public class FinishedProductReceiptDb
    {
        public string FinishedProductReceiptId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsSaved { get; set; }
        public List<FinishedProductReceiptEntry> Entries { get; set; }
        public FinishedProductReceiptDb(string finishedProductReceiptId, string employeeId, DateTime timestamp, bool isSaved, List<FinishedProductReceiptEntry> entries)
        {
            FinishedProductReceiptId = finishedProductReceiptId;
            EmployeeId = employeeId;
            Timestamp = timestamp;
            IsSaved = isSaved;
            Entries = entries;
        }
    }
}
