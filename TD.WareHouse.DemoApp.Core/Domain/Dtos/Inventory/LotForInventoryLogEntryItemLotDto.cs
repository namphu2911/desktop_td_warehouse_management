using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory
{
    public class LotForInventoryLogEntryItemLotDto
    {
        public string LotId { get; set; }
        public double Quantity { get; set; }
        public double? NumOfPackets { get; set; }
        public LotForInventoryLogEntryItemLotDto(string lotId, double quantity, double? numOfPackets)
        {
            LotId = lotId;
            Quantity = quantity;
            NumOfPackets = numOfPackets;
        }
    }
}
