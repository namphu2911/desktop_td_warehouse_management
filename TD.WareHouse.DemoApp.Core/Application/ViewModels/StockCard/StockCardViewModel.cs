using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class StockCardViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        private readonly ItemLotStore _itemLotStore;
        private readonly WarehouseStore _warehouseStore;
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-30).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        private string itemId = "";
        private string itemName = "";

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                var item = _itemStore.Items.First(i => i.ItemId == itemId);
                itemName = item.ItemName;
                OnPropertyChanged(nameof(ItemName));
            }
        }
        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
                var item = _itemStore.Items.First(i => i.ItemName == itemName);
                itemId = item.ItemId;
                OnPropertyChanged(nameof(ItemId));
            }
        }
        public string WarehouseId { get; set; } = "";
        public string PurchaseOrderNumber { get; set; } = "";
        public ObservableCollection<StockCardEntryViewModel> StockCardEntries { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.ItemIds;
        public ObservableCollection<string> ItemNames => _itemStore.ItemNames;
        public ObservableCollection<string> WarehouseIds => _warehouseStore.WarehouseIds;
        public ObservableCollection<string> PurchaseOrderNumbers => _itemLotStore.PurchaseOrderNumbers;

        public ICommand LoadStockCardEntryCommand { get; set; }
        public ICommand LoadStockCardViewCommand { get; set; }
        public StockCardViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore, ItemLotStore itemLotStore, WarehouseStore warehouseStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
            _itemLotStore = itemLotStore;
            _warehouseStore = warehouseStore;
            LoadStockCardEntryCommand = new RelayCommand(LoadStockCardEntry);
            LoadStockCardViewCommand = new RelayCommand(LoadStockCardView);
        }

        private void LoadStockCardView()
        {
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(WarehouseIds));
            OnPropertyChanged(nameof(PurchaseOrderNumbers));
        }
        private async void LoadStockCardEntry()
        {
            try
            {
                var stockCardEntries = await _apiService.GetStockCardEntriesAsync(WarehouseId, ItemId, ItemName, StartDate, EndDate, PurchaseOrderNumber);
                var viewModels = _mapper.Map<IEnumerable<InventoryLogEntryDto>, IEnumerable<StockCardEntryViewModel>>(stockCardEntries);
                StockCardEntries = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }


    }
}

