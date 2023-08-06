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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees;
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

        //MiscellaneousData
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

        public async Task CreateItem(CreateItemDto item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Items/item", content);

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
                        switch (error.Code)
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
        public async Task CreateItemFromExcel(CreateListItemDto item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Items", content);

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
                        switch (error.Code)
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

        public async Task FixItemAsync(FixItemDto fixDto)
        {
            var json = JsonConvert.SerializeObject(fixDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/Items", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Employees");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<EmployeeDto>>(responseBody);
            if (items is null)
            {
                return new List<EmployeeDto>();
            }

            return items;
        }

        public async Task CreateEmployee(CreateEmployeeDto employee)
        {
            var json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Employees", content);

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
                        switch (error.Code)
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

        public async Task CreateLocation(CreateLocationDto location)
        {
            var json = JsonConvert.SerializeObject(location);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Warehouses", content);

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
                        switch (error.Code)
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

        public async Task CreateDepartment(CreateDepartmentDto department)
        {
            var json = JsonConvert.SerializeObject(department);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Departments", content);

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
                        switch (error.Code)
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

        //OtherStore
        ///Receipt
        public async Task<IEnumerable<GoodsReceiptDto>> GetUncompletedGoodsReceiptMaterialsAsync()
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

        public async Task<IEnumerable<GoodsReceiptDto>> GetCompletedGoodsReceiptMaterialsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsReceipts/goodsReceipts/true");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<GoodsReceiptDto>>(responseBody);
            if (items is null)
            {
                return new List<GoodsReceiptDto>();
            }
            return items;
        }

        public async Task<List<string>> GetAllFinishedProductReceiptIdAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductReceipts/Ids");

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

        ///Issue
        public async Task<List<string>> GetUnfinishedGoodsIssuesIdAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsIssues/Ids?isExported=false");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<List<string>>(responseBody);
            if (items is null)
            {
                return new List<string>();
            }
            return items;
        }

        public async Task<List<string>> GetFinishedGoodsIssuesIdAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductIssues/Ids");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<List<string>>(responseBody);
            if (items is null)
            {
                return new List<string>();
            }
            return items;
        }

        public async Task<List<string>> GetFinishedGoodsIssuesReceiverAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductIssues/Receivers");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<List<string>>(responseBody);
            if (items is null)
            {
                return new List<string>();
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

        public async Task<List<string>> GetAllFinishedPurchaseOrderNumbersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductInventories/POs");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<List<string>>(responseBody);
            if (items is null)
            {
                return new List<string>();
            }
            return items;
        }

        //Receipt
        ///MaterialReceipt
        public async Task<GoodsReceiptDto> GetGoodsReceiptsByIdAsync(string goodsReceiptId)
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
        public async Task<IEnumerable<GoodsReceiptDto>> GetGoodsReceiptsByTimeAndStateAsync(DateTime startDate, DateTime endDate, bool isCompleted)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsReceipts/TimeRange/{isCompleted}?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GoodsReceiptDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task FixGoodsReceiptMaterialsAsync(string goodsReceiptId, IEnumerable<FixGoodsReceiptMaterialsDto> fixDto)
        {
            var json = JsonConvert.SerializeObject(fixDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/GoodsReceipts/{goodsReceiptId}/updatedGoodsReceiptLots", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task RemovedGoodsReceiptLotAsync(string goodsReceiptId, string goodsReceiptLotId)
        {
            List<string> goodsReceiptLotIds = new();
            goodsReceiptLotIds.Add(goodsReceiptLotId);
            var json = JsonConvert.SerializeObject(goodsReceiptLotIds);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/GoodsReceipts/{goodsReceiptId}/removedGoodsReceiptLots", content);
            response.EnsureSuccessStatusCode();
        }

        ///FinishedProductReceipt
        public async Task CreateFinishedProductReceiptsAsync(CreateFinishedProductReceiptDto goodsReceipts)
        {
            var json = JsonConvert.SerializeObject(goodsReceipts);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/FinishedProductReceipts", content);

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
                        switch (error.Code)
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
        public async Task<IEnumerable<FinishedProductReceiptDto>> GetFinishedProductReceiptsByTimeAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductReceipts/TimeRange?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<FinishedProductReceiptDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<FinishedProductReceiptDto> GetFinishedProductReceiptsByIdAsync(string goodsReceiptId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductReceipts/{goodsReceiptId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<FinishedProductReceiptDto>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }
            return result;
        }
        public async Task FixFinishedProductReceiptsAsync(string goodsReceiptId, IEnumerable<FixCompletedGoodsReceiptDto> fixDto)
        {
            var json = JsonConvert.SerializeObject(fixDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/FinishedProductReceipts/{goodsReceiptId}/updatedEntry", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddFinishedProductReceiptEntryAsync(string goodsReceiptId, CreateFinishedProductReceiptEntryDto newEntryDto)
        {
            List<CreateFinishedProductReceiptEntryDto> newEntries = new();
            newEntries.Add(newEntryDto);
            var json = JsonConvert.SerializeObject(newEntries);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/FinishedProductReceipts/{goodsReceiptId}/addedEntry", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemovedFinishedProductReceiptEntryAsync(string goodsReceiptId, string itemId, string unit, string purchaseOrderNumber)
        {
            List<RemovedFinishedProductReceiptEntryDto> finishedProductReceiptEntry = new();
            finishedProductReceiptEntry.Add(new RemovedFinishedProductReceiptEntryDto(itemId, unit, purchaseOrderNumber));
            var json = JsonConvert.SerializeObject(finishedProductReceiptEntry);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/FinishedProductReceipts/{goodsReceiptId}/removedEntry", content);
            response.EnsureSuccessStatusCode();
        }

        ///MaterialIssue
        public async Task<IEnumerable<GoodsIssueDto>> GetIssuedGoodsIssuesAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/GoodsIssues?StartTime={startDateString}&EndTime={endDateString}&isExported=true");

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
        public async Task FixGoodsIssueEntryMaterialsAsync(string goodsIssueId, IEnumerable<FixGoodsIssueEntryMaterialsDto> fixDto)
        {
            var json = JsonConvert.SerializeObject(fixDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/GoodsIssues/{goodsIssueId}/goodsIssueEntries", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateInternalGoodsIssuesAsync(CreateInternalGoodsIssueDto goodsIssues)
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
                        switch (error.Code)
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

        ///FinishedProductIssue
        public async Task<IEnumerable<FinishedProductIssueDto>> GetFinishedProductIssuesByTimeAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductIssues/Timerange?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<FinishedProductIssueDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<FinishedProductIssueDto> GetFinishedProductIssuesByIdAsync(string goodsIssueId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductIssues/{goodsIssueId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<FinishedProductIssueDto>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task AddFinishedProductIssueEntryAsync(string goodsIssueId, CreateExternalGoodsIssueEntryDto newEntryDto)
        {
            List<CreateExternalGoodsIssueEntryDto> newEntries = new();
            newEntries.Add(newEntryDto);
            var json = JsonConvert.SerializeObject(newEntries);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/FinishedProductIssues/{goodsIssueId}/addedEntry", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task RemovedFinishedProductIssueEntryAsync(string finishedProductIssueId, string itemId, string unit, string purchaseOrderNumber)
        {
            RemovedFinishedProductIssueEntryDto finishedProductReceiptEntry = new RemovedFinishedProductIssueEntryDto(finishedProductIssueId, itemId, unit, purchaseOrderNumber);

            var json = JsonConvert.SerializeObject(finishedProductReceiptEntry);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/FinishedProductIssues/removedEntry", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task CreateExternalGoodsIssuesAsync(CreateExternalGoodsIssueDto goodsIssues)
        {
            var json = JsonConvert.SerializeObject(goodsIssues);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/FinishedProductIssues", content);

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
                        switch (error.Code)
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

        //HistoryImport
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

        public async Task<IEnumerable<FinishedProductReceiptEntryDto>> GetHistoryFinishedProductReceiptEntriesAsync(string itemId, string purchaseOrderNumber, DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductReceipts/ImportHistory?itemId={itemId}&purchaseOrderNumber={purchaseOrderNumber}&StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<FinishedProductReceiptEntryDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        //HistoryExport
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

        public async Task<IEnumerable<FinishedProductIssueEntryDto>> GetHistoryFinishedProductIssueEntriesAsync(string receiver, string itemId, string purchaseOrderNumber, DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductIssues/ExportHistory?receiver={receiver}&itemId={itemId}&purchaseOrderNumber={purchaseOrderNumber}&StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<FinishedProductIssueEntryDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        //Inventory
        public async Task<IEnumerable<LotAdjustmentDto>> GetUnfixedLotAdjustmentsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/LotAdjustments/false");

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

        //Alarm
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

        //Stockcard
        ///MaterialStockCard
        public async Task<InventoryLogEntryItemLotDto> GetStockCardItemLotsAsync(DateTime endDate, string itemId)
        {
            string endDateString = endDate.ToString("yyyy-MM-dd");
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/InventoryLogEntries/itemLots?trackingTime={endDateString}&itemId={itemId}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<InventoryLogEntryItemLotDto>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        //public async Task<IEnumerable<ItemLotDto>> GetStockCardItemLotsAsync(DateTime endDate, string itemId)
        //{
        //    string endDateString = endDate.ToString("yyyy-MM-dd");
        //    HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/ItemLots/{itemId}/TimeRange?timestamp={endDateString}"); 

        //    response.EnsureSuccessStatusCode();
        //    string responseBody = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<IEnumerable<ItemLotDto>>(responseBody);

        //    if (result is null)
        //    {
        //        throw new Exception();
        //    }

        //    return result;
        //}

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

        ///FinishedProductStockCard
        public async Task<IEnumerable<FinishedProductInventoryDto>> GetFinishedProductStockCardAsync(DateTime endDate, string itemId, string unit)
        {
            string endDateString = endDate.ToString("yyyy-MM-dd");
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductInventories/{itemId}/{unit}?timestamp={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<FinishedProductInventoryDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }
        public async Task<InventoryLogExtendedEntryDto> GetFinishedProductExtendedStockCardEntriesByIdAsync(string itemId, string unit, DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductInventories/extendedProductLogEntry?itemId={itemId}&unit={unit}&StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<InventoryLogExtendedEntryDto>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<IEnumerable<InventoryLogExtendedEntryDto>> GetFinishedProductExtendedStockCardEntriesByTimeAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/FinishedProductInventories/extendedProductLogEntries?StartTime={startDateString}&EndTime={endDateString}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<InventoryLogExtendedEntryDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        //ShelfManagement
        public async Task<IEnumerable<ItemLotDto>> GetItemShelfManagementEntriesAsync(string itemId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/ItemLots/{itemId}");

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
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}api/ItemLots/{locationId}/lots");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ItemLotDto>>(responseBody);

            if (result is null)
            {
                throw new Exception();
            }

            return result;
        }

        //Isolation
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
            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/ItemLots/{LotId}?isIsolated=false", null);

            response.EnsureSuccessStatusCode();
        }
    }
}
