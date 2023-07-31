using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory;

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IExcelExporter
    {
        void ExportReport(string filePath, IEnumerable<ConfirmedLotAdjustmentViewModel> filters);
    }
}
