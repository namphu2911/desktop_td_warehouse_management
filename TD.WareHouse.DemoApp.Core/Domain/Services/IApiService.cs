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
        Task CreateItem(CreateItemDto item);
        Task<IEnumerable<GoodsReceiptDto>> GetAllGoodsReceiptsAsync();
        Task<IEnumerable<GoodsReceiptDto>> GetUncompletedGoodsReceiptsAsync();
        Task<IEnumerable<GoodsReceiptDto>> GetCompletedGoodsReceiptsAsync();
        Task<List<string>> GetAllFinishedProductReceiptIdAsync();
        Task<List<string>> GetUnfinishedGoodsIssuesIdAsync();
        Task<List<string>> GetFinishedGoodsIssuesIdAsync();
        Task<List<string>> GetFinishedGoodsIssuesReceiverAsync();
        Task<List<string>> GetAllGoodsIssuesReceiverAsync();
        Task<List<string>> GetAllGoodsReceiptsSupplierAsync();
        Task<List<string>> GetAllFinishedPurchaseOrderNumbersAsync();
        Task<IEnumerable<GoodsIssueDto>> GetUnconfirmedGoodsIssuesAsync();
        Task<IEnumerable<GoodsReceiptDto>> GetGoodsReceiptsByTimeAndStateAsync(DateTime startDate, DateTime endDate, bool isCompleted);
        Task<GoodsReceiptDto> GetGoodsReceiptsByIdAsync(string goodsReceiptId);
        //Task FixUncompltedGoodsReceiptAsync(string goodsReceiptId, IEnumerable<FixGoodsReceiptMaterialsDto> fixDto);
        Task FixGoodsReceiptMaterialsAsync(string goodsReceiptId, IEnumerable<FixGoodsReceiptMaterialsDto> fixDto);
        Task RemovedGoodsReceiptLotAsync(string goodsReceiptId, string goodsReceiptLotId);
        Task DeleteGoodsReceiptAsync(string goodsReceiptId);
        Task ConfirmGoodsReceiptAsync(string goodsReceiptId);
        //Nhap TP
        Task CreateFinishedProductReceiptsAsync(CreateFinishedProductReceiptDto goodsReceipt);
        Task<IEnumerable<FinishedProductReceiptDto>> GetFinishedProductReceiptsByTimeAsync(DateTime startDate, DateTime endDate);
        Task<FinishedProductReceiptDto> GetFinishedProductReceiptsByIdAsync(string goodsReceiptId);
        Task FixFinishedProductReceiptsAsync(string goodsReceiptId, IEnumerable<FixCompletedGoodsReceiptDto> fixDto);
        Task AddFinishedProductReceiptEntryAsync(string goodsReceiptId, CreateFinishedProductReceiptEntryDto newEntryDto);
        Task RemovedFinishedProductReceiptEntryAsync(string goodsReceiptId, string itemId, string unit, string purchaseOrderNumber);


        //
        Task<IEnumerable<GoodsIssueDto>> GetIssuedGoodsIssuesAsync(DateTime startDate, DateTime endDate);
        Task<GoodsIssueDto> GetIssuingGoodsIssuesAsync(string goodsIssueId);
        Task AddFinishedProductIssueEntryAsync(string goodsIssueId, CreateExternalGoodsIssueEntryDto newEntryDto);
        Task RemovedFinishedProductIssueEntryAsync(string finishedProductIssueId, string itemId, string unit, string purchaseOrderNumber);
        //
        Task<IEnumerable<FinishedProductIssueDto>> GetFinishedProductIssuesByTimeAsync(DateTime startDate, DateTime endDate);
        Task<FinishedProductIssueDto> GetFinishedProductIssuesByIdAsync(string goodsIssueId);
        Task FixGoodsIssueEntryMaterialsAsync(string goodsIssueId, IEnumerable<FixGoodsIssueEntryMaterialsDto> fixDto);
        Task DeleteGoodsIssueAsync(string goodsIssueId);
        Task ConfirmGoodsIssueAsync(string goodsIssueId);
        //HistoryImport
        Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsSupplierAsync(DateTime startDate, DateTime endDate, string supplier);
        Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<FinishedProductReceiptEntryDto>> GetHistoryFinishedProductReceiptEntriesAsync(string itemId, string purchaseOrderNumber, DateTime startDate, DateTime endDate);
        //
        Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsReceiverAsync(DateTime startDate, DateTime endDate, string receiver);
        Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<FinishedProductIssueEntryDto>> GetHistoryFinishedProductIssueEntriesAsync(string receiver, string itemId, string purchaseOrderNumber, DateTime startDate, DateTime endDate);
        Task<IEnumerable<LotAdjustmentDto>> GetUnfixedLotAdjustmentsAsync();
        Task<IEnumerable<LotAdjustmentDto>> GetConfirmedLotAdjustmentsAsync(DateTime startDate, DateTime endDate);
        Task FixLotAdjustmentAsync(string lotId);
        Task DeleteLotAdjustmentAsync(string lotId);

        //Issue
        Task CreateInternalGoodsIssuesAsync(CreateInternalGoodsIssueDto goodsIssue);
        Task CreateExternalGoodsIssuesAsync(CreateExternalGoodsIssueDto goodsIssue);

        //Task<IEnumerable<GoodsIssueDto>> GetPendingGoodsIssuesAsync();
        //Task<QueryResult<GoodsIssueEntryDtos>> GetGoodsIssueEntriesAsync(DateTime startTime, DateTime endTime, string itemId);
        //Task<QueryResult<GoodsIssueDto>> GetGoodsIssuesAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<ItemLotDto>> GetExpirationDateAlarmEntriesAsync(double timeLeft);
        Task<IEnumerable<ItemLotDto>> GetQuantityAlarmEntriesAsync(string warehouseId);
        //stockcard
        Task<IEnumerable<ItemLotDto>> GetStockCardItemLotsAsync(DateTime endDate, string itemId);
        Task<IEnumerable<InventoryLogEntryDto>> GetStockCardEntriesAsync(string itemId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<InventoryLogEntryDto>> GetStockCardEntriesByTimeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<InventoryLogExtendedEntryDto>> GetExtendedStockCardEntriesAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate);
        Task<InventoryLogExtendedEntryDto> GetFinishedProductExtendedStockCardEntriesByIdAsync(string itemId, string unit, DateTime startDate, DateTime endDate);
        Task<IEnumerable<InventoryLogExtendedEntryDto>> GetFinishedProductExtendedStockCardEntriesByTimeAsync(DateTime startDate, DateTime endDate);

        //Task<InventoryLogExtendedEntryDto> GetStockCardEntriesByItemAsync(string itemId, string unit, DateTime startDate, DateTime endDate);
        //shelf

        Task<IEnumerable<ItemLotDto>> GetLocationShelfManagementEntriesAsync(string locationId);

        Task<IEnumerable<ItemLotDto>> GetUnfixedItemLotsAsync();
        Task IssueIsolationItemLotsAsync(string itemLotId);
        Task ReceiveIsolationItemLotsAsync(string LotId);
    }
}
