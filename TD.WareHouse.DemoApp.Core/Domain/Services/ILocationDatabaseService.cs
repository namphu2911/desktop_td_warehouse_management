using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface ILocationDatabaseService
    {
        Task<IEnumerable<Location>> FilterLocationsByName(string locationName);
        Task<IEnumerable<Location>> GetAllLocations();
        Task Clear();
        Task Insert(IEnumerable<Location> locations);
        Task OverrideAllLocations(IEnumerable<Location> locations);
    }
}
