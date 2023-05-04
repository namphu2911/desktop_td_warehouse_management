using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IApiService
    {
        void SetToken(string token);
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task<IEnumerable<WarehouseDto>> GetAllWarehousesAsync();
        Task<IEnumerable<ItemLotDto>> GetAllItemLotsAsync();
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task<IEnumerable<GoodsReceiptDto>> GetAllGoodsReceiptsAsync();
        Task<List<string>> GetAllGoodsIssuesReceiverAsync();
        Task<QueryResult<GoodsReceiptDto>> GetReceivedGoodsReceiptsAsync(DateTime startDate, DateTime endDate);
        Task<QueryResult<GoodsReceiptDto>> GetReceivingGoodsReceiptsAsync(string goodsReceiptId);
        Task DeleteGoodsReceiptAsync(string goodsReceiptId);
        Task ConfirmGoodsReceiptAsync(string goodsReceiptId);
        Task<QueryResult<GoodsIssueDto>> GetIssuedGoodsIssuesAsync(DateTime startDate, DateTime endDate);
        Task<QueryResult<GoodsIssueDto>> GetIssuingGoodsIssuesAsync(string goodsIssueId);
        Task DeleteGoodsIssueAsync(string goodsIssueId);
        Task ConfirmGoodsIssueAsync(string goodsIssueId);
        Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsAsync(string warehouseId, string itemId, string itemName, DateTime startDate, DateTime endDate, string supplier, string purchaseOrderNumber);
        Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsAsync(string warehouseId, string itemId, string itemName, DateTime startDate, DateTime endDate, string reveiver, string purchaseOrderNumber);
        Task<IEnumerable<LotAdjustmentDto>> GetUnfixedLotAdjustmentsAsync();
        //Task<IEnumerable<LotAdjustmentDto>> GetLotAdjustmentsAsync(DateTime startDate, DateTime endDate);
        Task FixLotAdjustmentAsync(string lotId);
        Task DeleteLotAdjustmentAsync(string lotId);

        //Issue
        
        Task CreateGoodsIssuesAsync(IEnumerable<CreateGoodsIssueDto> goodsIssue);
        //Task<IEnumerable<GoodsIssueDto>> GetPendingGoodsIssuesAsync();
        //Task<QueryResult<GoodsIssueEntryDtos>> GetGoodsIssueEntriesAsync(DateTime startTime, DateTime endTime, string itemId);
        //Task<QueryResult<GoodsIssueDto>> GetGoodsIssuesAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<ItemLotDto>> GetExpirationDateAlarmEntriesAsync(double timeLeft);
        Task<IEnumerable<ItemLotDto>> GetQuantityAlarmEntriesAsync(string warehouseId);

        Task<IEnumerable<InventoryLogEntryDto>> GetStockCardEntriesAsync(string warehouseId, string itemId, string itemName, DateTime startDate, DateTime endDate, string purchaseOrderNumber);
        Task<IEnumerable<ItemLotDto>> GetItemShelfManagementEntriesAsync(string itemId);
        Task<IEnumerable<ItemLotDto>> GetLocationShelfManagementEntriesAsync(string locationId);

        Task<IEnumerable<ItemLotDto>> GetUnfixedItemLotsAsync();
        Task IssueIsolationItemLotsAsync(string itemLotId);
        Task ReceiveIsolationItemLotsAsync(string LotId);
    }
}
