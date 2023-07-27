using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueExternalToCreateViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public string GoodsIssueId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EmployeeId { get; set; }
        public string Receiver { get; set; }
        public List<FinishedProductIssueEntry> Entries { get; set; }

        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event EventHandler? GoodsIssueCreated;
        public event EventHandler? GoodsIssueDeleted;
        public GoodsIssueExternalToCreateViewModel(IApiService apiService, string goodsIssueId, DateTime timestamp, string employeeId, string receiver, List<FinishedProductIssueEntry> entries)
        {
            _apiService = apiService;
            GoodsIssueId = goodsIssueId;
            Timestamp = timestamp;
            EmployeeId = employeeId;
            Receiver = receiver;
            Entries = entries;

            CreateCommand = new RelayCommand(ConfirmAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
            
        }
        
        private async void ConfirmAsync()
        {
            var createDto = new CreateExternalGoodsIssueDto(
                GoodsIssueId,
                Receiver,
                EmployeeId,
                Entries.Select(x => new CreateExternalGoodsIssueEntryDto(
                    x.PurchaseOrderNumber,
                    x.Quantity,
                    x.ItemId,
                    x.Unit,
                    x.Note)).ToList());
            try
            {
                await _apiService.CreateExternalGoodsIssuesAsync(createDto);
                GoodsIssueCreated?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Đã Lưu Đơn Mới", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            catch (DuplicateEntityException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Phiếu xuất kho đã tồn tại.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: " + ex.Message);
            }
        }

        private void DeleteAsync()
        {
            if (MessageBox.Show("Xác nhận xóa đơn", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                GoodsIssueDeleted?.Invoke(this, EventArgs.Empty);
            }
            else { }
        }
    }
}
