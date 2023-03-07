using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Isolation
{
    public class IsolationViewModel : BaseViewModel
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public ICommand LoadIsolationCommand { get; set; }
        public IsolationViewModel(string itemId, string itemName)
        {
            ItemId = itemId;
            ItemName = itemName;
            LoadIsolationCommand = new RelayCommand(LoadIsolation);
        }
        private async void LoadIsolation()
        {
            try
            {
                //var lotInconsistencies = await _apiService.GetLotInconsistenciesAsync(StartDate, EndDate);

                //var viewModels = _mapper.Map<IEnumerable<LotInconsistencyDto>, IEnumerable<HistoryLotInconsisencyViewModel>>(lotInconsistencies);
                //LotInconsistencies = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

    }
}
