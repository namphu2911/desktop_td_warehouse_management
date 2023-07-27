using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class CreateInternalGoodsIssueDto
    {
        public string goodsIssueId { get; set; }
        public string receiver { get; set; }
        public DateTime timestamp { get; set; }
        public string employeeId { get; set; }
        public List<CreateInternalGoodsIssueEntryDto> entries { get; set; }
        public CreateInternalGoodsIssueDto(string goodsIssueId, string receiver, DateTime timestamp, string employeeId, List<CreateInternalGoodsIssueEntryDto> entries)
        {
            this.goodsIssueId = goodsIssueId;
            this.receiver = receiver;
            this.timestamp = timestamp;
            this.employeeId = employeeId;
            this.entries = entries;
        }
    }
}
