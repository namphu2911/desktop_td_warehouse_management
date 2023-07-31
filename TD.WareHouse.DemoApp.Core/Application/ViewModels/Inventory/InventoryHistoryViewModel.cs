using AutoMapper;
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
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.LotAdjustment;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory
{
    public class InventoryHistoryViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly IExcelExporter _excelExporter;
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;

        public ObservableCollection<ConfirmedLotAdjustmentViewModel> LotAdjustments { get; set; } = new();

        public ICommand LoadLotAdjustmentsCommand { get; set; }
        public ICommand ExportToExcelCommand { get; set; }
        public InventoryHistoryViewModel(IApiService apiService, IMapper mapper, IExcelExporter excelExporter)
        {
            _apiService = apiService;
            _mapper = mapper;
            _excelExporter = excelExporter;

            LoadLotAdjustmentsCommand = new RelayCommand(LoadLotAdjustmentsAsync);
            ExportToExcelCommand = new RelayCommand<string>(ExportToExcel);
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

        private void ExportToExcel(string? filePath)
        {
            if (filePath is not null)
            {
                _excelExporter.ExportReport(filePath, LotAdjustments);
            }
            MessageBox.Show("Đã Xuất File Excel", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
