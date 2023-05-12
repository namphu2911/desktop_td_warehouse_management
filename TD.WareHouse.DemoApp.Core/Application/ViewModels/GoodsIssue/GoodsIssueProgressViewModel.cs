using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
  
    public class GoodsIssueProgressViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private List<GoodsIssueDto> goodsIssueByIds = new();
        private List<GoodsIssueDto> goodsIssueByTimes = new();
        private List<GoodsIssueDto> goodsIssuesTotal = new();
        private PendingGoodsIssueViewModel? selectedGoodsIssue;
        //
        private readonly GoodsIssueStore _goodsIssueStore;
        public ObservableCollection<string> GoodsIssueIds => _goodsIssueStore.GoodsIssueIds;
        public string GoodsIssueId { get; set; } = "";
        //
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime EndDate { get; set; } = DateTime.Today;
        public ObservableCollection<PendingGoodsIssueViewModel> GoodsIssueByIds { get; set; } = new();
        public ObservableCollection<PendingGoodsIssueViewModel> GoodsIssueByTimes { get; set; } = new();
        public ObservableCollection<GoodsIssueLotForGoodsIssueProgressViewModel> Lots { get; set; } = new();

        public PendingGoodsIssueViewModel? SelectedGoodsIssue
        {
            get => selectedGoodsIssue;
            set
            {
                selectedGoodsIssue = value;
                if (selectedGoodsIssue is not null)
                {
                    foreach (var goodIssue in goodsIssueByIds)
                    {
                        goodsIssuesTotal.Add(goodIssue);
                    }
                    foreach (var goodIssue in goodsIssueByTimes)
                    {
                        goodsIssuesTotal.Add(goodIssue);
                    }
                    var goodsIssue = goodsIssuesTotal.First(g => g.GoodsIssueId == selectedGoodsIssue.GoodsIssueId);
                    var lotViewModels = goodsIssue.Entries.SelectMany(gi =>
                                                            gi.Lots.Select(gie =>
                                                                new GoodsIssueLotForGoodsIssueProgressViewModel(
                                                                    gi.Item.ItemClassId,
                                                                    gi.Item.ItemId,
                                                                    gi.Item.ItemName,
                                                                    gi.Item.Unit,
                                                                    gie.GoodsIssueLotId,
                                                                    gie.Quantity,
                                                                    goodsIssue.PurchaseOrderNumber)));
                    Lots = new(lotViewModels);
                }
            }
        }

        public ICommand LoadIssuedGoodsIssuesCommand { get; set; }
        public ICommand LoadIssuingGoodsIssuesCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadGoodsIssueProgressViewCommand { get; set; }
        public GoodsIssueProgressViewModel(IApiService apiService, GoodsIssueStore goodsIssueStore)
        {
            _apiService = apiService;
            _goodsIssueStore = goodsIssueStore;
            LoadIssuedGoodsIssuesCommand = new RelayCommand(LoadIssuedGoodsIssuesAsync);
            LoadIssuingGoodsIssuesCommand = new RelayCommand(LoadIssuingGoodsIssuesAsync);

            ConfirmCommand = new RelayCommand(ConfirmAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
            LoadGoodsIssueProgressViewCommand = new RelayCommand(LoadGoodsIssueProgressView);
        }
        private void LoadGoodsIssueProgressView()
        {
            OnPropertyChanged(nameof(GoodsIssueIds));
        }
        public bool EnableConfirmButton { get; set; }
        public async void LoadIssuedGoodsIssuesAsync()
        {
            EnableConfirmButton = false;
            var goodsIssueByTime = await _apiService.GetIssuedGoodsIssuesAsync(StartDate, EndDate);
            goodsIssueByTimes = goodsIssueByTime.ToList();
            var goodsIssueByTimeViewModels = goodsIssueByTime.Select(g =>
                new PendingGoodsIssueViewModel(g.GoodsIssueId,
                                                 g.Timestamp,
                                                 g.Employee.EmployeeName,
                                                 g.Receiver));
            GoodsIssueByTimes = new ObservableCollection<PendingGoodsIssueViewModel>(goodsIssueByTimeViewModels);
            Lots = new();
            GoodsIssueConfirmed += LoadIssuedGoodsIssuesAsync;
            GoodsIssueDeletedConfirmed += LoadIssuedGoodsIssuesAsync;
        }

        public event Action? GoodsIssueConfirmed;
        public event Action? GoodsIssueDeletedConfirmed;
        public event Action? GoodsIssueUpdatedUnconfirmed;
        public async void LoadIssuingGoodsIssuesAsync()
        {
            EnableConfirmButton = true;
            goodsIssueByIds = new();
            var goodsIssueById = await _apiService.GetIssuingGoodsIssuesAsync(GoodsIssueId);
            goodsIssueByIds.Add(goodsIssueById);
            var goodsIssueByIdViewModels = goodsIssueByIds.Select(g =>
                  new PendingGoodsIssueViewModel(g.GoodsIssueId,
                                                 g.Timestamp,
                                                 g.Employee.EmployeeName,
                                                 g.Receiver));
            GoodsIssueByIds = new ObservableCollection<PendingGoodsIssueViewModel>(goodsIssueByIdViewModels);
            Lots = new();

        }
        public void ReloadWhenConfirm()
        {
            Lots = new();
            OnPropertyChanged(nameof(GoodsIssueIds));
            GoodsIssueId = "";
            GoodsIssueByIds = new();
            GoodsIssueConfirmed += ReloadWhenConfirm;
        }
        public void ReloadWhenDelete()
        {
            OnPropertyChanged(nameof(GoodsIssueIds));
            GoodsIssueId = "";
            GoodsIssueByIds = new();
            Lots = new();
            GoodsIssueUpdatedUnconfirmed += ReloadWhenDelete;
        }
        private async void ConfirmAsync()
        {
            try
            {
                await _apiService.ConfirmGoodsIssueAsync(SelectedGoodsIssue.GoodsIssueId);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            GoodsIssueId = "";
            GoodsIssueByIds = new();
            GoodsIssueConfirmed?.Invoke();
        }

        private async void DeleteAsync()
        {
            try
            {
                await _apiService.DeleteGoodsIssueAsync(SelectedGoodsIssue.GoodsIssueId);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            GoodsIssueDeletedConfirmed?.Invoke();
            GoodsIssueUpdatedUnconfirmed?.Invoke();

    }
    }
}
