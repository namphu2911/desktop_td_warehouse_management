using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class GoodsIssueDto
    {
        public string GoodsIssueId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Receiver { get; set; }
        public EmployeeDto Employee { get; set; }
        public List<GoodsIssueEntryDto> Entries { get; set; }
        public GoodsIssueDto(string goodsIssueId, DateTime timestamp, string receiver, EmployeeDto employee, List<GoodsIssueEntryDto> entries)
        {
            GoodsIssueId = goodsIssueId;
            Timestamp = timestamp;
            Receiver = receiver;
            Employee = employee;
            Entries = entries;
        }
    }
}
