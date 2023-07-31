using ExcelDataReader;
using System.IO;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class ExcelReader : IExcelReader
    {
        public GoodsIssueDb ReadGoodsIssueInternalExportRequests(string filePath, string sheetName, DateTime date)
        {
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            JumpToSheet(sheetName, reader);
            SkipRows(4, reader); reader.Read();
            var GoodsIssueId = reader.GetString(6);

            reader.Read(); //doc Row hien tai va tu xuong dong
            var Receiver = reader.GetString(1);
            GoodsIssueDb request = new(GoodsIssueId, DateTime.Now, "NV01", Receiver, new List<GoodsIssueEntry>());

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

        public FinishedProductIssueDb ReadGoodsIssueExternalRequests(string filePath, string sheetName, DateTime date)
        {
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            JumpToSheet(sheetName, reader);
            SkipRows(3, reader); reader.Read();
            var GoodsIssueId = reader.GetString(6);

            reader.Read(); //doc Row hien tai va tu xuong dong
            var Receiver = reader.GetString(1);
            FinishedProductIssueDb request = new(GoodsIssueId, DateTime.Now, "NV01", Receiver, new List<FinishedProductIssueEntry>());

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
                    var purchaseOrderNumber = reader.GetString(5);
                    var note = reader.GetString(6);
                    request.Entries.Add(new FinishedProductIssueEntry(itemId, itemName, unit, requestedQuantity, purchaseOrderNumber, note));
                }
            }
            return request;

        }

        public FinishedProductReceiptDb ReadReceiptExportRequests(string filePath, string sheetName, DateTime date)
        {
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            JumpToSheet(sheetName, reader);
            reader.Read();
            var GoodsReceiptId = reader.GetString(17);

            //reader.Read(); //doc Row hien tai va tu xuong dong
            FinishedProductReceiptDb request = new(GoodsReceiptId, "NV01", DateTime.Now, new List<FinishedProductReceiptEntry>());

            SkipRows(7, reader);
            while (reader.Read())
            {
                var value = reader.GetValue(1);
                if (value is null) break;
                else if (value is not null)
                {
                    var goodsReceiptLotId = reader.GetString(2);
                    var itemId = reader.GetString(4);
                    var itemName = reader.GetString(10);
                    var unit = reader.GetString(16);
                    var quantity = reader.GetDouble(18);
                    var purchaseOrderNumber = reader.GetString(19);
                    var note = reader.GetString(23);
                    request.Entries.Add(new FinishedProductReceiptEntry(purchaseOrderNumber, itemId, itemName, unit, quantity, note));
                }
            }
            return request;

        }

        public CreateListItemDto ReadItemExportRequests(string filePath, string sheetName, DateTime date)
        {
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            JumpToSheet(sheetName, reader);
            reader.Read();
            List<CreateItemDto> dtos = new List<CreateItemDto>();
            CreateListItemDto request = new(dtos);

            while (reader.Read())
            {
                var value = reader.GetValue(0);
                if (value is not null)
                {   
                    var itemClassId = reader.GetString(0);
                    var itemId = Convert.ToString(reader.GetValue(1));
                    var itemName = reader.GetString(2);
                    var unit = reader.GetString(3);
                    var price = Convert.ToDouble(reader.GetValue(4));
                    var minimumStockLevel = Convert.ToDecimal(reader.GetValue(5));
                    var packetSize = Convert.ToDouble(reader.GetValue(6));
                    var packetUnit = Convert.ToString(reader.GetValue(7));
                    request.items.Add(new CreateItemDto(itemId, itemName, price, minimumStockLevel, itemClassId, unit, packetSize, packetUnit));
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

