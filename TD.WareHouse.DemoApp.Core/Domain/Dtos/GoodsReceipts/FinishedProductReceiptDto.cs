using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class FinishedProductReceiptDto
    {
        public string FinishedProductReceiptId { get; set; }
        public DateTime Timestamp { get; set; }
        public EmployeeDto Employee { get; set; }
        public List<FinishedProductReceiptEntryDto> Entries { get; set; }

        public FinishedProductReceiptDto(string finishedProductReceiptId, DateTime timestamp, EmployeeDto employee, List<FinishedProductReceiptEntryDto> entries)
        {
            FinishedProductReceiptId = finishedProductReceiptId;
            Timestamp = timestamp;
            Employee = employee;
            Entries = entries;
        }
    }
}
