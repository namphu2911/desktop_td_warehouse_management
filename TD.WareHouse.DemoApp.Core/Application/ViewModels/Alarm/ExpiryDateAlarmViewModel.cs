using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm
{
    public class ExpiryDateAlarmViewModel
    {
        public string RestDay { get; set; }

        public ICommand LoadLotInconsistenciesCommand { get; set; }

        public ExpiryDateAlarmViewModel(string restDay)
        {
            RestDay = restDay;
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
