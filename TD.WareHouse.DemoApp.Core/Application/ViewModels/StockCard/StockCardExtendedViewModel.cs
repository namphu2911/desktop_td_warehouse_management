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
    public class StockCardExtendedViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        private readonly WarehouseStore _warehouseStore;

        private List<InventoryLogExtendedEntryDto> stockCardEntries = new();
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-30).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public string ItemId { get; set; } = "";
        public string Unit { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        public ObservableCollection<StockCardExtendedEntryViewModel> StockCardEntries { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.ItemIds;
        public ObservableCollection<string> Units => _itemStore.Units;
        public ObservableCollection<string> WarehouseIds => _warehouseStore.WarehouseIds;
        public ICommand LoadStockCardEntryCommand { get; set; }
        public ICommand LoadStockCardViewCommand { get; set; }
        public bool ButtonEnable => (!String.IsNullOrEmpty(WarehouseId) && String.IsNullOrEmpty(ItemId) && String.IsNullOrEmpty(Unit))
            || (!String.IsNullOrEmpty(ItemId) && !String.IsNullOrEmpty(Unit) && String.IsNullOrEmpty(WarehouseId));
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
            OnPropertyChanged(nameof(Units));
            OnPropertyChanged(nameof(WarehouseIds));
            OnPropertyChanged(nameof(ButtonEnable));
        }
        private async void LoadStockCardEntry()
        {
            try
            {
                if (!String.IsNullOrEmpty(WarehouseId) && String.IsNullOrEmpty(ItemId) && String.IsNullOrEmpty(Unit))
                {
                    var stockCardEntries = await _apiService.GetStockCardEntriesByWarehouseAsync(WarehouseId, StartDate, EndDate);
                    var viewModels = _mapper.Map<IEnumerable<InventoryLogExtendedEntryDto>, IEnumerable<StockCardExtendedEntryViewModel>>(stockCardEntries);
                    StockCardEntries = new(viewModels);

                }
                else
                    if (!String.IsNullOrEmpty(ItemId) && !String.IsNullOrEmpty(Unit) && String.IsNullOrEmpty(WarehouseId))
                {
                    stockCardEntries = new();
                    var stockCardEntry = await _apiService.GetStockCardEntriesByItemAsync(ItemId, Unit, StartDate, EndDate);
                    stockCardEntries.Add(stockCardEntry);
                    var viewModels = stockCardEntries.Select(g =>
                    new StockCardExtendedEntryViewModel(g.Item.ItemClassId,
                                                        g.Item.ItemId,
                                                        g.Item.ItemName,
                                                        g.Item.Unit,
                                                        g.BeforeQuantity,
                                                        g.AfterQuantity,
                                                        g.ReceivedQuantity,
                                                        g.ShippedQuantity));
                    StockCardEntries = new(viewModels);
                                   
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }


    }
}
