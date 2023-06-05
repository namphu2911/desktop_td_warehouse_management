using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueInternalToCreateViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public string GoodsIssueId { get; set; }
        public DateTime Timestamp { get; set; } 
        public string EmployeeId { get; set; }
        public string Receiver { get; set; }
        public List<GoodsIssueEntry> Entries { get; set; }

        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event EventHandler? GoodsIssueCreated;
        public event EventHandler? GoodsIssueDeleted;
        public string SaveStatusString { get; set; } = "Lưu";
        public bool SaveStatusBool { get; set; }
        public GoodsIssueInternalToCreateViewModel(IApiService apiService, string goodsIssueId, DateTime timestamp, string employeeId, string receiver, List<GoodsIssueEntry> entries)
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
            var createDto = new CreateGoodsIssueDto(
                GoodsIssueId,
                Receiver,
                purchaseOrderNumber:null,
                Timestamp,
                EmployeeId,
                Entries.Select(x => new CreateGoodsIssueEntryDto(
                    x.ItemId,
                    x.Unit,
                    requestedSublotSize:null,
                    x.RequestedQuantity)).ToList());
            try
            {
                await _apiService.CreateGoodsIssuesAsync(createDto);
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
            SaveStatusString = "Đã lưu";
            SaveStatusBool = false;
            GoodsIssueCreated?.Invoke(this, EventArgs.Empty);
        }

        private void DeleteAsync()
        {
            GoodsIssueDeleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
