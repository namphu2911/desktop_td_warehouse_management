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

        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public string ItemId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        //
        public bool DecreaseButton { get; set; } = false;
        public bool IncreaseButton { get; set; } = true;
        private ushort pageNumber = 1;
        public ushort PageNumber
        {
            get
            {
                return pageNumber;
            }
            set
            {
                pageNumber = value;
                LoadStockCardByItemClassId();
                if (value == 1)
                {
                    DecreaseButton = false;
                }
                else
                {
                    DecreaseButton = true;
                }
                
                if (value == TotalPage)
                {
                    IncreaseButton = false;
                }
                else
                {
                    IncreaseButton = true;
                }
            }

        }

        private ushort itemsPerPage = 15;
        public ushort ItemsPerPage
        {
            get
            {
                return itemsPerPage;
            }
            set
            {
                PageNumber = 1;
                itemsPerPage = value;
                if (!String.IsNullOrEmpty(WarehouseId))
                {
                    LoadStockCardByItemClassId();
                }
            }

        }
        public ushort TotalPage { get; set; }
        public ObservableCollection<short> ItemsPerPages { get; private set; }
        public ObservableCollection<StockCardExtendedEntryViewModel> StockCardEntries { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.ItemIds;
        public ObservableCollection<string> WarehouseIds => _warehouseStore.WarehouseIds;
        public ICommand LoadStockCardEntryCommand { get; set; }
        public ICommand LoadStockCardViewCommand { get; set; }

        public ICommand IncreasePageNumberCommand { get; set; }
        public ICommand DecreasePageNumberCommand { get; set; }
        public ICommand FirstPageCommand { get; set; }
        public ICommand LastPageCommand { get; set; }

        public StockCardExtendedViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore, WarehouseStore warehouseStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
            _warehouseStore = warehouseStore;
            ItemsPerPages = new ObservableCollection<short>() { 10, 15, 20, 25 };

            LoadStockCardEntryCommand = new RelayCommand(LoadStockCardEntry);
            LoadStockCardViewCommand = new RelayCommand(LoadStockCardView);

            IncreasePageNumberCommand = new RelayCommand(IncreasePageNumber);
            DecreasePageNumberCommand = new RelayCommand(DecreasePageNumber);
            FirstPageCommand = new RelayCommand(FirstPage);
            LastPageCommand = new RelayCommand(LastPage);
        }

        private void LoadStockCardView()
        {
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(WarehouseIds));
        }
        private void LoadStockCardEntry()
        {
            ItemsPerPage = 15;
            if (String.IsNullOrEmpty(WarehouseId))
            {
                LoadStockCardByItemId();
            }
            else
            {
                LoadStockCardByItemClassId();
            }
            PageNumber = 1;
        }

        private async void LoadStockCardByItemId()
        {
            try
            {
                var dtos = await _apiService.GetExtendedStockCardEntriesAsync(WarehouseId, ItemId, StartDate, EndDate);
                var stockCardEntries = dtos.Results;
                TotalPage = (ushort)Math.Ceiling(dtos.TotalItems / (double)ItemsPerPage);
                var viewModels = _mapper.Map<IEnumerable<InventoryLogExtendedEntryDto>, IEnumerable<StockCardExtendedEntryViewModel>>(stockCardEntries);
                StockCardEntries = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        private async void LoadStockCardByItemClassId()
        {

            try
            {
                var dtos = await _apiService.GetExtendedStockCardEntriesAsync(WarehouseId, ItemId, StartDate, EndDate, PageNumber, ItemsPerPage);
                var stockCardEntries = dtos.Results;
                TotalPage = (ushort)Math.Ceiling(dtos.TotalItems / (double)ItemsPerPage);
                var viewModels = _mapper.Map<IEnumerable<InventoryLogExtendedEntryDto>, IEnumerable<StockCardExtendedEntryViewModel>>(stockCardEntries);
                StockCardEntries = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        private void IncreasePageNumber()
        {
            PageNumber++;
        }

        private void DecreasePageNumber()
        {
            PageNumber--;
        }
        private void FirstPage()
        {
            PageNumber = 1;
        }

        private void LastPage()
        {
            PageNumber = TotalPage;
        }

    }
}
