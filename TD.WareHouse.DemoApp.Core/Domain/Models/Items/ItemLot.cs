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
        public string Unit { get; set; }
        public bool IsIsolatedd { get; set; }
        public double Quantity { get; set; }
        public double SublotSize { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public Location Location { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public ItemLot(string lotId, Item item, string unit, bool isIsolatedd, double quantity, double sublotSize, string purchaseOrderNumber, Location location, DateTime productionDate, DateTime expirationDate)
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
        }
    }
}
