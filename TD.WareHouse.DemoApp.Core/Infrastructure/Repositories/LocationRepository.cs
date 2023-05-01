using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Infrastructure.Context;

namespace TD.WareHouse.DemoApp.Core.Infrastructure.Repositories
{
    public class LocationRepository : BaseRepository, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddList(IEnumerable<Location> locations)
        {
            _context.Locations.AddRange(locations);
        }

        public void Clear()
        {
            _context.Locations.RemoveRange(_context.Locations);
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            var locations = await _context.Locations.ToListAsync();

            return locations;
        }
    }
}
