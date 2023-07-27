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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.MiscellaneousData
{
    public class ManageItemViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        private readonly WarehouseStore _warehouseStore;
        public ObservableCollection<string> Units => _itemStore.Units;
        public ObservableCollection<string> WarehouseIds => _warehouseStore.WarehouseIds;
        IDatabaseSynchronizationService _databaseSynchronizationService;

        public string ItemClassId { get; set; } = "";
        public string ItemId { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string Unit { get; set; } = "";
        public double MinimumStockLevel { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public string ItemIdFilter { get; set; } = "";
        private List<ItemDto> items = new();
        public ObservableCollection<ItemViewModel> Items { get; set; } = new();
        public ICommand LoadAllItemsCommand { get; set; }
        public ICommand FilterItemsCommand { get; set; }
        public ICommand CreateItemCommand { get; set; }
        public ICommand LoadManageItemViewCommand { get; set; }
        public ManageItemViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore, WarehouseStore warehouseStore, IDatabaseSynchronizationService databaseSynchronizationService)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
            _warehouseStore = warehouseStore;
            _databaseSynchronizationService = databaseSynchronizationService;

            LoadAllItemsCommand = new RelayCommand(LoadAllItemsAsync);
            FilterItemsCommand = new RelayCommand(FilterItem);
            CreateItemCommand = new RelayCommand(CreateItemAsync);
            LoadManageItemViewCommand = new RelayCommand(LoadManageItemView);
        }

        private async void LoadAllItemsAsync()
        {
            items = (await _apiService.GetAllItemsAsync()).ToList();
            var filteredItemDtos = items.Where(i => i.ItemId.Contains(ItemIdFilter));
            var filteredItems = _mapper.Map<IEnumerable<ItemDto>, IEnumerable<ItemViewModel>>(filteredItemDtos);

            Items = new ObservableCollection<ItemViewModel>(filteredItems);
        }

        private void FilterItem()
        {
            var filteredItemDtos = items.Where(i => i.ItemId.Contains(ItemIdFilter));
            var filteredItems = _mapper.Map<IEnumerable<ItemDto>, IEnumerable<ItemViewModel>>(filteredItemDtos);

            Items = new ObservableCollection<ItemViewModel>(filteredItems);
        }
        private void LoadManageItemView()
        {
            _databaseSynchronizationService.SynchronizeItemsData();
            OnPropertyChanged(nameof(WarehouseIds));
            OnPropertyChanged(nameof(Units));
            LoadAllItemsAsync();
        }

        private async void CreateItemAsync()
        {
            var createItemDto = new CreateItemDto(
                ItemId,
                ItemName,
                MinimumStockLevel,
                Price,
                ItemClassId,
                Unit);
            try
            {
                await _apiService.CreateItem(createItemDto);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            catch (DuplicateEntityException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mã vật tư đã tồn tại.");
            }
            catch (Exception)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Không thể tạo vật tư mới.");
            }
            LoadManageItemView();
        }
    }
}
