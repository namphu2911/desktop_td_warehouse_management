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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm
{
    public class QuantityAlarmViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly WarehouseStore _warehouseStore;
        public string WarehouseId { get; set; } = "";
        public ObservableCollection<string> WarehouseIds => _warehouseStore.WarehouseIds;
        public ObservableCollection<EntryForQuantityAlarmViewModel> Entries { get; set; } = new();

        public ICommand LoadQuantityAlarmCommand { get; set; }
        public ICommand LoadQuantityAlarmViewCommand { get; set; }

        public QuantityAlarmViewModel(IApiService apiService, IMapper mapper, WarehouseStore warehouseStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _warehouseStore = warehouseStore;
            LoadQuantityAlarmCommand = new RelayCommand(LoadQuantityAlarmAsync);
            LoadQuantityAlarmViewCommand = new RelayCommand(LoadQuantityAlarmView);
        }
        private void LoadQuantityAlarmView()
        {
            OnPropertyChanged(nameof(WarehouseIds));
        }
        private async void LoadQuantityAlarmAsync()
        {
            try
            {
                var dtos = await _apiService.GetQuantityAlarmEntriesAsync(WarehouseId);
                var entries = _mapper.Map<IEnumerable<ItemLotDto>, IEnumerable<EntryForQuantityAlarmViewModel>>(dtos);
                Entries = new(entries);                
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }
    }
}
