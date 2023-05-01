using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;
using TD.WareHouse.DemoApp.Core.Infrastructure.Context;

namespace TD.WareHouse.DemoApp.Core.Infrastructure.Repositories
{
    public class GoodsReceiptRepository : BaseRepository, IGoodsReceiptRepository
    {
        public GoodsReceiptRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddList(IEnumerable<GoodsReceipt> goodsReceipts)
        {
            _context.GoodsReceipts.AddRange(goodsReceipts);
        }

        public void Clear()
        {
            _context.GoodsReceipts.RemoveRange(_context.GoodsReceipts);
        }

        public async Task<IEnumerable<GoodsReceipt>> GetAll()
        {
            var goodsReceipts = await _context.GoodsReceipts.ToListAsync();

            return goodsReceipts;
        }
    }
}
