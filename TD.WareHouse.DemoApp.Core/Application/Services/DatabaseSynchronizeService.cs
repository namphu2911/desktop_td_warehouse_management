using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Location;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Warehouse;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;
using TD.WareHouse.DemoApp.Core.Domain.Models.Warehouses;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.Services
{
    public class DatabaseSynchronizeService : IDatabaseSynchronizationService
    {
        private readonly IApiService _apiService;
        private readonly IItemDatabaseService _itemDatabaseService;
        private readonly IItemLotDatabaseService _itemLotDatabaseService;
        private readonly IWarehouseDatabaseService _warehouseDatabaseService;
        private readonly IGoodReceiptDatabaseService _goodReceiptDatabaseService;
        private readonly IGoodIssueDatabaseService _goodIssueDatabaseService;

        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        private readonly ItemLotStore _itemLotStore;
        private readonly WarehouseStore _warehouseStore;
        private readonly GoodsReceiptStore _goodsReceiptStore;
        private readonly GoodsIssueStore _goodsIssueStore;
        private readonly DepartmentStore _departmentStore;
        public DatabaseSynchronizeService(IApiService apiService, IItemDatabaseService itemDatabaseService, IItemLotDatabaseService itemLotDatabaseService, IWarehouseDatabaseService warehouseDatabaseService, IGoodReceiptDatabaseService goodReceiptDatabaseService, IGoodIssueDatabaseService goodIssueDatabaseService, IMapper mapper, ItemStore itemStore, ItemLotStore itemLotStore, WarehouseStore warehouseStore, GoodsReceiptStore goodsReceiptStore, GoodsIssueStore goodsIssueStore, DepartmentStore departmentStore)
        {
            _apiService = apiService;
            _mapper = mapper;

            _itemDatabaseService = itemDatabaseService;
            _itemLotDatabaseService = itemLotDatabaseService;
            _warehouseDatabaseService = warehouseDatabaseService;
            _goodReceiptDatabaseService = goodReceiptDatabaseService;
            _goodIssueDatabaseService = goodIssueDatabaseService;

            _itemStore = itemStore;
            _itemLotStore = itemLotStore;
            _warehouseStore = warehouseStore;
            _goodsReceiptStore = goodsReceiptStore;
            _goodsIssueStore = goodsIssueStore;
            _departmentStore = departmentStore;
        }

        public async Task SynchronizeItemsData()
        {
            try
            {
                await SynchronizeItemsFromServer();
            }
            catch
            {
                //await SynchronizeItemsFromLocal();
            }
        }

        private async Task SynchronizeItemsFromServer()
        {
            var itemDtos = await _apiService.GetAllItemsAsync();
            var items = _mapper.Map<IEnumerable<ItemDto>, IEnumerable<Item>>(itemDtos);

            //await _itemDatabaseService.OverrideAllItems(items);
            _itemStore.SetItem(items);
        }
        private async Task SynchronizeItemsFromLocal()
        {
            var items = await _itemDatabaseService.GetAllItems();
            _itemStore.SetItem(items);
        }

        public async Task SynchronizeItemLotsData()
        {
            try
            {
                await SynchronizeItemLotsFromServer();
            }
            catch
            {
                //await SynchronizeItemLotsFromLocal();
            }
        }
        private async Task SynchronizeItemLotsFromServer()
        {
            var purchaseOrderNumber = await _apiService.GetAllPurchaseOrderNumbersAsync();
            _itemLotStore.SetPurchaseOrderNumbers(purchaseOrderNumber);

            var itemLotDtos = await _apiService.GetAllItemLotsAsync();
            var itemLots = _mapper.Map<IEnumerable<ItemLotDto>, IEnumerable<ItemLot>>(itemLotDtos);

            //await _itemLotDatabaseService.OverrideAllItemLots(itemLots);
            _itemLotStore.SetItemLot(itemLots);
        }
        //private async Task SynchronizeItemLotsFromLocal()
        //{
        //    var itemLots = await _itemLotDatabaseService.GetAllItemLots();
        //    _itemLotStore.SetItemLot(itemLots);
        //}

        public async Task SynchronizeWarehousesData()
        {
            try
            {
                await SynchronizeWarehousesFromServer();
            }
            catch
            {
                //await SynchronizeWarehousesFromLocal();
            }
        }
        private async Task SynchronizeWarehousesFromServer()
        {
            var warehouseDtos = await _apiService.GetAllWarehousesAsync();
            var warehouses = _mapper.Map<IEnumerable<WarehouseDto>, IEnumerable<Warehouse>>(warehouseDtos);

            //await _warehouseDatabaseService.OverrideAllWarehouses(warehouses);
            _warehouseStore.SetWarehouse(warehouses);
        }
        private async Task SynchronizeWarehousesFromLocal()
        {
            var warehouses = await _warehouseDatabaseService.GetAllWarehouses();
            _warehouseStore.SetWarehouse(warehouses);
        }
     
        public async Task SynchronizeGoodReceiptsData()
        {
            try
            {
                await SynchronizeGoodsReceiptsFromServer();
            }
            catch
            {
                //await SynchronizeGoodsReceiptsFromLocal();
            }
        }
        private async Task SynchronizeGoodsReceiptsFromServer()
        {
            var goodsReceiptSupplier = await _apiService.GetAllGoodsReceiptsSupplierAsync();
            _goodsReceiptStore.SetGoodsIssueSuppliers(goodsReceiptSupplier);

            var goodsReceiptDto = await _apiService.GetAllGoodsReceiptsAsync();
            var goodsReceipts = _mapper.Map<IEnumerable<GoodsReceiptDto>, IEnumerable<GoodsReceipt>>(goodsReceiptDto);
            _goodsReceiptStore.SetGoodsReceipts(goodsReceipts);
        }
        //private async Task SynchronizeGoodsReceiptsFromLocal()
        //{
        //    var goodsReceipts = await _goodReceiptDatabaseService.GetAllGoodsReceipts();
        //    _goodsReceiptStore.SetGoodsReceipts(goodsReceipts);
        //}

        public async Task SynchronizeGoodIssuesData()
        {
            try
            {
                await SynchronizeGoodsIssuesFromServer();
            }
            catch
            {
                //await SynchronizeGoodsIssuesFromLocal();
            }
        }
        private async Task SynchronizeGoodsIssuesFromServer()
        {
            var unconfirmedGoodsIssueDtos = await _apiService.GetUnconfirmedGoodsIssuesAsync();
            var unconfirmedGoodsIssue = _mapper.Map<IEnumerable<GoodsIssueDto>, IEnumerable<GoodsIssue>>(unconfirmedGoodsIssueDtos);
            _goodsIssueStore.SetUnconfirmedGoodsIssues(unconfirmedGoodsIssue);

            var goodsIssueReceiver = await _apiService.GetAllGoodsIssuesReceiverAsync();
            _goodsIssueStore.SetGoodsIssueReceivers(goodsIssueReceiver);
            //await _goodIssueDatabaseService.OverrideAllGoodsIssues(goodsIssues);
            
        }
        //private async Task SynchronizeGoodsIssuesFromLocal()
        //{
        //    var goodsIssues = await _goodIssueDatabaseService.GetAllGoodsIssues();
        //    _goodsIssueStore.SetGoodsIssues(goodsIssues);
        //}

        public async Task SynchronizeDepartmentsData()
        {
            try
            {
                await SynchronizeDepartmentsFromServer();
            }
            catch
            {
                //await SynchronizeDepartmentsFromLocal();
            }
        }
        private async Task SynchronizeDepartmentsFromServer()
        {
            var departmentDtos = await _apiService.GetAllDepartmentsAsync();
            var departments = _mapper.Map<IEnumerable<DepartmentDto>, IEnumerable<Department>>(departmentDtos);

            //await _departmentDatabaseService.OverrideAllDepartments(departments);
            _departmentStore.SetDepartment(departments);
        }
        //private async Task SynchronizeDepartmentsFromLocal()
        //{
        //    var departments = await _departmentDatabaseService.GetAllDepartments();
        //    _DepartmentStore.SetDepartment(departments);
        //}
    }
}
