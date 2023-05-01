using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues
{
    public class ExportRequest
    {
        public string ItemId { get; set; }
        public double Quantity { get; set; }

        public ExportRequest(string itemId, double quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
