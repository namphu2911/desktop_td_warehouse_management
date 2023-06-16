using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class StockCardExtendedViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        private readonly WarehouseStore _warehouseStore;

        private List<InventoryLogExtendedEntryDto> stockCardEntries = new();
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-30).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(+1).Date;
        public string ItemId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        public ObservableCollection<StockCardExtendedEntryViewModel> StockCardEntries { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.ItemIds;
        public ObservableCollection<string> WarehouseIds => _warehouseStore.WarehouseIds;
        public ICommand LoadStockCardEntryCommand { get; set; }
        public ICommand LoadStockCardViewCommand { get; set; }
        
        public StockCardExtendedViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore, WarehouseStore warehouseStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
            _warehouseStore = warehouseStore;
            LoadStockCardEntryCommand = new RelayCommand(LoadStockCardEntry);
            LoadStockCardViewCommand = new RelayCommand(LoadStockCardView);
        }

        private void LoadStockCardView()
        {
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(WarehouseIds));
        }
        private async void LoadStockCardEntry()
        {
            
            try
            {
                var stockCardEntries = await _apiService.GetExtendedStockCardEntriesAsync(WarehouseId, ItemId, StartDate, EndDate);
                var viewModels = _mapper.Map<IEnumerable<InventoryLogExtendedEntryDto>, IEnumerable<StockCardExtendedEntryViewModel>>(stockCardEntries);
                StockCardEntries = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }


    }
}
