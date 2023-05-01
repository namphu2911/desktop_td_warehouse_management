using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class GoodIssueDatabaseService : IGoodIssueDatabaseService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public GoodIssueDatabaseService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GoodsIssue>> FilterGoodsIssuesByName(string goodsIssueName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GoodsIssue>> GetAllGoodsIssues()
        {
            using var scope = _scopeFactory.CreateScope();
            var goodsIssueRepository = CreateGoodsIssueRepository(scope);

            var goodsIssues = goodsIssueRepository.GetAll();

            return goodsIssues;
        }

        public Task Insert(IEnumerable<GoodsIssue> goodsIssues)
        {
            throw new NotImplementedException();
        }

        public async Task OverrideAllGoodsIssues(IEnumerable<GoodsIssue> goodsIssues)
        {
            using var scope = _scopeFactory.CreateScope();
            var goodsIssueRepository = CreateGoodsIssueRepository(scope);

            goodsIssueRepository.Clear();
            goodsIssueRepository.AddList(goodsIssues);

            await goodsIssueRepository.UnitOfWork.SaveChangesAsync();
        }

        private IGoodsIssueRepository CreateGoodsIssueRepository(IServiceScope scope)
        {
            var goodsIssueRepository = scope.ServiceProvider.GetService<IGoodsIssueRepository>();
            if (goodsIssueRepository is null)
            {
                throw new InvalidOperationException();
            }

            return goodsIssueRepository;
        }
    }
}
