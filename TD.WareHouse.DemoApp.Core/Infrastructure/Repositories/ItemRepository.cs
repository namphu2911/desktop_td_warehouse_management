using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Infrastructure.Context;

namespace TD.WareHouse.DemoApp.Core.Infrastructure.Repositories
{
    public class ItemRepository : BaseRepository, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddList(IEnumerable<Item> items)
        {
            _context.Items.AddRange(items);
        }

        public void Clear()
        {
            _context.Items.RemoveRange(_context.Items);
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            var items = await _context.Items.ToListAsync();

            return items;
        }
    }
}
