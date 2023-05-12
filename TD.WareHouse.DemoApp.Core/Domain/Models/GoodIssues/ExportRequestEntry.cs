using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues
{
    public class ExportRequestEntry
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double RequestedQuantity { get; set; }
        public ExportRequestEntry(string itemId, string itemName, string unit, double requestedQuantity)
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            RequestedQuantity = requestedQuantity;
        }
    }
}
