using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using BorderStyle = NPOI.SS.UserModel.BorderStyle;
using HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class ExcelExporter : IExcelExporter
    {
        private const string reportTemplatePath = @"./BBKK.xlsx";
        private const int reportTemplateStartRow = 8;

        public void ExportReport(string filePath, IEnumerable<ConfirmedLotAdjustmentViewModel> filters)
        {
            XSSFWorkbook workBook;
            using (FileStream file = new FileStream(reportTemplatePath, FileMode.Open, FileAccess.Read))
            {
                workBook = new XSSFWorkbook(file);
            }

            ISheet sheet = workBook.GetSheet("Sheet1");
            var orderedFilter = filters.OrderBy(x => x.LotId).ToList();

            var currentRowIndex = reportTemplateStartRow;
            var currentRowCount = 1;

            XSSFCellStyle defaultStyle = (XSSFCellStyle)workBook.CreateCellStyle();
            defaultStyle.WrapText = true;
            defaultStyle.Alignment = HorizontalAlignment.Left;
            defaultStyle.VerticalAlignment = VerticalAlignment.Bottom;
            defaultStyle.BorderBottom = BorderStyle.Thin;
            defaultStyle.BorderTop = BorderStyle.Thin;
            defaultStyle.BorderLeft = BorderStyle.Thin;
            defaultStyle.BorderRight = BorderStyle.Thin;

            sheet.GetRow(1).GetCell(5).SetCellValue(DateTime.Now.Month);
            sheet.GetRow(4).GetCell(3).SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"));
            foreach (var filter in orderedFilter)
            {
                var currentRow = sheet.GetRow(currentRowIndex);
                if (currentRow == null)
                {
                    sheet.CreateRow(currentRowIndex);
                    currentRow = sheet.GetRow(currentRowIndex);
                    var cell0 = currentRow.CreateCell(0); cell0.CellStyle = defaultStyle;
                    var cell1 = currentRow.CreateCell(1); cell1.CellStyle = defaultStyle;
                    var cell2 = currentRow.CreateCell(2); cell2.CellStyle = defaultStyle;
                    var cell3 = currentRow.CreateCell(3); cell3.CellStyle = defaultStyle;
                }
                currentRow.GetCell(0).SetCellValue(currentRowCount);
                currentRow.GetCell(2).SetCellValue(filter.ItemId);
                currentRow.GetCell(3).SetCellValue(filter.ItemName);
                currentRow.GetCell(4).SetCellValue(filter.Unit);
                currentRow.GetCell(5).SetCellValue(filter.LotId);
                currentRow.GetCell(6).SetCellValue(filter.BeforeQuantity);
                currentRow.GetCell(7).SetCellValue(filter.AfterQuantity);
                currentRowCount++;
                currentRowIndex++;
            }

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                workBook.Write(fs);
            }
        }
    }
}
