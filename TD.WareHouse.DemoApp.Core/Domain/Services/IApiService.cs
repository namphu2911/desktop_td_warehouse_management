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
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<IEnumerable<GoodsReceiptDto>> GetAllGoodsReceiptsAsync();
        Task<IEnumerable<GoodsReceiptDto>> GetUnconfirmedGoodsReceiptsAsync();
        Task<List<string>> GetAllGoodsIssuesReceiverAsync();
        Task<List<string>> GetAllGoodsReceiptsSupplierAsync();
        Task<List<string>> GetAllPurchaseOrderNumbersAsync();
        Task<IEnumerable<GoodsIssueDto>> GetUnconfirmedGoodsIssuesAsync();
        Task<IEnumerable<GoodsReceiptDto>> GetReceivedGoodsReceiptsAsync(DateTime startDate, DateTime endDate);
        Task<GoodsReceiptDto> GetReceivingGoodsReceiptsAsync(string goodsReceiptId);
        Task CreateGoodsReceiptsAsync(CreateGoodsReceiptDto goodsReceipt);
        //Task FixUncompltedGoodsReceiptAsync(string goodsReceiptId, IEnumerable<FixUncompletedGoodsReceiptDto> fixDto);
        Task FixCompltedGoodsReceiptAsync(string goodsReceiptId, IEnumerable<FixCompletedGoodsReceiptDto> fixDto);
        Task DeleteGoodsReceiptAsync(string goodsReceiptId);
        Task ConfirmGoodsReceiptAsync(string goodsReceiptId);
        Task<IEnumerable<GoodsIssueDto>> GetIssuedGoodsIssuesAsync(DateTime startDate, DateTime endDate);
        Task<GoodsIssueDto> GetIssuingGoodsIssuesAsync(string goodsIssueId);
        Task DeleteGoodsIssueAsync(string goodsIssueId);
        Task ConfirmGoodsIssueAsync(string goodsIssueId);
        //HistoryImport
        Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsPOAsync(string purchaseOrderNumber);
        Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsSupplierAsync(DateTime startDate, DateTime endDate, string supplier);
        Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate);
        //
        Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsPOAsync(string purchaseOrderNumber);
        Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsReceiverAsync(DateTime startDate, DateTime endDate, string receiver);
        Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<LotAdjustmentDto>> GetUnfixedLotAdjustmentsAsync();
        Task<IEnumerable<LotAdjustmentDto>> GetConfirmedLotAdjustmentsAsync(DateTime startDate, DateTime endDate);
        Task FixLotAdjustmentAsync(string lotId);
        Task DeleteLotAdjustmentAsync(string lotId);

        //Issue
        
        Task CreateGoodsIssuesAsync(CreateGoodsIssueDto goodsIssue);
        //Task<IEnumerable<GoodsIssueDto>> GetPendingGoodsIssuesAsync();
        //Task<QueryResult<GoodsIssueEntryDtos>> GetGoodsIssueEntriesAsync(DateTime startTime, DateTime endTime, string itemId);
        //Task<QueryResult<GoodsIssueDto>> GetGoodsIssuesAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<ItemLotDto>> GetExpirationDateAlarmEntriesAsync(double timeLeft);
        Task<IEnumerable<ItemLotDto>> GetQuantityAlarmEntriesAsync(string warehouseId);
        //stockcard
        Task<IEnumerable<InventoryLogEntryDto>> GetStockCardEntriesAsync(string itemId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<InventoryLogEntryDto>> GetStockCardEntriesByTimeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<InventoryLogExtendedEntryDto>> GetExtendedStockCardEntriesAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate);
        //Task<InventoryLogExtendedEntryDto> GetStockCardEntriesByItemAsync(string itemId, string unit, DateTime startDate, DateTime endDate);
        //shelf
        Task<IEnumerable<ItemLotDto>> GetItemShelfManagementEntriesAsync(string itemId);
        Task<IEnumerable<ItemLotDto>> GetLocationShelfManagementEntriesAsync(string locationId);

        Task<IEnumerable<ItemLotDto>> GetUnfixedItemLotsAsync();
        Task IssueIsolationItemLotsAsync(string itemLotId);
        Task ReceiveIsolationItemLotsAsync(string LotId);
    }
}
