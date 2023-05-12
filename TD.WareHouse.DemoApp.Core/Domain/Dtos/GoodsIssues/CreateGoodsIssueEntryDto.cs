using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class CreateGoodsIssueEntryDto
    {
        public string itemId { get; set; }
        public string unit { get; set; }
        public double? requestedSublotSize { get; set; }
        public double requestedQuantity { get; set; }
        public CreateGoodsIssueEntryDto(string itemId, string unit, double? requestedSublotSize, double requestedQuantity)
        {
            this.itemId = itemId;
            this.unit = unit;
            this.requestedSublotSize = requestedSublotSize;
            this.requestedQuantity = requestedQuantity;
        }
    }
}
