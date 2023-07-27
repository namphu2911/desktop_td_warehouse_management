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
    public class FinishedProductStockCardExtendedViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;

        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public ObservableCollection<StockCardExtendedEntryViewModel> StockCardEntries { get; set; } = new();
        
        public ObservableCollection<string> ItemIds => _itemStore.FinishedItemIds;
        public ObservableCollection<string> Units => _itemStore.FinishedItemUnits;
        private string itemId = "";
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                if (String.IsNullOrEmpty(value))
                {
                    Unit = "";
                    OnPropertyChanged(nameof(Unit));
                }
                else
                {
                    var item = _itemStore.FinishedItems.First(i => i.ItemId == itemId);
                    Unit = item.Unit;
                    OnPropertyChanged(nameof(Unit));
                }
            }

        }
        public string Unit { get; set; } = "";
        public ICommand LoadStockCardEntryCommand { get; set; }
        public ICommand LoadStockCardViewCommand { get; set; }

        public FinishedProductStockCardExtendedViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
            LoadStockCardEntryCommand = new RelayCommand(LoadStockCardEntry);
            LoadStockCardViewCommand = new RelayCommand(LoadStockCardView);
        }

        private void LoadStockCardView()
        {
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(Units));
        }
        private async void LoadStockCardEntry()
        {

            try
            {
                if (!String.IsNullOrEmpty(ItemId))
                {
                    var stockCardEntry = await _apiService.GetFinishedProductExtendedStockCardEntriesByIdAsync(ItemId, Unit, StartDate, EndDate);
                    List<InventoryLogExtendedEntryDto> entries = new List<InventoryLogExtendedEntryDto>();
                    entries.Add(stockCardEntry);
                    IEnumerable<InventoryLogExtendedEntryDto> stockCardEntries = entries;
                    var viewModels = _mapper.Map<IEnumerable<InventoryLogExtendedEntryDto>, IEnumerable<StockCardExtendedEntryViewModel>>(stockCardEntries);
                    StockCardEntries = new(viewModels);
                }
                else
                {
                    var stockCardEntries = await _apiService.GetFinishedProductExtendedStockCardEntriesByTimeAsync(StartDate, EndDate);
                    var viewModels = _mapper.Map<IEnumerable<InventoryLogExtendedEntryDto>, IEnumerable<StockCardExtendedEntryViewModel>>(stockCardEntries);
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
