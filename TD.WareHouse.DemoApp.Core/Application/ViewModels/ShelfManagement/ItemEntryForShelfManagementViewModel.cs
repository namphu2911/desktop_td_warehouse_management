using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.ShelfManagement
{
    public class ItemEntryForShelfManagementViewModel
    {
        public string LotId { get; set; }
        public double? Quantity { get; set; }
        public string Unit { get; set; }
        public double? PacketSize { get; set; }
        public double? NumberOfPacket { get; set; }
        public string? LocationId { get; set; }
        public double? QuantityPerLocation { get; set; }
        public ItemEntryForShelfManagementViewModel(string lotId, double? quantity, string unit, double? packetSize, double? numberOfPacket, string? locationId, double? quantityPerLocation)
        {
            LotId = lotId;
            Quantity = quantity;
            Unit = unit;
            PacketSize = packetSize;
            NumberOfPacket = numberOfPacket;
            LocationId = locationId;
            QuantityPerLocation = quantityPerLocation;
        }
    }
}
