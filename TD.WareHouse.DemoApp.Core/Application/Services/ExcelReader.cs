using ExcelDataReader;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class ExcelReader : IExcelReader
    {
        public GoodsIssueDb ReadExportRequests(string filePath, string sheetName, DateTime date)
        {
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            if (sheetName is "Phieu XK NVL")
            {
                JumpToSheet(sheetName, reader);
                SkipRows(4, reader); reader.Read();
            }
            else if (sheetName is "Phieu XK TP")
            {
                JumpToSheet(sheetName, reader);
                SkipRows(3, reader); reader.Read();
            }
            var GoodsIssueId = reader.GetString(6);

            reader.Read(); //doc Row hien tai va tu xuong dong
            var Receiver = reader.GetString(1);
            GoodsIssueDb request = new(GoodsIssueId, DateTime.Now, "", Receiver, new List<GoodsIssueEntry>());

            SkipRows(2, reader);
            while (reader.Read())
            {
                var value = reader.GetValue(0);
                if (value is not null)
                {
                    var itemId = reader.GetString(1);
                    var itemName = reader.GetString(2);
                    var unit = reader.GetString(3);
                    var requestedQuantity = reader.GetDouble(4);
                    request.Entries.Add(new GoodsIssueEntry(itemId, itemName, unit, requestedQuantity));
                }
            }
            return request;

        }

        private static void JumpToSheet(string sheetName, IExcelDataReader reader)
        {
            reader.Reset();
            while (reader.Name != sheetName)
            {
                if (!reader.NextResult())
                {
                    throw new Exception($"No sheet named '{sheetName}' found in the excel file.");
                }
            }
        }

        private static void SkipRows(int numberOfRow, IExcelDataReader reader)
        {
            for (int i = 0; i < numberOfRow; i++)
            {
                reader.Read();
            }
        }

        private static DateTime ParseDate(string dateString)
        {
            while (dateString.EndsWith("."))
            {
                dateString = dateString.Remove(dateString.Count() - 1);
            }

            return DateTime.Parse(dateString);
        }
    }
}

