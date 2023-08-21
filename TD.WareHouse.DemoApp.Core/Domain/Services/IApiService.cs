using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TD.WareHouse.DemoApp.Core.Domain.Services
{
    public interface IApiService
    {
        void SetToken(string token);
        //MiscellaneousData
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task CreateItem(CreateItemDto item);
        Task CreateItemFromExcel(CreateListItemDto item);
        Task FixItemAsync(FixItemDto fixDto);
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task CreateEmployee(CreateEmployeeDto employee);
        Task<IEnumerable<WarehouseDto>> GetAllWarehousesAsync();
        Task CreateLocation(CreateLocationDto location);
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task CreateDepartment(CreateDepartmentDto department);

        //OtherStore
        ///Receipt
        Task<IEnumerable<GoodsReceiptDto>> GetUncompletedGoodsReceiptMaterialsAsync();
        Task<IEnumerable<GoodsReceiptDto>> GetCompletedGoodsReceiptMaterialsAsync();
        Task<List<string>> GetAllFinishedProductReceiptIdAsync();
        Task<List<string>> GetAllGoodsReceiptsSupplierAsync();
        ///Issue
        Task<List<string>> GetUnfinishedGoodsIssuesIdAsync();
        Task<List<string>> GetFinishedGoodsIssuesIdAsync();
        Task<List<string>> GetFinishedGoodsIssuesReceiverAsync();
        Task<List<string>> GetAllGoodsIssuesReceiverAsync();
        Task<List<string>> GetAllFinishedPurchaseOrderNumbersAsync();

        //Receipt
        ///MaterialReceipt
        Task<GoodsReceiptDto> GetGoodsReceiptsByIdAsync(string goodsReceiptId);
        Task<IEnumerable<GoodsReceiptDto>> GetGoodsReceiptsByTimeAndStateAsync(DateTime startDate, DateTime endDate, bool isCompleted);
        Task FixGoodsReceiptMaterialsAsync(string goodsReceiptId, IEnumerable<FixGoodsReceiptMaterialsDto> fixDto);
        Task RemovedGoodsReceiptLotAsync(string goodsReceiptId, string goodsReceiptLotId);
        
        ///FinishedProductReceipt
        Task CreateFinishedProductReceiptsAsync(CreateFinishedProductReceiptDto goodsReceipt);
        Task<IEnumerable<FinishedProductReceiptDto>> GetFinishedProductReceiptsByTimeAsync(DateTime startDate, DateTime endDate);
        Task<FinishedProductReceiptDto> GetFinishedProductReceiptsByIdAsync(string goodsReceiptId); 
        Task FixFinishedProductReceiptsAsync(string goodsReceiptId, IEnumerable<FixCompletedGoodsReceiptDto> fixDto);
        Task AddFinishedProductReceiptEntryAsync(string goodsReceiptId, CreateFinishedProductReceiptEntryDto newEntryDto);
        Task RemovedFinishedProductReceiptEntryAsync(string goodsReceiptId, string itemId, string unit, string purchaseOrderNumber);

        ///MaterialIssue
        Task<IEnumerable<GoodsIssueDto>> GetIssuedGoodsIssuesAsync(DateTime startDate, DateTime endDate);
        Task<GoodsIssueDto> GetIssuingGoodsIssuesAsync(string goodsIssueId);
        Task FixGoodsIssueEntryMaterialsAsync(string goodsIssueId, IEnumerable<FixGoodsIssueEntryMaterialsDto> fixDto);
        Task CreateInternalGoodsIssuesAsync(CreateInternalGoodsIssueDto goodsIssue);

        ///FinishedProductIssue
        Task<IEnumerable<FinishedProductIssueDto>> GetFinishedProductIssuesByTimeAsync(DateTime startDate, DateTime endDate);
        Task<FinishedProductIssueDto> GetFinishedProductIssuesByIdAsync(string goodsIssueId);
        Task AddFinishedProductIssueEntryAsync(string goodsIssueId, CreateExternalGoodsIssueEntryDto newEntryDto);
        Task RemovedFinishedProductIssueEntryAsync(string finishedProductIssueId, string itemId, string unit, string purchaseOrderNumber);
        Task CreateExternalGoodsIssuesAsync(CreateExternalGoodsIssueDto goodsIssue);

        //HistoryImport
        Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsSupplierAsync(DateTime startDate, DateTime endDate, string supplier);
        Task<IEnumerable<GoodsReceiptDto>> GetHistoryGoodsReceiptLotsAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<FinishedProductReceiptEntryDto>> GetHistoryFinishedProductReceiptEntriesAsync(string itemId, string purchaseOrderNumber, DateTime startDate, DateTime endDate);
        
        //HistoryExport
        Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsReceiverAsync(DateTime startDate, DateTime endDate, string receiver);
        Task<IEnumerable<GoodsIssueDto>> GetHistoryGoodsIssueLotsAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<FinishedProductIssueEntryDto>> GetHistoryFinishedProductIssueEntriesAsync(string receiver, string itemId, string purchaseOrderNumber, DateTime startDate, DateTime endDate);
        
        //Inventory
        Task<IEnumerable<LotAdjustmentDto>> GetUnfixedLotAdjustmentsAsync();
        Task<IEnumerable<LotAdjustmentDto>> GetConfirmedLotAdjustmentsAsync(DateTime startDate, DateTime endDate);
        Task FixLotAdjustmentAsync(string lotId);
        Task DeleteLotAdjustmentAsync(string lotId);

        //Alarm
        Task<IEnumerable<ItemLotDto>> GetExpirationDateAlarmEntriesAsync(double timeLeft);
        Task<IEnumerable<ItemLotDto>> GetQuantityAlarmEntriesAsync(string warehouseId);

        //Stockcard
        ///MaterialStockCard
        Task<InventoryLogEntryItemLotDto> GetStockCardItemLotsAsync(DateTime endDate, string itemId);
        //Task<IEnumerable<ItemLotDto>> GetStockCardItemLotsAsync(DateTime endDate, string itemId);
        Task<ExtendedStockCardDto> GetExtendedStockCardEntriesAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate);
        Task<ExtendedStockCardDto> GetExtendedStockCardEntriesAsync(string warehouseId, string itemId, DateTime startDate, DateTime endDate, ushort pageNumber, ushort itemsPerPage);
        ///FinishedProductStockCard
        Task<IEnumerable<FinishedProductInventoryDto>> GetFinishedProductStockCardAsync(DateTime endDate, string itemId, string unit);
        Task<InventoryLogExtendedEntryDto> GetFinishedProductExtendedStockCardEntriesByIdAsync(string itemId, string unit, DateTime startDate, DateTime endDate);
        Task<IEnumerable<InventoryLogExtendedEntryDto>> GetFinishedProductExtendedStockCardEntriesByTimeAsync(DateTime startDate, DateTime endDate);

        //ShelfManagement
        Task<IEnumerable<ItemLotDto>> GetItemShelfManagementEntriesAsync(string itemId);
        Task<IEnumerable<ItemLotDto>> GetLocationShelfManagementEntriesAsync(string locationId);
        
        //Isolation
        Task<IEnumerable<ItemLotDto>> GetUnfixedItemLotsAsync();
        Task IssueIsolationItemLotsAsync(string itemLotId);
        Task ReceiveIsolationItemLotsAsync(string LotId);
    }
}
