using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Items
{
    public class CreateItemLotDto
    {
        public string itemId { get; set; }
        public string unit { get; set; }
        public string lotId { get; set; }
        public double quantity { get; set; }
        public DateTime? productionDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public CreateItemLotDto(string itemId, string unit, string lotId, double quantity, DateTime? productionDate, DateTime? expirationDate)
        {
            this.itemId = itemId;
            this.unit = unit;
            this.lotId = lotId;
            this.quantity = quantity;
            this.productionDate = productionDate;
            this.expirationDate = expirationDate;
        }
    }
}
