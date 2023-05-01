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
        public string Unit { get; set; }
        public string GoodsIssueId { get; set; }
        public double RequestedQuantity { get; set; }
        public double? RequestedSublotSize { get; set; }
        public List<GoodsIssueLotDto> Lots { get; set; }
        public GoodsIssueEntryDto(ItemDto item, string unit, string goodsIssueId, double requestedQuantity, double? requestedSublotSize, List<GoodsIssueLotDto> lots)
        {
            Item = item;
            Unit = unit;
            GoodsIssueId = goodsIssueId;
            RequestedQuantity = requestedQuantity;
            RequestedSublotSize = requestedSublotSize;
            Lots = lots;
        }
    }
}
