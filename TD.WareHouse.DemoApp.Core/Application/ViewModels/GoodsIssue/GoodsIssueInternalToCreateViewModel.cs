using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
    public class GoodsIssueInternalToCreateViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public string GoodsIssueId { get; set; }
        public DateTime Timestamp { get; set; } 
        public string EmployeeId { get; set; }
        public string Receiver { get; set; }
        public bool IsSaved { get; set; }
        public List<GoodsIssueEntry> Entries { get; set; }
        public Visibility ButtonVisibility { get; set; } = Visibility.Visible;
        public Visibility SavedVisibility { get; set; } = Visibility.Hidden;
        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event EventHandler? GoodsIssueCreated;
        public event EventHandler? GoodsIssueDeleted;
        public GoodsIssueInternalToCreateViewModel(IApiService apiService, string goodsIssueId, DateTime timestamp, string employeeId, string receiver, bool isSaved, List<GoodsIssueEntry> entries)
        {
            _apiService = apiService;
            GoodsIssueId = goodsIssueId;
            Timestamp = timestamp;
            EmployeeId = employeeId;
            Receiver = receiver;
            IsSaved = isSaved;
            Entries = entries;

            CreateCommand = new RelayCommand(ConfirmAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);

        }

        private async void ConfirmAsync()
        {
            var createDto = new CreateInternalGoodsIssueDto(
                GoodsIssueId,
                Receiver,
                Timestamp,
                EmployeeId,
                Entries.Select(x => new CreateInternalGoodsIssueEntryDto(
                    x.ItemId,
                    x.Unit,
                    x.RequestedQuantity)).ToList());
            try
            {
                await _apiService.CreateInternalGoodsIssuesAsync(createDto);
                MessageBox.Show("Đã Lưu Đơn Mới", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                GoodsIssueCreated?.Invoke(this, EventArgs.Empty); 
                IsSaved = true;
                if (IsSaved)
                {
                    ButtonVisibility = Visibility.Hidden;
                    SavedVisibility = Visibility.Visible;
                }
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
