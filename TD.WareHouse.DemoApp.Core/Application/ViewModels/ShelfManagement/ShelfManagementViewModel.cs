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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Location;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.ShelfManagement
{
    public class ShelfManagementViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        private readonly LocationStore _locationStore;
        private string itemId = "";
        private string itemName = "";
        public string LocationId { get; set; } = "";

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
        
        public ObservableCollection<ItemEntryForShelfManagementViewModel> ItemEntries { get; set; } = new();
        public ObservableCollection<LocationEntryForShelfManagementViewModel> LocationEntries { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.ItemIds;
        public ObservableCollection<string> ItemNames => _itemStore.ItemNames;
        public ObservableCollection<string> LocationIds => _locationStore.LocationIds;

        public ICommand LoadItemEntryCommand { get; set; }
        public ICommand LoadLocationEntryCommand { get; set; }
        public ShelfManagementViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore, LocationStore locationStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
            _locationStore = locationStore;
            LoadItemEntryCommand = new RelayCommand(LoadItemEntryEntry);
            LoadLocationEntryCommand = new RelayCommand(LoadLocationEntry);
        }

        private async void LoadItemEntryEntry()
        {
            try
            {
                var itemEntries = await _apiService.GetItemShelfManagementEntriesAsync(ItemId, ItemName);

                var viewModels = _mapper.Map<IEnumerable<LocationDto>, IEnumerable<ItemEntryForShelfManagementViewModel>>(itemEntries);
                ItemEntries = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }
        private async void LoadLocationEntry()
        {
            try
            {
                var locationEntries = await _apiService.GetLocationShelfManagementEntriesAsync(LocationId);

                var viewModels = _mapper.Map<IEnumerable<LocationDto>, IEnumerable<LocationEntryForShelfManagementViewModel>>(locationEntries);
                LocationEntries = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }


    }
}

