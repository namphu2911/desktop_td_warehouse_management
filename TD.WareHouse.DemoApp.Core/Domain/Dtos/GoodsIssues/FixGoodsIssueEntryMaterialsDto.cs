using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class FixGoodsIssueEntryMaterialsDto
    {
        public string itemId { get; set; }
        public string unit { get; set; }
        public double requestedQuantity { get; set; }
        public FixGoodsIssueEntryMaterialsDto(string itemId, string unit, double requestedQuantity)
        {
            this.itemId = itemId;
            this.unit = unit;
            this.requestedQuantity = requestedQuantity;
        }
    }
}
