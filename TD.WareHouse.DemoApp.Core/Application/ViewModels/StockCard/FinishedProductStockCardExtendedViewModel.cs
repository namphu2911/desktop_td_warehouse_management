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
                if (!String.IsNullOrEmpty(ItemId))
                {
                    LoadStockCardByItemId();
                }
                else
                {
                    LoadStockCardByTime();
                }
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
                if (String.IsNullOrEmpty(ItemId))
                {
                    LoadStockCardByTime();
                }
            }

        }
        public ushort TotalPage { get; set; }
        public ObservableCollection<short> ItemsPerPages { get; private set; }
        //
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

        public ICommand IncreasePageNumberCommand { get; set; }
        public ICommand DecreasePageNumberCommand { get; set; }
        public ICommand FirstPageCommand { get; set; }
        public ICommand LastPageCommand { get; set; }

        public FinishedProductStockCardExtendedViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
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
            OnPropertyChanged(nameof(Units));
        }

        private void LoadStockCardEntry()
        {
            ItemsPerPage = 15;
            if (!String.IsNullOrEmpty(ItemId))
            {
                LoadStockCardByItemId();
            }
            else
            {
                LoadStockCardByTime();
            }
            PageNumber = 1;
        }

        private async void LoadStockCardByItemId()
        {

            try
            {
                var dtos = await _apiService.GetFinishedProductExtendedStockCardEntriesByIdAsync(ItemId, Unit, StartDate, EndDate);
                var stockCardEntries = dtos.Results;
                TotalPage = (ushort)Math.Ceiling(dtos.TotalItems / (double)ItemsPerPage);
                if (pageNumber == 1)
                {
                    DecreaseButton = false;
                }
                else
                {
                    DecreaseButton = true;
                }

                if (pageNumber == TotalPage)
                {
                    IncreaseButton = false;
                }
                else
                {
                    IncreaseButton = true;
                }
                var viewModels = _mapper.Map<IEnumerable<InventoryLogExtendedEntryDto>, IEnumerable<StockCardExtendedEntryViewModel>>(stockCardEntries);
                StockCardEntries = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        private async void LoadStockCardByTime()
        {

            try
            {
                var dtos = await _apiService.GetFinishedProductExtendedStockCardEntriesByTimeAsync(StartDate, EndDate, PageNumber, ItemsPerPage);
                var stockCardEntries = dtos.Results;
                TotalPage = (ushort)Math.Ceiling(dtos.TotalItems / (double)ItemsPerPage);
                if (pageNumber == 1)
                {
                    DecreaseButton = false;
                }
                else
                {
                    DecreaseButton = true;
                }

                if (pageNumber == TotalPage)
                {
                    IncreaseButton = false;
                }
                else
                {
                    IncreaseButton = true;
                }
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
