using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class RemovedFinishedProductIssueEntryDto
    {
        public string finishedProductIssueId { get; set; }
        public string itemId { get; set; }
        public string unit { get; set; }
        public string purchaseOrderNumber { get; set; }
        public RemovedFinishedProductIssueEntryDto(string finishedProductIssueId, string itemId, string unit, string purchaseOrderNumber)
        {
            this.finishedProductIssueId = finishedProductIssueId;
            this.itemId = itemId;
            this.unit = unit;
            this.purchaseOrderNumber = purchaseOrderNumber;
        }
    }
}
