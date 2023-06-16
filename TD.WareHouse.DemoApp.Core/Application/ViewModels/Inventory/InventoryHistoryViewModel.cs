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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.LotAdjustment;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory
{
    public class InventoryHistoryViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-30).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(+1).Date;

        public ObservableCollection<ConfirmedLotAdjustmentViewModel> LotAdjustments { get; set; } = new();

        public ICommand LoadLotAdjustmentsCommand { get; set; }

        public InventoryHistoryViewModel(IApiService apiService, IMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;

            LoadLotAdjustmentsCommand = new RelayCommand(LoadLotAdjustmentsAsync);
        }

        private async void LoadLotAdjustmentsAsync()
        {
            try
            {
                var lotAdjustments = await _apiService.GetConfirmedLotAdjustmentsAsync(StartDate, EndDate);
                var viewModels = _mapper.Map<IEnumerable<LotAdjustmentDto>, IEnumerable<ConfirmedLotAdjustmentViewModel>>(lotAdjustments);

                LotAdjustments = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }
    }
}
