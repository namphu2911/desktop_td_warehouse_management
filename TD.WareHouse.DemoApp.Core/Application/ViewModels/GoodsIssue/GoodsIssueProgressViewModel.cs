using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
  
    public class GoodsIssueProgressViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private List<GoodsIssueDto> goodsIssues = new();
        private PendingGoodsIssueViewModel? selectedGoodsIssue;
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime EndDate { get; set; } = DateTime.Today;
        public string GoodsIssueId { get; set; } = "";
        public ObservableCollection<PendingGoodsIssueViewModel> GoodsIssues { get; set; } = new();
        public ObservableCollection<GoodsIssueEntryForGoodsIssueProgressView> Entries { get; set; } = new();


        public PendingGoodsIssueViewModel? SelectedGoodsIssue
        {
            get => selectedGoodsIssue;
            set
            {
                selectedGoodsIssue = value;
                if (selectedGoodsIssue is not null)
                {
                    var goodsIssue = goodsIssues.First(g => g.GoodsIssueId == selectedGoodsIssue.GoodsIssueId);
                    var containerViewModels = goodsIssue.Entries.Select(c => new GoodsIssueEntryForGoodsIssueProgressView(
                    _apiService,
                    c.Item.ItemClass.ItemClassId,
                    c.Item.ItemId,
                    c.Item.ItemName,
                    c.Item.Unit,
                    c.Lots.Select(e => new GoodsIssueEntryLotForGoodsIssueProgressView(
                        e.GoodsIssueLotId)).ToList(),
                    c.RequestedQuantity,
                    goodsIssue.PurchaseOrderNumber,
                    goodsIssue.Receiver,
                    goodsIssue.Employee.EmployeeName));

                    Entries = new(containerViewModels);

                }
            }
        }

        public ICommand LoadIssuedGoodsIssuesCommand { get; set; }
        public ICommand LoadIssuingGoodsIssuesCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public GoodsIssueProgressViewModel(IApiService apiService)
        {
            _apiService = apiService;
            LoadIssuedGoodsIssuesCommand = new RelayCommand(LoadIssuedGoodsIssuesAsync);
            LoadIssuingGoodsIssuesCommand = new RelayCommand(LoadIssuingGoodsIssuesAsync);
            ConfirmCommand = new RelayCommand(ConfirmAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
        }

        public async void LoadIssuedGoodsIssuesAsync()
        {
            var queryResult = (await _apiService.GetIssuedGoodsIssuesAsync(StartDate, EndDate));
            goodsIssues = queryResult.Items.ToList();

            var goodsIssueViewModels = goodsIssues.Select(g =>
                new PendingGoodsIssueViewModel(_apiService,
                                                 g.GoodsIssueId,
                                                 g.Timestamp,
                                                 g.Employee.EmployeeName,
                                                 g.Receiver));
            GoodsIssues = new ObservableCollection<PendingGoodsIssueViewModel>(goodsIssueViewModels);

            Entries = new();

        }

        public async void LoadIssuingGoodsIssuesAsync()
        {
            var queryResult = (await _apiService.GetIssuingGoodsIssuesAsync(GoodsIssueId));
            goodsIssues = queryResult.Items.ToList();

            var goodsIssueViewModels = goodsIssues.Select(g =>
                new PendingGoodsIssueViewModel(_apiService,
                                                 g.GoodsIssueId,
                                                 g.Timestamp,
                                                 g.Employee.EmployeeName,
                                                 g.Receiver));
            GoodsIssues = new ObservableCollection<PendingGoodsIssueViewModel>(goodsIssueViewModels);

            Entries = new();

        }

        private async void ConfirmAsync()
        {
            try
            {
                await _apiService.ConfirmGoodsIssueAsync(GoodsIssueId);
                LoadIssuedGoodsIssuesAsync();
                LoadIssuingGoodsIssuesAsync();
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }

        }

        private async void DeleteAsync()
        {
            try
            {
                await _apiService.DeleteGoodsIssueAsync(GoodsIssueId);
                LoadIssuedGoodsIssuesAsync();
                LoadIssuingGoodsIssuesAsync();
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }

        }
    }
}
