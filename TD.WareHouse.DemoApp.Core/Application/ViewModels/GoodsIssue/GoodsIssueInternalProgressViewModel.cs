using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
  
    public class GoodsIssueInternalProgressViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
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
        public ObservableCollection<GoodsIssueEntryForGoodsIssueInternalProgressViewModel> Entries { get; set; } = new();

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
                    GoodsIssueDto goodsIssue = goodsIssuesTotal.Last(g => g.GoodsIssueId == selectedGoodsIssue.GoodsIssueId);

                    var entries = goodsIssue.Entries.Select(e =>
                    new GoodsIssueEntryForGoodsIssueInternalProgressViewModel(
                        e.Item.ItemClassId,
                        e.Item.ItemId,
                        e.Item.ItemName,
                        e.Item.Unit,
                        e.RequestedQuantity,
                        e.Lots.Select(c => new GoodsIssueLotForGoodsIssueInternalProgressViewModel(
                            c.GoodsIssueLotId,
                            c.Quantity))
                            .ToList()));

                    Entries = new(entries);
                    
                    //var lotViewModels = entries.SelectMany(gi =>
                    //                                        gi.Lots.Select(gie =>
                    //                                            new GoodsIssueLotForGoodsIssueInternalProgressViewModel(
                    //                                                gi.Item.ItemClassId,
                    //                                                gi.Item.ItemId,
                    //                                                gi.Item.ItemName,
                    //                                                gi.Item.Unit,
                    //                                                gie.GoodsIssueLotId,
                    //                                                gie.Quantity,
                    //                                                goodsIssue.PurchaseOrderNumber)));
                    //Lots = new(lotViewModels);

                }
            }
        }

        public ICommand LoadIssuedGoodsIssuesCommand { get; set; }
        public ICommand LoadIssuingGoodsIssuesCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand LoadGoodsIssueProgressViewCommand { get; set; }
        public GoodsIssueInternalProgressViewModel(IApiService apiService, IDatabaseSynchronizationService databaseSynchronizationService, GoodsIssueStore goodsIssueStore)
        {
            _apiService = apiService;
            _databaseSynchronizationService = databaseSynchronizationService;
            _goodsIssueStore = goodsIssueStore;
            LoadIssuedGoodsIssuesCommand = new RelayCommand(LoadIssuedGoodsIssuesAsync);
            LoadIssuingGoodsIssuesCommand = new RelayCommand(LoadIssuingGoodsIssuesAsync);

            UpdateCommand = new RelayCommand(UpdateAsync);
            LoadGoodsIssueProgressViewCommand = new RelayCommand(LoadGoodsIssueProgressView);
        }
        private void LoadGoodsIssueProgressView()
        {
            _databaseSynchronizationService.SynchronizeGoodIssuesData();
            OnPropertyChanged(nameof(GoodsIssueIds));
        }
        public bool EnableConfirmButton { get; set; }
        public async void LoadIssuedGoodsIssuesAsync()
        {
            try
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
                Entries = new();
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        public async void LoadIssuingGoodsIssuesAsync()
        {
            if (!String.IsNullOrEmpty(GoodsIssueId))
            {
                try
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
                    Entries = new();
                }
                catch (HttpRequestException)
                {
                    ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
                }
            }
            else
            { }

        }
        
        public async void UpdateAsync()
        {
            if (SelectedGoodsIssue is not null)
            {
                var fixDto = Entries.Select(x => new FixGoodsIssueEntryMaterialsDto(
                x.ItemId,
                x.Unit,
                x.RequestedQuantity));

                try
                {

                    await _apiService.FixGoodsIssueEntryMaterialsAsync(SelectedGoodsIssue.GoodsIssueId, fixDto);
                    Entries = new();
                    MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
            

        }

        //public void ReloadWhenConfirm()
        //{
        //    Entries = new();
        //    OnPropertyChanged(nameof(GoodsIssueIds));
        //    GoodsIssueId = "";
        //    GoodsIssueByIds = new();
        //}
        //public void ReloadWhenDelete()
        //{
        //    OnPropertyChanged(nameof(GoodsIssueIds));
        //    GoodsIssueId = "";
        //    GoodsIssueByIds = new();
        //    Entries = new();
        //}

        //private async void ConfirmAsync()
        //{
        //    if (SelectedGoodsIssue is not null)
        //    {
        //        try
        //        {
        //            await _apiService.ConfirmGoodsIssueAsync(SelectedGoodsIssue.GoodsIssueId);
        //            MessageBox.Show("Đã Duyệt Đơn", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //        catch (HttpRequestException)
        //        {
        //            ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
        //        }
        //        GoodsIssueId = "";
        //        GoodsIssueByIds = new();
        //        GoodsIssueConfirmed?.Invoke();

        //    }
        //}

        //private async void DeleteAsync()
        //{
        //    if (SelectedGoodsIssue is not null)
        //    {
        //        try
        //        {
        //            await _apiService.DeleteGoodsIssueAsync(SelectedGoodsIssue.GoodsIssueId);
        //        }
        //        catch (HttpRequestException)
        //        {
        //            ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
        //        }
        //        GoodsIssueDeletedConfirmed?.Invoke();
        //        GoodsIssueUpdatedUnconfirmed?.Invoke();
        //    }
        //}
    }
}
