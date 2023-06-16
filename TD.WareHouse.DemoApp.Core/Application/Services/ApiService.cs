using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Location;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.LotAdjustment;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Warehouse;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        private const string serverUrl = "https://thaiduongwarehouse.azurewebsites.net/";
        private string? _token;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }
        public void SetToken(string token)
        {
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Items");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<ItemDto>>(responseBody);
            if (items is null)
            {
                return new List<ItemDto>();
            }

            return items;
        }
        public async Task<IEnumerable<WarehouseDto>> GetAllWarehousesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Warehouses");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<WarehouseDto>>(responseBody);
            if (items is null)
            {
                return new List<WarehouseDto>();
            }

            return items;
        }
        public async Task<IEnumerable<ItemLotDto>> GetAllItemLotsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/ItemLots");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<ItemLotDto>>(responseBody);
            if (items is null)
            {
                return new List<ItemLotDto>();
            }

            return items;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Departments");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<DepartmentDto>>(responseBody);
            if (items is null)
            {
                return new List<DepartmentDto>();
            }

            return items;
        }
        public async Task<IEnumerable<GoodsReceiptDto>> GetAllGoodsReceiptsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsReceipts");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<GoodsReceiptDto>>(responseBody);
            if (items is null)
            {
                return new List<GoodsReceiptDto>();
            }
            return items;
        }

        public async Task<IEnumerable<GoodsReceiptDto>> GetUnconfirmedGoodsReceiptsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsReceipts/goodsReceipts/false");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<GoodsReceiptDto>>(responseBody);
            if (items is null)
            {
                return new List<GoodsReceiptDto>();
            }
            return items;
        }

        public async Task<List<string>> GetAllGoodsIssuesReceiverAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsIssues/Receivers");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<List<string>>(responseBody);
            if (items is null)
            {
                return new List<string>();
            }
            return items;
        }
        public async Task<List<string>> GetAllGoodsReceiptsSupplierAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsReceipts/Suppliers");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<List<string>>(responseBody);
            if (items is null)
            {
                return new List<string>();
            }
            return items;
        }

        public async Task<List<string>> GetAllPurchaseOrderNumbersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsReceipts/PurchaseOrderNumbers");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<List<string>>(responseBody);
            if (items is null)
            {
                return new List<string>();
            }
            return items;
        }

        public async Task<IEnumerable<GoodsIssueDto>> GetUnconfirmedGoodsIssuesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsIssues/Unconfirmed");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<GoodsIssueDto>>(responseBody);
            if (items is null)
            {
                return new List<GoodsIssueDto>();
            }
            return items;
        }

        public async Task<IEnumerable<GoodsReceiptDto>> GetReceivedGoodsReceiptsAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsReceipts/TimeRange?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsReceiptDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<GoodsReceiptDto> GetReceivingGoodsReceiptsAsync(string goodsReceiptId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsReceipts/{goodsReceiptId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GoodsReceiptDto>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }
            return result;
        }
        public async Task CreateGoodsReceiptsAsync(CreateGoodsReceiptDto goodsReceipts)
        {
            var json = JsonConvert.SerializeObject(goodsReceipts);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/GoodsReceipts", content);

            response.EnsureSuccessStatusCode();
        }
        //public async Task FixUncompltedGoodsReceiptAsync(string goodsReceiptId, IEnumerable<FixUncompletedGoodsReceiptDto> fixDto)
        //{
        //    var json = JsonConvert.SerializeObject(fixDto);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/GoodsReceipts/{goodsReceiptId}", content);
        //    response.EnsureSuccessStatusCode();
        //}
        public async Task FixCompltedGoodsReceiptAsync(string goodsReceiptId, IEnumerable<FixCompletedGoodsReceiptDto> fixDto)
        {
            var json = JsonConvert.SerializeObject(fixDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/GoodsReceipts/Reconfirm/{goodsReceiptId}", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task ConfirmGoodsReceiptAsync(string goodsReceiptId)
        {
            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/GoodsReceipts/Confirm/{goodsReceiptId}", null);

            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteGoodsReceiptAsync(string goodsReceiptId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{serverUrl}/api/GoodsReceipts/{goodsReceiptId}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<GoodsIssueDto>> GetIssuedGoodsIssuesAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsIssues?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsIssueDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<GoodsIssueDto> GetIssuingGoodsIssuesAsync(string goodsIssueId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsIssues/{goodsIssueId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GoodsIssueDto>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task ConfirmGoodsIssueAsync(string goodsIssueId)
        {
            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/GoodsIssues/Confirm/{goodsIssueId}", null);

            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteGoodsIssueAsync(string goodsIssueId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{serverUrl}/api/goodsreceipts/{goodsIssueId}");

            response.EnsureSuccessStatusCode();
        }
        //HistoryImport
        public async Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsPOAsync(string purchaseOrderNumber)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryHistories/ByPO/Import?purchaseOrderNumber={purchaseOrderNumber}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsReceiptDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsSupplierAsync(DateTime startDate, DateTime endDate, string supplier)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryHistories/BySupplier/Import?supplier={supplier}&StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsReceiptDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryHistories/Import?StartTime={startDateString}&EndTime={endDateString}&itemClassId={warehouseId}&itemId={itemId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsReceiptDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        /////
        public async Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsPOAsync(string purchaseOrderNumber)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryHistories/ByPO/Export?purchaseOrderNumber={purchaseOrderNumber}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsIssueDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsReceiverAsync(DateTime startDate, DateTime endDate, string receiver)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryHistories/ByReceiver/Export?StartTime={startDateString}&EndTime={endDateString}&receiver={receiver}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsIssueDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryHistories/Export?StartTime={startDateString}&EndTime={endDateString}&itemClassId={warehouseId}&itemId={itemId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsIssueDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        /////
        //public async Task<IEnumerable<LotAdjustmentDto>> GetLotAdjustmentsAsync(DateTime startDate, DateTime endDate)
        //{
        //    string startDateString = startDate.ToString("yyyy-MM-dd");
        //    string endDateString = endDate.ToString("yyyy-MM-dd");

        //    HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/lotinconsistencies?StartTime={startDateString}&EndTime={endDateString}");

        //    response.EnsureSuccessStatusCode();
        //    string responseBody = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<IEnumerable<LotAdjustmentDto>>(responseBody);

        //    if (result is null)
        //    {
        //        throw new Exception();
        //    }

        //    return result;
        //}
        public async Task<IEnumerable<LotAdjustmentDto>> GetUnfixedLotAdjustmentsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/LotAdjustments/Unconfirmed");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<LotAdjustmentDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<IEnumerable<LotAdjustmentDto>> GetConfirmedLotAdjustmentsAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");
            
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/LotAdjustments?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<LotAdjustmentDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        
        public async Task FixLotAdjustmentAsync(string lotId)
        {
            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/LotAdjustments/Confirm/{lotId}", null);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteLotAdjustmentAsync(string lotId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{serverUrl}/api/LotAdjustments/{lotId}");

            response.EnsureSuccessStatusCode();
        }

        //issue
        public async Task CreateGoodsIssuesAsync(CreateGoodsIssueDto goodsIssues)
        {
            var json = JsonConvert.SerializeObject(goodsIssues);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/GoodsIssues", content);

            try
            {
                
            }
            catch (HttpRequestException ex)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ServerSideError>(responseBody);
                    if (error is not null)
                    {
                        switch (error.ErrorCode)
                        {
                            case "Domain.EntityDuplication":
                                throw new DuplicateEntityException();
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<ItemLotDto>> GetExpirationDateAlarmEntriesAsync(double timeLeft)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Warnings/ExpirationDate/{timeLeft}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ItemLotDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<IEnumerable<ItemLotDto>> GetQuantityAlarmEntriesAsync(string itemClassId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Warnings/MinimumStockLevel/{itemClassId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ItemLotDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<IEnumerable<InventoryLogEntryDto>> GetStockCardEntriesAsync(string itemId, DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryLogEntries/{itemId}?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<InventoryLogEntryDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<IEnumerable<InventoryLogEntryDto>> GetStockCardEntriesByTimeAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryLogEntries?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<InventoryLogEntryDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<IEnumerable<InventoryLogExtendedEntryDto>> GetExtendedStockCardEntriesAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");
         
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryLogEntries/extendedLogEntries?itemClassId={warehouseId}&itemId={itemId}&StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<InventoryLogExtendedEntryDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        //public async Task<InventoryLogExtendedEntryDto> GetStockCardEntriesByItemAsync(string itemId, string unit, DateTime startDate, DateTime endDate)
        //{
        //    string startDateString = startDate.ToString("yyyy-MM-dd");
        //    string endDateString = endDate.ToString("yyyy-MM-dd");
        //    HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryLogEntries/extendedLogEntry/{itemId}&{unit}?StartTime={startDateString}&EndTime={endDateString}");

        //    response.EnsureSuccessStatusCode();
        //    string responseBody = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<InventoryLogExtendedEntryDto>(responseBody);

        //    if (result is null)
        //    {
        //        throw new Exception();
        //    }

        //    return result;
        //}

        public async Task<IEnumerable<ItemLotDto>> GetItemShelfManagementEntriesAsync(string itemId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/ItemLots/ByItemId/{itemId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ItemLotDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<IEnumerable<ItemLotDto>> GetLocationShelfManagementEntriesAsync(string locationId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/ItemLots/ByLocation/{locationId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ItemLotDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<IEnumerable<ItemLotDto>> GetUnfixedItemLotsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/ItemLots/Isolated");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ItemLotDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task IssueIsolationItemLotsAsync(string itemLotId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{serverUrl}/api/ItemLots?itemLotId={itemLotId}");

            response.EnsureSuccessStatusCode();
        }
        public async Task ReceiveIsolationItemLotsAsync(string LotId)
        {
            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/ItemLots/{LotId}", null);

            response.EnsureSuccessStatusCode();
        }


    }
}
