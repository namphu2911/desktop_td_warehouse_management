using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.Items
{
    public class ItemLot
    {
        public string LotId { get; set; }
        public Item Item { get; set; }
        public bool IsIsolatedd { get; set; }
        public double Quantity { get; set; }
        public double SublotSize { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public Location Location { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ItemLot() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ItemLot(string lotId, Item item, bool isIsolatedd, double quantity, double sublotSize, string purchaseOrderNumber, Location location, DateTime productionDate, DateTime expirationDate)
        {
            LotId = lotId;
            Item = item;
            IsIsolatedd = isIsolatedd;
            Quantity = quantity;
            SublotSize = sublotSize;
            PurchaseOrderNumber = purchaseOrderNumber;
            Location = location;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
        }
    }
}
