using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Items
{
    public class CreateListItemLotDto
    {
        public List<CreateItemLotDto> itemLots { get; set; }
        public CreateListItemLotDto(List<CreateItemLotDto> itemLots)
        {
            this.itemLots = itemLots;
        }
    }
}
