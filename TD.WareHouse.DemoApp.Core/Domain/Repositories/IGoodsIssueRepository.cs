using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;

namespace TD.WareHouse.DemoApp.Core.Domain.Repositories;
public interface IGoodsIssueRepository : IRepository
{
    void AddList(IEnumerable<GoodsIssue> goodsIssue);
    Task<IEnumerable<GoodsIssue>> GetAll();
    void Clear();
}
