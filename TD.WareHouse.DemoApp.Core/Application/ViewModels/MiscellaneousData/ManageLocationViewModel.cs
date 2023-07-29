using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Location;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Warehouse;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.MiscellaneousData
{
    public class ManageLocationViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly WarehouseStore _warehouseStore;
        public ObservableCollection<string> LocationIds => _warehouseStore.LocationIds;
        public ObservableCollection<string> WarehouseIds => _warehouseStore.WarehouseIds;
        IDatabaseSynchronizationService _databaseSynchronizationService;

        public string WarehouseId { get; set; } = "";
        public string LocationId { get; set; } = "";

        public string LocationIdFilter { get; set; } = "";

        //
        private List<WarehouseDto> warehouses = new();
        public ObservableCollection<LocationViewModel> Locations { get; set; } = new();
        public ICommand LoadAllLocationsCommand { get; set; }
        public ICommand FilterLocationsCommand { get; set; }
        public ICommand CreateLocationCommand { get; set; }
        public ICommand LoadManageLocationViewCommand { get; set; }
        
        public ManageLocationViewModel(IApiService apiService, WarehouseStore warehouseStore, IDatabaseSynchronizationService databaseSynchronizationService)
        {
            _apiService = apiService;
            _warehouseStore = warehouseStore;
            _databaseSynchronizationService = databaseSynchronizationService;

            LoadAllLocationsCommand = new RelayCommand(LoadAllLocationsAsync);
            FilterLocationsCommand = new RelayCommand(FilterLocation);
            CreateLocationCommand = new RelayCommand(CreateLocationAsync);
            LoadManageLocationViewCommand = new RelayCommand(LoadManageLocationView);
        }

        private async void LoadAllLocationsAsync()
        {
            warehouses = (await _apiService.GetAllWarehousesAsync()).ToList();
            var filteredLocationDtos = warehouses.SelectMany(i => i.Locations.Select(g => new LocationViewModel(
                i.WarehouseId,
                g.LocationId))).Where(i => i.LocationId.Contains(LocationIdFilter));
            Locations = new(filteredLocationDtos);
        }

        private void FilterLocation()
        {
            var filteredLocationDtos = warehouses.SelectMany(i => i.Locations.Select(g => new LocationViewModel(
                i.WarehouseId,
                g.LocationId))).Where(i => i.LocationId.Contains(LocationIdFilter));
            Locations = new(filteredLocationDtos);
        }
        private void LoadManageLocationView()
        {
            _databaseSynchronizationService.SynchronizeWarehousesData();
            LoadAllLocationsAsync();
            OnPropertyChanged(nameof(LocationIds));
            OnPropertyChanged(nameof(WarehouseIds));
        }

        private async void CreateLocationAsync()
        {
            var createLocationDto = new CreateLocationDto(
                WarehouseId,
                LocationId);
            try
            {
                await _apiService.CreateLocation(createLocationDto);
                LoadManageLocationView();
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
            WarehouseId = "";
            LocationId = "";
            LoadManageLocationView();
        }
    }
}
