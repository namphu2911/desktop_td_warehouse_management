using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class PendingGoodsIssueExternalViewModel : BaseViewModel
    {
        public string GoodsReceiptId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public ICommand ConfirmCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event Action? GoodsReceiptConfirmed;
        public event Action? GoodsReceiptDeleted;

        public PendingGoodsIssueExternalViewModel(/*IApiService apiService,*/ string goodsReceiptId, DateTime timestamp, string employeeId, string employeeName)
        {
            //_apiService = apiService;
            GoodsReceiptId = goodsReceiptId;
            Timestamp = timestamp;
            EmployeeId = employeeId;
            EmployeeName = employeeName;

            ConfirmCommand = new RelayCommand(ConfirmAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
        }

        private async void ConfirmAsync()
        {
            //try
            //{
            //    await _apiService.ConfirmGoodsReceiptAsync(GoodsReceiptId);
            //}
            //catch (HttpRequestException)
            //{
            //    ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            //}
            //GoodsReceiptConfirmed?.Invoke();
        }

        private async void DeleteAsync()
        {
            //try
            //{
            //    await _apiService.DeleteGoodsReceiptAsync(GoodsReceiptId);
            //}
            //catch (HttpRequestException)
            //{
            //    ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            //}
            //GoodsReceiptDeleted?.Invoke();
        }

    }
}
