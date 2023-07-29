using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IExcelReader
    {
        GoodsIssueDb ReadGoodsIssueInternalExportRequests(string filePath, string sheetName, DateTime date);
        FinishedProductIssueDb ReadGoodsIssueExternalRequests(string filePath, string sheetName, DateTime date);
        FinishedProductReceiptDb ReadReceiptExportRequests(string filePath, string sheetName, DateTime date);
        CreateListItemDto ReadItemExportRequests(string filePath, string sheetName, DateTime date);
    }
}
