using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory
{
    public class ExtendedStockCardDto
    {
        public IEnumerable<InventoryLogExtendedEntryDto> Results { get; set; }
        public int TotalItems { get; set; }
        public ExtendedStockCardDto(List<InventoryLogExtendedEntryDto> results, int totalItems)
        {
            Results = results;
            TotalItems = totalItems;
        }
    } 
}
