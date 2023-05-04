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
        public DateTime Date { get; set; } = DateTime.Now;
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Receiver { get; set; }

        public List<GoodsIssueEntryDto> Entries { get; set; }

        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event EventHandler? GoodsIssueCreated;
        public event EventHandler? GoodsIssueDeleted;

        public GoodsIssueInternalToCreateViewModel(IApiService apiService, string goodsIssueId, string employeeId, string employeeName, string receiver, List<GoodsIssueEntryDto> entries)
        {
            _apiService = apiService;
            GoodsIssueId = goodsIssueId;
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            Receiver = receiver;
            Entries = entries;

            CreateCommand = new RelayCommand(ConfirmAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);

        }

        private async void ConfirmAsync()
        {
            var createDto = new CreateGoodsIssueDto(
                GoodsIssueId,
                Date,
                EmployeeId,
                Receiver,
                Entries.Select(x => new CreateGoodsIssueEntryDto(
                    x.Item.ItemId,
                    x.RequestedQuantity)).ToList());
            try
            {
                await _apiService.CreateGoodsIssuesAsync(new List<CreateGoodsIssueDto>() { createDto });
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

            GoodsIssueCreated?.Invoke(this, EventArgs.Empty);
        }

        private void DeleteAsync()
        {
            GoodsIssueDeleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
