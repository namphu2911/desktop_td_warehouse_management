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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Location;
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
        public ObservableCollection<EntryForAlarmViewModel> Entries { get; set; } = new();

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
                foreach (var dto in dtos)
                {
                    if (dto.ItemLotLocations.Count() == 0)
                    {
                        dto.ItemLotLocations.Add(new ItemLotLocationDto("", null));
                    }
                }
                var viewModels = dtos.SelectMany(i => i.ItemLotLocations.Select(x => new EntryForAlarmViewModel(
                    i.Item.ItemId,
                    i.Item.ItemName,
                    i.Item.Unit,
                    i.LotId,
                    i.Quantity,
                    i.Item.MinimumStockLevel,
                    i.Item.ItemClassId,
                    i.ProductionDate,
                    i.ExpirationDate,
                    x.LocationId,
                    x.QuantityPerLocation))).ToList();
                if (viewModels != null)
                {
                    for (int i = 0; i < viewModels.Count - 1; i++)
                    {
                        if (viewModels[i + 1].LotId == viewModels[i].LotId)
                        {
                            viewModels[i + 1].ItemId = "";
                            viewModels[i + 1].ItemName = "";
                            viewModels[i + 1].Unit = "";
                            viewModels[i + 1].LotId = "";
                            viewModels[i + 1].Quantity = null;
                            viewModels[i + 1].MinimumStockLevel = null;
                            viewModels[i + 1].ItemClassId = "";
                            viewModels[i + 1].IsQuantityAlarmed = viewModels[i].IsQuantityAlarmed;
                        }
                    }
                    Entries = new(viewModels);
                }
                //var entries = dtos.Select(i => new EntryForAlarmViewModel(
                //    i.Item.ItemId,
                //    i.Item.ItemName,
                //    i.Item.Unit,
                //    i.LotId,
                //    i.Quantity,
                //    i.Item.MinimumStockLevel,
                //    i.ItemLotLocations.Select(x => new LocationsForAlarmViewModel(
                //        x.LocationId,
                //        x.QuantityPerLocation)).ToList(),
                //    i.Item.ItemClassId,
                //    i.ProductionDate,
                //    i.ExpirationDate));
                //Entries = new(entries);                
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }
    }
}
