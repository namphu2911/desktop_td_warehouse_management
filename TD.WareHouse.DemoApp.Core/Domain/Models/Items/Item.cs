using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.Items
{
    public class Item
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Item() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Item(string itemid, string itemname, string unit)
        {
            ItemId = itemid;
            ItemName = itemname;
            Unit = unit;
        }
    }
}
