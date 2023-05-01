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
    public class ItemDatabaseService : IItemDatabaseService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ItemDatabaseService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> FilterItemsByName(string itemName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetAllItems()
        {
            using var scope = _scopeFactory.CreateScope();
            var itemRepository = CreateItemRepository(scope);

            var items = itemRepository.GetAll();

            return items;
        }

        public Task Insert(IEnumerable<Item> items)
        {
            throw new NotImplementedException();
        }

        public async Task OverrideAllItems(IEnumerable<Item> items)
        {
            using var scope = _scopeFactory.CreateScope();
            var itemRepository = CreateItemRepository(scope);

            itemRepository.Clear();
            itemRepository.AddList(items);

            await itemRepository.UnitOfWork.SaveChangesAsync();
        }

        private IItemRepository CreateItemRepository(IServiceScope scope)
        {
            var itemRepository = scope.ServiceProvider.GetService<IItemRepository>();
            if (itemRepository is null)
            {
                throw new InvalidOperationException();
            }

            return itemRepository;
        }
    }
}