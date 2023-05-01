using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class CreateGoodsIssueEntryDto
    {
        public string ItemId { get; set; }
        public double RequestedQuantity { get; set; }
        public CreateGoodsIssueEntryDto(string itemId, double requestedQuantity)
        {
            ItemId = itemId;
            RequestedQuantity = requestedQuantity;
        }
    }
}
