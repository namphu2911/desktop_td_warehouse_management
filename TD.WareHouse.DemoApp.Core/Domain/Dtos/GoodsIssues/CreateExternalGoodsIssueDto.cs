using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class CreateExternalGoodsIssueDto
    {
        public string finishedProductIssueId { get; set; }
        public string receiver { get; set; }
        public string employeeId { get; set; }
        public List<CreateExternalGoodsIssueEntryDto> entries { get; set; }
        public CreateExternalGoodsIssueDto(string finishedProductIssueId, string receiver, string employeeId, List<CreateExternalGoodsIssueEntryDto> entries)
        {
            this.finishedProductIssueId = finishedProductIssueId;
            this.receiver = receiver;
            this.employeeId = employeeId;
            this.entries = entries;
        }
    }
}
