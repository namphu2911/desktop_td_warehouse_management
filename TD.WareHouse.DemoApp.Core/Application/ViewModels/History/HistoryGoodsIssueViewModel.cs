using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.History
{
    public class HistoryGoodsIssueViewModel : BaseViewModel
    {
        public string WarehouseName { get; set; }  
        public string ItemId { get; set; }
        public string ItemName { get; set; }    
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-30).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public string DepartmentName { get; set; }
        public string? PurchaseOrderNumber { get; set; }


        public ICommand LoadLotInconsistenciesCommand { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public HistoryGoodsIssueViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
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
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

       
    }
}
