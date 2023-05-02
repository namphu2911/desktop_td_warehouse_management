using ExcelDataReader;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class ExcelReader : IExcelReader
    {
        public IEnumerable<ExportRequest> ReadExportRequests(string filePath, DateTime date)
        {
            List<ExportRequest> requests = new();

            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            JumpToSheet("KE HOACH", reader);

            SkipRows(2, reader);

            while (reader.Read())
            {
                var value = reader.GetValue(0);
                if (value is not null)
                {
                    DateTime rowDate;
                    try
                    {
                        rowDate = reader.GetDateTime(0);
                    }
                    catch
                    {
                        rowDate = ParseDate(reader.GetString(0));
                    }

                    if (rowDate.Date == date.Date)
                    {
                        var itemId = reader.GetString(1);
                        if (!String.IsNullOrEmpty(itemId) && itemId != "\\")
                        {
                            var quantity = reader.GetDouble(3);

                            requests.Add(new ExportRequest(itemId, quantity));
                        }
                    }
                }
            }

            return requests;
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

