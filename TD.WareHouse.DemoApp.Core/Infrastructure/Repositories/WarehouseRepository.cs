using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Warehouses;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Infrastructure.Context;

namespace TD.WareHouse.DemoApp.Core.Infrastructure.Repositories
{
    public class WarehouseRepository : BaseRepository, IWarehouseRepository
    {
        public WarehouseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddList(IEnumerable<Warehouse> warehouses)
        {
            _context.Warehouses.AddRange(warehouses);
        }

        public void Clear()
        {
            _context.Warehouses.RemoveRange(_context.Warehouses);
        }

        public async Task<IEnumerable<Warehouse>> GetAll()
        {
            var warehouses = await _context.Warehouses.ToListAsync();

            return warehouses;
        }
    }
}

