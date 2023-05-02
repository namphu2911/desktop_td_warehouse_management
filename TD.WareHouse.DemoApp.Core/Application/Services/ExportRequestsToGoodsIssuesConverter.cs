using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class ExportRequestsToGoodsIssuesConverter : IExportRequestsToGoodsIssuesConverter
    {
        private readonly IApiService _apiService;

        public ExportRequestsToGoodsIssuesConverter(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<GoodsIssue>> ConvertAsync(IEnumerable<ExportRequest> requests)
        {
            var goodsIssues = new List<GoodsIssue>();

            //foreach (var request in requests)
            //{
            //    var parts = await _apiService.GetItemPartsAsync(request.ItemId);

            //    foreach (var part in parts)
            //    {
            //        var manager = part.Part.Manager;
            //        if (manager is null)
            //        {
            //            throw new Exception();
            //        }

            //        var goodsIssue = goodsIssues.FirstOrDefault(
            //            g => g.Employee.EmployeeId == manager.EmployeeId);

            //        if (goodsIssue is null)
            //        {
            //            var today = DateTime.Now.Date;

            //            goodsIssue = new GoodsIssue(
            //                $"{today.ToString("yyyy-MM-dd")} {manager.EmployeeId}",
            //                DateTime.Now,
            //                new Employee(manager.EmployeeId,
            //                             manager.FirstName,
            //                             manager.LastName,
            //                             manager.DateOfBirth),
            //                new List<GoodsIssueEntry>());

            //            goodsIssues.Add(goodsIssue);
            //        }

            //        var goodsIssueEntry = goodsIssue.Entries.FirstOrDefault(
            //            e => e.Item.Id == part.Part.ItemId);
            //        if (goodsIssueEntry is null)
            //        {
            //            goodsIssueEntry = new GoodsIssueEntry(
            //                new Item(part.Part.ItemId, part.Part.Name, part.Part.Unit),
            //                part.Quantity * request.Quantity);
            //            goodsIssue.Entries.Add(goodsIssueEntry);
            //        }
            //        else
            //        {
            //            goodsIssueEntry.PlannedQuantity += part.Quantity * request.Quantity;
            //        }
            //    }
            //}
            return goodsIssues;
        }
    }
    
}
