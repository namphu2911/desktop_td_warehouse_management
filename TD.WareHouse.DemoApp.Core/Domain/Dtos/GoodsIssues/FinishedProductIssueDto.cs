using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class FinishedProductIssueDto
    {
        public string FinishedProductIssueId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Receiver { get; set; }
        public EmployeeDto Employee { get; set; }
        public List<FinishedProductIssueEntryDto> Entries { get; set; }
        public FinishedProductIssueDto(string finishedProductIssueId, DateTime timestamp, string receiver, EmployeeDto employee, List<FinishedProductIssueEntryDto> entries)
        {
            FinishedProductIssueId = finishedProductIssueId;
            Timestamp = timestamp;
            Receiver = receiver;
            Employee = employee;
            Entries = entries;
        }
    }
}
