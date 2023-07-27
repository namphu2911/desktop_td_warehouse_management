using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class GoodsIssueEntryDto
    {
        public ItemDto Item { get; set; }
        public double RequestedQuantity { get; set; }
        public List<GoodsIssueLotDto> Lots { get; set; }
        public GoodsIssueEntryDto(ItemDto item, double requestedQuantity, List<GoodsIssueLotDto> lots)
        {
            Item = item;
            RequestedQuantity = requestedQuantity;
            Lots = lots;
        }
    }
}
