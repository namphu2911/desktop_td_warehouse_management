using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues
{
    public class ExportRequest
    {
        public string GoodsIssueId { get; set; }
        public string Receiver { get; set; }
        public DateTime Timestamp { get; set; }
        public List<ExportRequestEntry> ExportRequestEntries { get; set; } = new();

        public ExportRequest(string goodsIssueId, string receiver, DateTime timestamp, List<ExportRequestEntry> exportRequestEntries)
        {
            GoodsIssueId = goodsIssueId;
            Receiver = receiver;
            Timestamp = timestamp;
            ExportRequestEntries = exportRequestEntries;
        }
    }
}
