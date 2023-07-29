using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Items
{
    public class CreateListItemDto
    {
        public List<CreateItemDto> items { get; set; }
        public CreateListItemDto(List<CreateItemDto> items)
        {
            this.items = items;
        }
    }
}
