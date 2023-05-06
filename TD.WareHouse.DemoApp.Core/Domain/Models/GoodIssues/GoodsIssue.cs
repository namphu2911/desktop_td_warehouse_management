using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Employees;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues
{
    public class GoodsIssue
    {
        public string GoodsIssueId { get; set; }
        public string Receiver { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private GoodsIssue() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public GoodsIssue(string goodsIssueId, string receiver)
        {
            GoodsIssueId = goodsIssueId;
            Receiver = receiver;
        }
    }
}
