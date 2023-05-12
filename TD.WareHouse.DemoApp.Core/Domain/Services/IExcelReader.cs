using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IExcelReader
    {
        GoodsIssueDb ReadExportRequests(string filePath, string sheetName, DateTime date);
    }
}
