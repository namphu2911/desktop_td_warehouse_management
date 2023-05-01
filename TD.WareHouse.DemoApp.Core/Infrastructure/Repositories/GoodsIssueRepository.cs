using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Infrastructure.Context;

namespace TD.WareHouse.DemoApp.Core.Infrastructure.Repositories
{
    public class GoodsIssueRepository : BaseRepository, IGoodsIssueRepository
    {
        public GoodsIssueRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddList(IEnumerable<GoodsIssue> goodsIssues)
        {
            _context.GoodsIssues.AddRange(goodsIssues);
        }

        public void Clear()
        {
            _context.GoodsIssues.RemoveRange(_context.GoodsIssues);
        }

        public async Task<IEnumerable<GoodsIssue>> GetAll()
        {
            var goodsIssues = await _context.GoodsIssues.ToListAsync();

            return goodsIssues;
        }
    }
}
