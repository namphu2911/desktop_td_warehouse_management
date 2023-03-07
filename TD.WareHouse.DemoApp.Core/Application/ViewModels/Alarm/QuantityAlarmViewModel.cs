using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm
{
    public class QuantityAlarmViewModel
    {
        public string WarehouseName { get; set; }
        public ICommand LoadLotInconsistenciesCommand { get; set; }
        public QuantityAlarmViewModel(string warehouseName)
        {
            WarehouseName = warehouseName;
            LoadLotInconsistenciesCommand = new RelayCommand(LoadLotInconsistencies);
        }

        private async void LoadLotInconsistencies()
        {
            try
            {
                //var lotInconsistencies = await _apiService.GetLotInconsistenciesAsync(StartDate, EndDate);

                //var viewModels = _mapper.Map<IEnumerable<LotInconsistencyDto>, IEnumerable<HistoryLotInconsisencyViewModel>>(lotInconsistencies);
                //LotInconsistencies = new(viewModels);
            }
            catch (HttpRequestException)
            {
                //ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }
    }
}
