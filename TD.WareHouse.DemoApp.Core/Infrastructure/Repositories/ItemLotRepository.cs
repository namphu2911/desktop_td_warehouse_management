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
    public class ItemLotRepository : BaseRepository, IItemLotRepository
    {
        public ItemLotRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddList(IEnumerable<ItemLot> itemLots)
        {
            _context.ItemLots.AddRange(itemLots);
        }

        public void Clear()
        {
            _context.ItemLots.RemoveRange(_context.ItemLots);
        }

        public async Task<IEnumerable<ItemLot>> GetAll()
        {
            var itemLots = await _context.ItemLots.ToListAsync();

            return itemLots;
        }
    }
}
