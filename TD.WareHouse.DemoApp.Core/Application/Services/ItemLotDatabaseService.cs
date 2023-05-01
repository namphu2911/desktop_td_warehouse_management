using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class ItemLotDatabaseService : IItemLotDatabaseService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ItemLotDatabaseService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemLot>> FilterItemLotsByName(string itemLotName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemLot>> GetAllItemLots()
        {
            using var scope = _scopeFactory.CreateScope();
            var itemLotRepository = CreateItemLotRepository(scope);

            var itemLots = itemLotRepository.GetAll();

            return itemLots;
        }

        public Task Insert(IEnumerable<ItemLot> itemLots)
        {
            throw new NotImplementedException();
        }

        public async Task OverrideAllItemLots(IEnumerable<ItemLot> itemLots)
        {
            using var scope = _scopeFactory.CreateScope();
            var itemLotRepository = CreateItemLotRepository(scope);

            itemLotRepository.Clear();
            itemLotRepository.AddList(itemLots);

            await itemLotRepository.UnitOfWork.SaveChangesAsync();
        }

        private IItemLotRepository CreateItemLotRepository(IServiceScope scope)
        {
            var ItemLotRepository = scope.ServiceProvider.GetService<IItemLotRepository>();
            if (ItemLotRepository is null)
            {
                throw new InvalidOperationException();
            }
            return ItemLotRepository;
        }
    }
}
