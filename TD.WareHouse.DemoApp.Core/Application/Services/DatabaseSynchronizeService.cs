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
        private readonly ILocationDatabaseService _locationDatabaseService;
        private readonly IGoodReceiptDatabaseService _goodReceiptDatabaseService;
        private readonly IGoodIssueDatabaseService _goodIssueDatabaseService;

        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        private readonly ItemLotStore _itemLotStore;
        private readonly LocationStore _locationStore;
        private readonly WarehouseStore _warehouseStore;
        private readonly GoodsReceiptStore _goodsReceiptStore;
        private readonly GoodsIssueStore _goodsIssueStore;
        
        public DatabaseSynchronizeService(IApiService apiService, IItemDatabaseService itemDatabaseService, IItemLotDatabaseService itemLotDatabaseService, IWarehouseDatabaseService warehouseDatabaseService, ILocationDatabaseService locationDatabaseService, IGoodReceiptDatabaseService goodReceiptDatabaseService, IGoodIssueDatabaseService goodIssueDatabaseService, IMapper mapper, ItemStore itemStore, ItemLotStore itemLotStore, LocationStore locationStore, WarehouseStore warehouseStore, GoodsReceiptStore goodsReceiptStore, GoodsIssueStore goodsIssueStore)
        {
            _apiService = apiService;
            _mapper = mapper;

            _itemDatabaseService = itemDatabaseService;
            _itemLotDatabaseService = itemLotDatabaseService;
            _warehouseDatabaseService = warehouseDatabaseService;
            _locationDatabaseService = locationDatabaseService;
            _goodReceiptDatabaseService = goodReceiptDatabaseService;
            _goodIssueDatabaseService = goodIssueDatabaseService;
           
            _itemStore = itemStore;
            _itemLotStore = itemLotStore;
            _locationStore = locationStore;
            _warehouseStore = warehouseStore;
            _goodsReceiptStore = goodsReceiptStore;
            _goodsIssueStore = goodsIssueStore;
        }

        public async Task SynchronizeItemsData()
        {
            try
            {
                await SynchronizeItemsFromServer();
            }
            catch
            {
                await SynchronizeItemsFromLocal();
            }
        }

        private async Task SynchronizeItemsFromServer()
        {
            var itemDtos = await _apiService.GetAllItemsAsync();
            var items = _mapper.Map<IEnumerable<ItemDto>, IEnumerable<Item>>(itemDtos);

            await _itemDatabaseService.OverrideAllItems(items);
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
                await SynchronizeItemLotsFromLocal();
            }
        }
        private async Task SynchronizeItemLotsFromServer()
        {
            var itemLotDtos = await _apiService.GetAllItemLotsAsync();
            var itemLots = _mapper.Map<IEnumerable<ItemLotDto>, IEnumerable<ItemLot>>(itemLotDtos);

            await _itemLotDatabaseService.OverrideAllItemLots(itemLots);
            _itemLotStore.SetItemLot(itemLots);
        }
        private async Task SynchronizeItemLotsFromLocal()
        {
            var itemLots = await _itemLotDatabaseService.GetAllItemLots();
            _itemLotStore.SetItemLot(itemLots);
        }

        public async Task SynchronizeWarehousesData()
        {
            try
            {
                await SynchronizeWarehousesFromServer();
            }
            catch
            {
                await SynchronizeWarehousesFromLocal();
            }
        }
        private async Task SynchronizeWarehousesFromServer()
        {
            var warehouseDtos = await _apiService.GetAllWarehousesAsync();
            var warehouses = _mapper.Map<IEnumerable<WarehouseDto>, IEnumerable<Warehouse>>(warehouseDtos);

            await _warehouseDatabaseService.OverrideAllWarehouses(warehouses);
            _warehouseStore.SetWarehouse(warehouses);
        }
        private async Task SynchronizeWarehousesFromLocal()
        {
            var warehouses = await _warehouseDatabaseService.GetAllWarehouses();
            _warehouseStore.SetWarehouse(warehouses);
        }

        public async Task SynchronizeLocationsData()
        {
            try
            {
                await SynchronizeLocationsFromServer();
            }
            catch
            {
                await SynchronizeLocationsFromLocal();
            }
        }
        private async Task SynchronizeLocationsFromServer()
        {
            var locationDtos = await _apiService.GetAllLocationsAsync();
            var locations = _mapper.Map<IEnumerable<LocationDto>, IEnumerable<Location>>(locationDtos);

            await _locationDatabaseService.OverrideAllLocations(locations);
            _locationStore.SetLocation(locations);
        }
        private async Task SynchronizeLocationsFromLocal()
        {
            var locations = await _locationDatabaseService.GetAllLocations();
            _locationStore.SetLocation(locations);
        }

        public async Task SynchronizeGoodReceiptsData()
        {
            try
            {
                await SynchronizeGoodsReceiptsFromServer();
            }
            catch
            {
                await SynchronizeGoodsReceiptsFromLocal();
            }
        }
        private async Task SynchronizeGoodsReceiptsFromServer()
        {
            var goodsReceiptDtos = await _apiService.GetAllGoodsReceiptsAsync();
            var goodsReceipts = _mapper.Map<IEnumerable<GoodsReceiptDto>, IEnumerable<GoodsReceipt>>(goodsReceiptDtos);

            await _goodReceiptDatabaseService.OverrideAllGoodsReceipts(goodsReceipts);
            _goodsReceiptStore.SetGoodsReceipts(goodsReceipts);
        }
        private async Task SynchronizeGoodsReceiptsFromLocal()
        {
            var goodsReceipts = await _goodReceiptDatabaseService.GetAllGoodsReceipts();
            _goodsReceiptStore.SetGoodsReceipts(goodsReceipts);
        }

        public async Task SynchronizeGoodIssuesData()
        {
            try
            {
                await SynchronizeGoodsIssuesFromServer();
            }
            catch
            {
                await SynchronizeGoodsIssuesFromLocal();
            }
        }
        private async Task SynchronizeGoodsIssuesFromServer()
        {
            var goodsIssueDtos = await _apiService.GetAllGoodsIssuesAsync();
            var goodsIssues = _mapper.Map<IEnumerable<GoodsIssueDto>, IEnumerable<GoodsIssue>>(goodsIssueDtos);

            await _goodIssueDatabaseService.OverrideAllGoodsIssues(goodsIssues);
            _goodsIssueStore.SetGoodsIssues(goodsIssues);
        }
        private async Task SynchronizeGoodsIssuesFromLocal()
        {
            var goodsIssues = await _goodIssueDatabaseService.GetAllGoodsIssues();
            _goodsIssueStore.SetGoodsIssues(goodsIssues);
        }


    }
}
