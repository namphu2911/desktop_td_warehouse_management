using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        private const string serverUrl = "https://chawarehousemicroserviceapi.azurewebsites.net";
        private string? _token;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<QueryResult<GoodsReceiptDto>> GetGoodsReceiptsAsync(DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/goodsreceipts?Page=1&ItemsPerPage=100&StartTime={startDateString}&EndTime={endDateString}");

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
            HttpResponseMessage response = await _httpClient.PatchAsync($"{serverUrl}/api/goodsreceipts/{goodsReceiptId}/confirmed", null);

            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteGoodsReceiptAsync(string goodsReceiptId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{serverUrl}/api/goodsreceipts/{goodsReceiptId}");

            response.EnsureSuccessStatusCode();
        }
    }
}
