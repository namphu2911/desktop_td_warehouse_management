using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Items
{
    public class ItemClassDto
    {
        string ItemClassId { get; set; }
        public ItemClassDto(string itemClassId)
        {
            ItemClassId = itemClassId;
        }   
    }
}
