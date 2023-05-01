using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IItemLotDatabaseService
    {
        Task<IEnumerable<ItemLot>> FilterItemLotsByName(string itemLotName);
        Task<IEnumerable<ItemLot>> GetAllItemLots();
        Task Clear();
        Task Insert(IEnumerable<ItemLot> itemLots);
        Task OverrideAllItemLots(IEnumerable<ItemLot> itemLots);
    }
}
