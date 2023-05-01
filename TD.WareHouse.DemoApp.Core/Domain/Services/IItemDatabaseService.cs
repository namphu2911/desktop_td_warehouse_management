using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IItemDatabaseService
    {
        Task<IEnumerable<Item>> FilterItemsByName(string itemName);
        Task<IEnumerable<Item>> GetAllItems();
        Task Clear();
        Task Insert(IEnumerable<Item> items);
        Task OverrideAllItems(IEnumerable<Item> items);
    }
}
