using Microsoft.Extensions.DependencyInjection;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class LocationDatabaseService : ILocationDatabaseService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public LocationDatabaseService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Location>> FilterLocationsByName(string locationName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Location>> GetAllLocations()
        {
            using var scope = _scopeFactory.CreateScope();
            var locationRepository = CreateLocationRepository(scope);

            var locations = locationRepository.GetAll();

            return locations;
        }

        public Task Insert(IEnumerable<Location> locations)
        {
            throw new NotImplementedException();
        }

        public async Task OverrideAllLocations(IEnumerable<Location> locations)
        {
            using var scope = _scopeFactory.CreateScope();
            var locationRepository = CreateLocationRepository(scope);

            locationRepository.Clear();
            locationRepository.AddList(locations);

            await locationRepository.UnitOfWork.SaveChangesAsync();
        }

        private ILocationRepository CreateLocationRepository(IServiceScope scope)
        {
            var locationRepository = scope.ServiceProvider.GetService<ILocationRepository>();
            if (locationRepository is null)
            {
                throw new InvalidOperationException();
            }

            return locationRepository;
        }
    }
}
