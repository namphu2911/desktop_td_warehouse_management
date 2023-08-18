using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.MiscellaneousData
{
    public class ManageItemViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly IExcelReader _excelReader;
        private readonly ItemStore _itemStore;
        private readonly WarehouseStore _warehouseStore;
        public ObservableCollection<string> Units => _itemStore.AllUnits; 
        public ObservableCollection<string> ItemIds => _itemStore.AllItemIds;
        public ObservableCollection<string> WarehouseIds => _warehouseStore.AllWarehouseIds;
        IDatabaseSynchronizationService _databaseSynchronizationService;

        public string ItemClassId { get; set; } = "";
        public string ItemId { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string Unit { get; set; } = "";
        public double MinimumStockLevel { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public double? PacketSize { get; set; }
        public string? PacketUnit { get; set; }
        public string ItemIdFilter { get; set; } = "";
        private List<ItemDto> items = new();
        public ObservableCollection<ItemViewModel> Items { get; set; } = new();
        public ICommand LoadAllItemsCommand { get; set; }
        public ICommand FilterItemsCommand { get; set; }
        public ICommand CreateItemCommand { get; set; }
        public ICommand ImportItemsCommand { get; set; }
        public ICommand LoadManageItemViewCommand { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string FilePath { get; set; } = "";
        public ManageItemViewModel(IApiService apiService, IMapper mapper, IExcelReader excelReader, ItemStore itemStore, WarehouseStore warehouseStore, IDatabaseSynchronizationService databaseSynchronizationService)
        {
            _apiService = apiService;
            _mapper = mapper;
            _excelReader = excelReader;
            _itemStore = itemStore;
            _warehouseStore = warehouseStore;
            _databaseSynchronizationService = databaseSynchronizationService;

            LoadAllItemsCommand = new RelayCommand(LoadAllItemsAsync);
            FilterItemsCommand = new RelayCommand(FilterItem);
            CreateItemCommand = new RelayCommand(CreateItemAsync);
            LoadManageItemViewCommand = new RelayCommand(LoadManageItemView);
            ImportItemsCommand = new RelayCommand(ImportItems);
        }

        private async void LoadAllItemsAsync()
        {
            items = (await _apiService.GetAllItemsAsync()).ToList();
            var filteredItemDtos = items.Where(i => i.ItemId.Contains(ItemIdFilter));
            var filteredItems = _mapper.Map<IEnumerable<ItemDto>, IEnumerable<ItemViewModel>>(filteredItemDtos);

            Items = new ObservableCollection<ItemViewModel>(filteredItems);
            foreach (var item in Items)
            {
                item.SetApiService(_apiService);
                item.SetMapper(_mapper);
                item.Updated += LoadAllItemsAsync;
            }
        }

        private void FilterItem()
        {
            var filteredItemDtos = items.Where(i => i.ItemId.Contains(ItemIdFilter));
            var filteredItems = _mapper.Map<IEnumerable<ItemDto>, IEnumerable<ItemViewModel>>(filteredItemDtos);

            Items = new ObservableCollection<ItemViewModel>(filteredItems);
            foreach (var item in Items)
            {
                item.SetApiService(_apiService);
                item.SetMapper(_mapper);
                item.Updated += LoadAllItemsAsync;
            }
        }
        private void LoadManageItemView()
        {
            _databaseSynchronizationService.SynchronizeItemsData();
            LoadAllItemsAsync();
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(WarehouseIds));
            OnPropertyChanged(nameof(Units));
            OnPropertyChanged(nameof(Items));
        }

        private async void CreateItemAsync()
        {
            var createItemDto = new CreateItemDto(
                ItemId,
                ItemName,
                MinimumStockLevel,
                Price,
                ItemClassId,
                Unit,
                PacketSize,
                PacketUnit);
            try
            {
                await _apiService.CreateItem(createItemDto);
                LoadManageItemView();
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
            MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            ItemClassId = "";
            ItemId = "";
            ItemName = "";
            Unit = "";
            MinimumStockLevel = 0;
            Price  = 0;
            PacketSize = null;
            PacketUnit = null;
            LoadManageItemView();
        }

        private async void ImportItems()
        {
            try
            {
                var request = _excelReader.ReadItemExportRequests(FilePath, "Data", Date);
                await _apiService.CreateItemFromExcel(request);
                MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadManageItemView();
            }
            catch (IOException)
            {
                ShowErrorMessage($"Vui lòng tắt file trước khi nhập vào phần mềm.");
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
