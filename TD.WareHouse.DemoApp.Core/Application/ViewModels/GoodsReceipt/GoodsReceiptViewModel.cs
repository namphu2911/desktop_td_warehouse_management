using System;
using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using CommunityToolkit.Mvvm.Input;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using System.Net.Http;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
        private List<GoodsReceiptDto> goodsReceipts = new();
        private List<GoodsReceiptDto> goodsReceiptByIds = new();
        private List<GoodsReceiptDto> goodsReceiptByTimes = new();
        private PendingGoodsReceiptViewModel? selectedGoodsReceipt;
        //
        private readonly WarehouseStore _warehouseStore;
        public ObservableCollection<string> LocationIds => _warehouseStore.LocationIds;
        //
        private readonly GoodsReceiptStore _goodsReceiptStore;
        public ObservableCollection<string> GoodsReceiptIds => _goodsReceiptStore.GoodsReceiptIds;
        public string GoodsReceiptId { get; set; } = "";
        //
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(+1);
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceiptByIds { get; set; } = new();
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceiptByTimes { get; set; } = new();
        public ObservableCollection<GoodsReceiptLotForGoodsReceiptView> Lots { get; set; } = new();

        public PendingGoodsReceiptViewModel? SelectedGoodsReceipt
        {
            get => selectedGoodsReceipt;
            set
            {
                selectedGoodsReceipt = value;
                if (selectedGoodsReceipt is not null)
                {
                    foreach (var goodReceipt in goodsReceiptByIds)
                    {
                        goodsReceipts.Add(goodReceipt);
                    }
                    foreach (var goodReceipt in goodsReceiptByTimes)
                    {
                        goodsReceipts.Add(goodReceipt);
                    }
                    var goodsReceipt = goodsReceipts.Last(g => g.GoodsReceiptId == selectedGoodsReceipt.GoodsReceiptId);
                    var lotViewModels = goodsReceipt.Lots.Select(c => new GoodsReceiptLotForGoodsReceiptView(
                    this.LocationIds,
                    c.Item.ItemClassId,
                    c.Item.ItemId,
                    c.Item.ItemName,
                    c.GoodsReceiptLotId,
                    c.Item.Unit,
                    c.Quantity,
                    c.PurchaseOrderNumber,
                    c.ProductionDate,
                    c.ExpirationDate,
                    c.LocationId));

                    Lots = new(lotViewModels);
                }
            }
        }
        public ICommand LoadReceivingGoodsReceiptsCommand { get; set; }
        public ICommand LoadReceivedGoodsReceiptsCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand LoadGoodsReceiptViewCommand { get; set; }
        public GoodsReceiptViewModel(IApiService apiService, IDatabaseSynchronizationService databaseSynchronizationService, GoodsReceiptStore goodsReceiptStore, WarehouseStore warehouseStore)
        {
            _apiService = apiService;
            _databaseSynchronizationService = databaseSynchronizationService;
            _goodsReceiptStore = goodsReceiptStore;
            _warehouseStore = warehouseStore;
            LoadReceivingGoodsReceiptsCommand = new RelayCommand(LoadReceivingGoodsReceiptsAsync);
            LoadReceivedGoodsReceiptsCommand = new RelayCommand(LoadReceivedGoodsReceiptsAsync);

            UpdateCommand = new RelayCommand(UpdateAsync);
            LoadGoodsReceiptViewCommand = new RelayCommand(LoadGoodsReceiptView);
            //DeleteCommand = new RelayCommand(DeleteAsync);
            //ConfirmCommand = new RelayCommand(ConfirmAsync);
        }

        private void LoadGoodsReceiptView()
        {
            _databaseSynchronizationService.SynchronizeGoodReceiptsData();
            OnPropertyChanged(nameof(GoodsReceiptIds));
        }

        public async void LoadReceivedGoodsReceiptsAsync()
        {
            try
            {
                var goodsReceiptByTime = await _apiService.GetReceivedGoodsReceiptsAsync(StartDate, EndDate);
                goodsReceiptByTimes = goodsReceiptByTime.ToList();
                var goodsReceiptByTimeViewModels = goodsReceiptByTime.Select(g =>
                    new PendingGoodsReceiptViewModel(
                                                     g.GoodsReceiptId,
                                                     g.Timestamp,
                                                     g.Employee.EmployeeName));
                GoodsReceiptByTimes = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptByTimeViewModels);
                Lots = new();
                //GoodsReceiptReceivedUpdated += LoadReceivedGoodsReceiptsAsync;
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        public async void LoadReceivingGoodsReceiptsAsync()
        {
            try
            {
                goodsReceiptByIds = new();
                var goodsReceiptById = await _apiService.GetReceivingGoodsReceiptsAsync(GoodsReceiptId);
                goodsReceiptByIds.Add(goodsReceiptById);
                var goodsReceiptByIdViewModels = goodsReceiptByIds.Select(g =>
                    new PendingGoodsReceiptViewModel(g.GoodsReceiptId,
                                                     g.Timestamp,
                                                     g.Employee.EmployeeName));
                GoodsReceiptByIds = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptByIdViewModels);
                Lots = new();
                //GoodsReceiptReceivingUpdated += LoadReceivingGoodsReceiptsAsync;
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        //public event Action? GoodsReceiptReceivingUpdated;
        //public event Action? GoodsReceiptReceivedUpdated;

        public async void UpdateAsync()
        {
            var fixDto = Lots.Select(x => new FixCompletedGoodsReceiptDto(
                x.LotId,
                x.LocationId,
                x.Quantity,
                x.PurchaseOrderNumber,
                x.ProductionDate,
                x.ExpirationDate));
            try
            { 
                if (SelectedGoodsReceipt is not null)
                {
                    await _apiService.FixCompltedGoodsReceiptAsync(SelectedGoodsReceipt.GoodsReceiptId, fixDto);
                    Lots = new();
                    LoadGoodsReceiptView();
                    if (GoodsReceiptId == SelectedGoodsReceipt.GoodsReceiptId)
                    {
                        GoodsReceiptByIds = new();
                        LoadReceivingGoodsReceiptsAsync();
                        GoodsReceiptId = "";
                    }
                    else
                    {
                        GoodsReceiptByTimes = new();
                        LoadReceivedGoodsReceiptsAsync();
                        //GoodsReceiptReceivedUpdated?.Invoke();
                    }
                    MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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

        //public event Action? GoodsReceiptConfirmed;
        //public event Action? GoodsReceiptDeleted;

        //public ICommand DeleteCommand { get; set; }
        //public ICommand ConfirmCommand { get; set; }

        //public void ReloadWhenConfirm()
        //{
        //    Lots = new();
        //    OnPropertyChanged(nameof(GoodsReceiptIds));
        //    GoodsReceiptId = "";
        //    GoodsReceiptByIds = new();
        //    GoodsReceiptByTimes = new();
        //    GoodsReceiptConfirmed += ReloadWhenConfirm;
        //}

        //public void ReloadWhenDelete()
        //{
        //    Lots = new();
        //    GoodsReceiptsByIds = new();
        //    OnPropertyChanged(nameof(GoodsReceiptIds));
        //    GoodsReceiptId = "";
        //}

        //private async void ConfirmAsync()
        //{
        //    try
        //    {
        //        if (SelectedGoodsReceipt is not null)
        //        {
        //            await _apiService.ConfirmGoodsReceiptAsync(SelectedGoodsReceipt.GoodsReceiptId);
        //            MessageBox.Show("Đã Duyệt Đơn", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //    }
        //    catch (HttpRequestException)
        //    {
        //        ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
        //    }
        //    GoodsReceiptConfirmed?.Invoke();
        //    GoodsReceiptId = "";
        //    GoodsReceiptByIds = new();
        //}

        //private async void DeleteAsync()
        //{
        //    if (MessageBox.Show("Xác nhận xóa đơn", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //    {
        //        try
        //        {
        //            if (SelectedGoodsReceipt is not null)
        //            {
        //                await _apiService.DeleteGoodsReceiptAsync(SelectedGoodsReceipt.GoodsReceiptId);
        //            }
        //        }
        //        catch (HttpRequestException)
        //        {
        //            ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
        //        }
        //        ReloadWhenDelete();
        //    }
        //    else { }
        //}
    }
}
