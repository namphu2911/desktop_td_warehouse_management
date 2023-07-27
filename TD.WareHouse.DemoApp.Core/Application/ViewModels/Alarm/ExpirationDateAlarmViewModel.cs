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
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm
{
    public class ExpirationDateAlarmViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;

        public double TimeLeft { get; set; } 

        public ObservableCollection<EntryForAlarmViewModel> Entries { get; set; } = new();

        public ICommand LoadExpirationDateAlarmCommand { get; set; }

        public ExpirationDateAlarmViewModel(IApiService apiService, IMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;
            LoadExpirationDateAlarmCommand = new RelayCommand(LoadExpirationDateAlarmAsync);
        }

        private async void LoadExpirationDateAlarmAsync()
        {
            try
            {
                var dtos = await _apiService.GetExpirationDateAlarmEntriesAsync(TimeLeft);
                var entries = dtos.Select(i => new EntryForAlarmViewModel(
                    i.Item.ItemId,
                    i.Item.ItemName,
                    i.Item.Unit,
                    i.LotId,
                    i.Quantity,
                    i.Item.MinimumStockLevel,
                    i.ItemLotLocations.Select(x => new LocationsForAlarmViewModel(
                        x.LocationId,
                        x.QuantityPerLocation)).ToList(),
                    i.Item.ItemClassId,
                    i.ProductionDate,
                    i.ExpirationDate));
                Entries = new(entries);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }
    }
}
