using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class CreateGoodsIssueDto
    {
        public string goodsIssueId { get; set; }
        public string receiver { get; set; }
        public string? purchaseOrderNumber { get; set; }
        public DateTime timestamp { get; set; }
        public string employeeId { get; set; }
        public List<CreateGoodsIssueEntryDto> entries { get; set; }
        public CreateGoodsIssueDto(string goodsIssueId, string receiver, string? purchaseOrderNumber, DateTime timestamp, string employeeId, List<CreateGoodsIssueEntryDto> entries)
        {
            this.goodsIssueId = goodsIssueId;
            this.receiver = receiver;
            this.purchaseOrderNumber = purchaseOrderNumber;
            this.timestamp = timestamp;
            this.employeeId = employeeId;
            this.entries = entries;
        }
    }
}
