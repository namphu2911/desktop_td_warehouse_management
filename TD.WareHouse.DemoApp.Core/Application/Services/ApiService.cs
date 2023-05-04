﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Warehouses");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<LocationDto>>(responseBody);
            if (items is null)
            {
                return new List<LocationDto>();
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


        public async Task<QueryResult<GoodsReceiptDto>> GetReceivedGoodsReceiptsAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsReceipts/TimeRange");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<QueryResult<GoodsReceiptDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<QueryResult<GoodsReceiptDto>> GetReceivingGoodsReceiptsAsync(string goodsReceiptId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsReceipts/{goodsReceiptId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<QueryResult<GoodsReceiptDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }
            return result;
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

        public async Task<QueryResult<GoodsIssueDto>> GetIssuedGoodsIssuesAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}api/GoodsReceipts/TimeRange?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<QueryResult<GoodsIssueDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<QueryResult<GoodsIssueDto>> GetIssuingGoodsIssuesAsync(string goodsIssueId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<QueryResult<GoodsIssueDto>>(responseBody);

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

        public async Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsAsync(string warehouseId, string itemId, string itemName, DateTime startDate, DateTime endDate, string supplier, string purchaseOrderNumber)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/lotinconsistencies?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsReceiptDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsAsync(string warehouseId, string itemId, string itemName, DateTime startDate, DateTime endDate, string receiver, string purchaseOrderNumber)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/lotinconsistencies?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsIssueDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
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
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/LotAdjustments");

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
        public async Task CreateGoodsIssuesAsync(IEnumerable<CreateGoodsIssueDto> goodsIssues)
        {
            var json = JsonConvert.SerializeObject(goodsIssues);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/GoodsIssues", content);

            try
            {
                response.EnsureSuccessStatusCode();
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

        public async Task<IEnumerable<InventoryLogEntryDto>> GetStockCardEntriesAsync(string warehouseId, string itemId, string itemName, DateTime startDate, DateTime endDate, string purchaseOrderNumber)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/lotinconsistencies?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<InventoryLogEntryDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

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
