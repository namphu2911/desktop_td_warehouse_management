using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class CreateGoodsIssueDto
    {
        public string GoodsIssueId { get; set; }
        public string Receiver { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string EmployeeId { get; set; }
        public List<CreateGoodsIssueEntryDto> Entries { get; set; }
        public CreateGoodsIssueDto(string goodsIssueId, string receiver, string purchaseOrderNumber, DateTime timestamp, string employeeId, List<CreateGoodsIssueEntryDto> entries)
        {
            GoodsIssueId = goodsIssueId;
            Receiver = receiver;
            PurchaseOrderNumber = purchaseOrderNumber;
            Timestamp = timestamp;
            EmployeeId = employeeId;
            Entries = entries;
        }



    }
}
