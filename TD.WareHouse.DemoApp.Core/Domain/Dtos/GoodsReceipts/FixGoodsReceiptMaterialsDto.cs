using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts
{
    public class FixGoodsReceiptMaterialsDto
    {
        public string oldGoodsReceiptLotId { get; set; }
        public string newGoodsReceiptLotId { get; set; }
        public List<ItemLotLocationsDto> itemLotLocations { get; set; }
        public double quantity { get; set; }
        public DateTime? productionDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public string note { get; set; }
        public FixGoodsReceiptMaterialsDto(string oldGoodsReceiptLotId, string newGoodsReceiptLotId, List<ItemLotLocationsDto> itemLotLocations, double quantity, DateTime? productionDate, DateTime? expirationDate, string note)
        {
            this.oldGoodsReceiptLotId = oldGoodsReceiptLotId;
            this.newGoodsReceiptLotId = newGoodsReceiptLotId;
            this.itemLotLocations = itemLotLocations;
            this.quantity = quantity;
            this.productionDate = productionDate;
            this.expirationDate = expirationDate;
            this.note = note;
        }
    }
}
