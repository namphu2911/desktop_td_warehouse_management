using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class GoodReceiptDatabaseService : IGoodReceiptDatabaseService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public GoodReceiptDatabaseService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GoodsReceipt>> FilterGoodsReceiptsByName(string goodReceiptName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GoodsReceipt>> GetAllGoodsReceipts()
        {
            using var scope = _scopeFactory.CreateScope();
            var goodReceiptRepository = CreateGoodsReceiptRepository(scope);

            var goodReceipts = goodReceiptRepository.GetAll();

            return goodReceipts;
        }

        public Task Insert(IEnumerable<GoodsReceipt> goodReceipts)
        {
            throw new NotImplementedException();
        }

        public async Task OverrideAllGoodsReceipts(IEnumerable<GoodsReceipt> goodReceipts)
        {
            using var scope = _scopeFactory.CreateScope();
            var goodReceiptRepository = CreateGoodsReceiptRepository(scope);

            goodReceiptRepository.Clear();
            goodReceiptRepository.AddList(goodReceipts);

            await goodReceiptRepository.UnitOfWork.SaveChangesAsync();
        }

        private IGoodsReceiptRepository CreateGoodsReceiptRepository(IServiceScope scope)
        {
            var goodReceiptRepository = scope.ServiceProvider.GetService<IGoodsReceiptRepository>();
            if (goodReceiptRepository is null)
            {
                throw new InvalidOperationException();
            }

            return goodReceiptRepository;
        }
    }
}
