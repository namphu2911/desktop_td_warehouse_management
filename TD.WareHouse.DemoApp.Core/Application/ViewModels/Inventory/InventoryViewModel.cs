using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.LotAdjustment;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory
{
    public class InventoryViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;

        public ObservableCollection<FixLotAdjustmentViewModel> LotAdjustments { get; set; } = new();

        public ICommand LoadLotAdjustmentsCommand { get; set; }

        public InventoryViewModel(IApiService apiService, IMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;

            LoadLotAdjustmentsCommand = new RelayCommand(LoadLotAdjustmentsAsync);
        }

        private async void LoadLotAdjustmentsAsync()
        {
            try
            {
                var lotAdjustments = await _apiService.GetUnfixedLotAdjustmentsAsync();
                var viewModels = _mapper.Map<IEnumerable<LotAdjustmentDto>, IEnumerable<FixLotAdjustmentViewModel>>(lotAdjustments);

                LotAdjustments = new(viewModels);
                foreach (var viewModel in LotAdjustments)
                {
                    viewModel.OnRemoved += LoadLotAdjustmentsAsync;
                    viewModel.SetApiService(_apiService);
                    viewModel.OnException += Error;
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }
        private void Error()
        {
            ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
        }
    }
}
