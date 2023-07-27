using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class CreateFinishedProductReceiptDto
    {
        public string finishedProductReceiptId { get; set; }
        public string employeeId { get; set; }
        public DateTime timestamp { get; set; }
        public List<CreateFinishedProductReceiptEntryDto> entries { get; set; }
        public CreateFinishedProductReceiptDto(string finishedProductReceiptId, string employeeId, DateTime timestamp, List<CreateFinishedProductReceiptEntryDto> entries)
        {
            this.finishedProductReceiptId = finishedProductReceiptId;
            this.employeeId = employeeId;
            this.timestamp = timestamp;
            this.entries = entries;
        }
    }
}
