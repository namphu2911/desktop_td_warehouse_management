using System.Collections.ObjectModel;
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
using System.Windows.Controls;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptMaterialsViewModel : BaseViewModel
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
        public ObservableCollection<string> UncompletedGoodsReceiptIds => _goodsReceiptStore.UncompletedGoodsReceiptIds;
        public ObservableCollection<string> CompletedGoodsReceiptIds => _goodsReceiptStore.CompletedGoodsReceiptIds;
        public string UncompletedGoodsReceiptId { get; set; } = "";
        public string CompletedGoodsReceiptId { get; set; } = "";
        //
        public DateTime UncompletedStartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime UncompletedEndDate { get; set; } = DateTime.Today;
        public DateTime CompletedStartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime CompletedEndDate { get; set; } = DateTime.Today;
        //
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceipts { get; set; } = new();
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceiptByIds { get; set; } = new();
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceiptByTimes { get; set; } = new();
        public ObservableCollection<GoodsReceiptLotForGoodsReceiptMaterialsView> Lots { get; set; } = new();
        
        public GoodsReceiptLotForGoodsReceiptMaterialsView? SelectedLot { get; set; }
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
                    var lotViewModels = goodsReceipt.Lots.Select(c => new GoodsReceiptLotForGoodsReceiptMaterialsView(
                    this.LocationIds,
                    c.Item.ItemClassId,
                    c.Item.ItemId,
                    c.Item.ItemName,
                    c.GoodsReceiptLotId,
                    c.GoodsReceiptLotId,
                    c.Item.Unit,
                    c.Quantity,
                    c.ProductionDate,
                    c.ExpirationDate,
                    c.IsExported,
                    c.Sublots.Select(e => new GoodsReceiptSublotViewModel(
                            e.LocationId,
                            e.QuantityPerLocation))
                                .ToList()));

                    Lots = new(lotViewModels);
                    foreach(var lot in Lots)
                    {
                        lot.SublotCreated += CreateSublot;
                        lot.OnRemoved += DeleteLot;
                        OnPropertyChanged(nameof(Lots));
                    }
                }
            }
        }
        public ICommand LoadUncompletedGoodsReceiptsCommand { get; set; }
        public ICommand LoadCompletedGoodsReceiptsCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand LoadGoodsReceiptViewCommand { get; set; }
        public GoodsReceiptMaterialsViewModel(IApiService apiService, IDatabaseSynchronizationService databaseSynchronizationService, GoodsReceiptStore goodsReceiptStore, WarehouseStore warehouseStore)
        {
            _apiService = apiService;
            _databaseSynchronizationService = databaseSynchronizationService;
            _goodsReceiptStore = goodsReceiptStore;
            _warehouseStore = warehouseStore;
            LoadUncompletedGoodsReceiptsCommand = new RelayCommand(LoadUncompletedGoodsReceiptsAsync);
            LoadCompletedGoodsReceiptsCommand = new RelayCommand(LoadCompletedGoodsReceiptsAsync);

            UpdateCommand = new RelayCommand(UpdateAsync);
            LoadGoodsReceiptViewCommand = new RelayCommand(LoadGoodsReceiptView);
            //DeleteCommand = new RelayCommand(DeleteAsync);
            //ConfirmCommand = new RelayCommand(ConfirmAsync);
        }

        private void LoadGoodsReceiptView()
        {
            _databaseSynchronizationService.SynchronizeGoodReceiptsData();
            OnPropertyChanged(nameof(UncompletedGoodsReceiptIds));
            OnPropertyChanged(nameof(CompletedGoodsReceiptIds));
        }

        public async void LoadUncompletedGoodsReceiptsAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(UncompletedGoodsReceiptId))
                {
                    goodsReceiptByIds = new();
                    var goodsReceiptById = await _apiService.GetGoodsReceiptsByIdAsync(UncompletedGoodsReceiptId);
                    goodsReceiptByIds.Add(goodsReceiptById);
                    var goodsReceiptByIdViewModels = goodsReceiptByIds.Select(g =>
                        new PendingGoodsReceiptViewModel(g.GoodsReceiptId,
                                                         g.Timestamp,
                                                         g.Employee.EmployeeName));
                    GoodsReceiptByIds = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptByIdViewModels);
                    GoodsReceipts = GoodsReceiptByIds;
                    Lots = new();
                    //GoodsReceiptReceivingUpdated += LoadUncompletedGoodsReceiptsAsync;
                }
                else
                {
                    var goodsReceiptByTime = await _apiService.GetGoodsReceiptsByTimeAndStateAsync(UncompletedStartDate, UncompletedEndDate, false);
                    goodsReceiptByTimes = goodsReceiptByTime.ToList();
                    var goodsReceiptByTimeViewModels = goodsReceiptByTime.Select(g =>
                        new PendingGoodsReceiptViewModel(
                                                         g.GoodsReceiptId,
                                                         g.Timestamp,
                                                         g.Employee.EmployeeName));
                    GoodsReceiptByTimes = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptByTimeViewModels);
                    GoodsReceipts = GoodsReceiptByTimes;
                    Lots = new();
                    //GoodsReceiptReceivedUpdated += LoadCompletedGoodsReceiptsAsync;
                }

            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        public async void LoadCompletedGoodsReceiptsAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(CompletedGoodsReceiptId))
                {
                    goodsReceiptByIds = new();
                    var goodsReceiptById = await _apiService.GetGoodsReceiptsByIdAsync(CompletedGoodsReceiptId);
                    goodsReceiptByIds.Add(goodsReceiptById);
                    var goodsReceiptByIdViewModels = goodsReceiptByIds.Select(g =>
                        new PendingGoodsReceiptViewModel(g.GoodsReceiptId,
                                                         g.Timestamp,
                                                         g.Employee.EmployeeName));
                    GoodsReceiptByIds = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptByIdViewModels);
                    GoodsReceipts = GoodsReceiptByIds;
                    Lots = new();
                    //GoodsReceiptReceivingUpdated += LoadUncompletedGoodsReceiptsAsync;
                }
                else
                {
                    var goodsReceiptByTime = await _apiService.GetGoodsReceiptsByTimeAndStateAsync(CompletedStartDate, CompletedEndDate, true);
                    goodsReceiptByTimes = goodsReceiptByTime.ToList();
                    var goodsReceiptByTimeViewModels = goodsReceiptByTime.Select(g =>
                        new PendingGoodsReceiptViewModel(g.GoodsReceiptId,
                                                         g.Timestamp,
                                                         g.Employee.EmployeeName));
                    GoodsReceiptByTimes = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptByTimeViewModels);
                    GoodsReceipts = GoodsReceiptByTimes;
                    Lots = new();
                    //GoodsReceiptReceivedUpdated += LoadCompletedGoodsReceiptsAsync;
                }

            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        private void CreateSublot(object? sender, EventArgs args)
        {
            if (sender is not null)
            {
                var lot = (GoodsReceiptLotForGoodsReceiptMaterialsView)sender;
                lot.GoodsReceiptSublots.Add(new GoodsReceiptSublotViewModel(lot.LocationId, lot.QuantityPerLocation));
            }
            OnPropertyChanged(nameof(Lots));
        }

        public async void DeleteLot()
        {
            if (selectedGoodsReceipt is not null)
            {
                if (SelectedLot is not null)
                {
                    if (MessageBox.Show("Xác nhận xóa lô " + SelectedLot.NewLotId, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            await _apiService.RemovedGoodsReceiptLotAsync(selectedGoodsReceipt.GoodsReceiptId, SelectedLot.NewLotId);
                            if (UncompletedGoodsReceiptIds.Contains(selectedGoodsReceipt.GoodsReceiptId))
                            {
                                if (!String.IsNullOrEmpty(UncompletedGoodsReceiptId))
                                {
                                    GoodsReceiptByIds = new();
                                    LoadUncompletedGoodsReceiptsAsync();
                                }
                                else
                                {
                                    GoodsReceiptByTimes = new();
                                    LoadUncompletedGoodsReceiptsAsync();
                                }

                            }

                            if (CompletedGoodsReceiptIds.Contains(selectedGoodsReceipt.GoodsReceiptId))
                            {
                                if (!String.IsNullOrEmpty(CompletedGoodsReceiptId))
                                {
                                    GoodsReceiptByIds = new();
                                    LoadCompletedGoodsReceiptsAsync();
                                }
                                else
                                {
                                    GoodsReceiptByTimes = new();
                                    LoadCompletedGoodsReceiptsAsync();
                                }
                            }
                            LoadGoodsReceiptView();
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
                        LoadGoodsReceiptView();
                    }
                    else { }
                }
            }
        }

        //public event Action? GoodsReceiptReceivingUpdated;
        //public event Action? GoodsReceiptReceivedUpdated;

        public async void UpdateAsync()
        {
            foreach (var lot in Lots)
            {
                var total = lot.GoodsReceiptSublots.Sum(i => i.QuantityPerLocation);
                if (total != lot.Quantity)
                {
                    MessageBox.Show("Nhập số sai lớn hoặc nhỏ hơn số lượng tổng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                }
            }
            var fixDto = Lots.Select(x => new FixGoodsReceiptMaterialsDto(
                x.LotId,
                x.NewLotId,
                x.GoodsReceiptSublots.Select(i => new ItemLotLocationsDto(
                    i.LocationId,
                    i.QuantityPerLocation)).ToList(),
                x.Quantity,
                x.ProductionDate,
                x.ExpirationDate,
                ""));
            try
            {
                if (SelectedGoodsReceipt is not null)
                {
                    await _apiService.FixGoodsReceiptMaterialsAsync(SelectedGoodsReceipt.GoodsReceiptId, fixDto);
                    Lots = new();
                    LoadGoodsReceiptView();
                    if (selectedGoodsReceipt is not null)
                    {
                        var goodsReceipt = goodsReceipts.Last(g => g.GoodsReceiptId == selectedGoodsReceipt.GoodsReceiptId);
                        if (UncompletedGoodsReceiptIds.Contains(goodsReceipt.GoodsReceiptId))
                        {
                            if (!String.IsNullOrEmpty(UncompletedGoodsReceiptId))
                            {
                                GoodsReceiptByIds = new();
                                LoadUncompletedGoodsReceiptsAsync();
                                UncompletedGoodsReceiptId = "";
                            }
                            else
                            {
                                GoodsReceiptByTimes = new();
                                LoadUncompletedGoodsReceiptsAsync();
                                //GoodsReceiptReceivedUpdated?.Invoke();
                            }

                        }
                        
                        if (CompletedGoodsReceiptIds.Contains(goodsReceipt.GoodsReceiptId))
                        {
                            if (!String.IsNullOrEmpty(CompletedGoodsReceiptId))
                            {
                                GoodsReceiptByIds = new();
                                LoadCompletedGoodsReceiptsAsync();
                                CompletedGoodsReceiptId = "";
                            }
                            else
                            {
                                GoodsReceiptByTimes = new();
                                LoadCompletedGoodsReceiptsAsync();
                                //GoodsReceiptReceivedUpdated?.Invoke();
                            }

                        }
                        MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                LoadGoodsReceiptView();
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
