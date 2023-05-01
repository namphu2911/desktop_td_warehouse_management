using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IGoodIssueDatabaseService
    {
        Task<IEnumerable<GoodsIssue>> FilterGoodsIssuesByName(string goodsIssueName);
        Task<IEnumerable<GoodsIssue>> GetAllGoodsIssues();
        Task Clear();
        Task Insert(IEnumerable<GoodsIssue> goodsIssues);
        Task OverrideAllGoodsIssues(IEnumerable<GoodsIssue> goodsIssues);
    }
}
