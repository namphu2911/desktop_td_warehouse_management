using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Location;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Items
{
    public class ItemLotDto
    {
        public string LotId { get; set; }
        public ItemDto Item { get; set; }
        public string Unit { get; set; }
        public bool IsIsolatedd { get; set; }
        public double Quantity { get; set; }
        public double SublotSize { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public LocationDto Location { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<InventoryLogEntryDto> InventoryLogEntries { get; set; }
        public ItemLotDto(string lotId, ItemDto item, string unit, bool isIsolatedd, double quantity, double sublotSize, string purchaseOrderNumber, LocationDto location, DateTime productionDate, DateTime expirationDate, List<InventoryLogEntryDto> inventoryLogEntries)
        {
            LotId = lotId;
            Item = item;
            Unit = unit;
            IsIsolatedd = isIsolatedd;
            Quantity = quantity;
            SublotSize = sublotSize;
            PurchaseOrderNumber = purchaseOrderNumber;
            Location = location;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
            InventoryLogEntries = inventoryLogEntries;
        }
    }
}
