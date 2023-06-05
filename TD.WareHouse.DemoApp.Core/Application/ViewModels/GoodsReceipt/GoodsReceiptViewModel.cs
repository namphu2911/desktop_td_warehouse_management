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
        private readonly GoodsReceiptStore _goodsReceiptStore;
        public ObservableCollection<string> GoodsReceiptIds => _goodsReceiptStore.GoodsReceiptIds;
        //
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(+1);
        public string GoodsReceiptId { get; set; } = "";
        //
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceipts { get; set; } = new();
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
                    var goodsReceipt = goodsReceipts.First(g => g.GoodsReceiptId == selectedGoodsReceipt.GoodsReceiptId);
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
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        //public ICommand ConfirmCommand { get; set; }
        public ICommand LoadGoodsReceiptViewCommand { get; set; }
        public GoodsReceiptViewModel(IApiService apiService, IDatabaseSynchronizationService databaseSynchronizationService, GoodsReceiptStore goodsReceiptStore, WarehouseStore warehouseStore)
        {
            _apiService = apiService;
            _databaseSynchronizationService = databaseSynchronizationService;
            _goodsReceiptStore = goodsReceiptStore;
            _warehouseStore = warehouseStore;
            LoadReceivingGoodsReceiptsCommand = new RelayCommand(LoadReceivingGoodsReceiptsAsync);

            UpdateCommand = new RelayCommand(UpdateAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
            LoadGoodsReceiptViewCommand = new RelayCommand(LoadGoodsReceiptView);
            //ConfirmCommand = new RelayCommand(ConfirmAsync);
        }

        private void LoadGoodsReceiptView()
        {
            _databaseSynchronizationService.SynchronizeGoodReceiptsData();
            OnPropertyChanged(nameof(GoodsReceiptIds));
        }
        
        public async void LoadReceivingGoodsReceiptsAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(GoodsReceiptId))
                {
                    goodsReceiptByIds = new();
                    var goodsReceiptById = await _apiService.GetReceivingGoodsReceiptsAsync(GoodsReceiptId);
                    goodsReceiptByIds.Add(goodsReceiptById);
                    var goodsReceiptByIdViewModels = goodsReceiptByIds.Select(g =>
                        new PendingGoodsReceiptViewModel(g.GoodsReceiptId,
                                                         g.Timestamp,
                                                         g.Employee.EmployeeName));
                    GoodsReceiptByIds = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptByIdViewModels);
                    GoodsReceipts = GoodsReceiptByIds;
                    goodsReceipts = goodsReceiptByIds;
                    Lots = new();
                    //GoodsReceiptConfirmed += LoadReceivingGoodsReceiptsAsync;
                    GoodsReceiptUpdated += LoadReceivingGoodsReceiptsAsync;
                }
                else
                {
                    var goodsReceiptByTime = await _apiService.GetReceivedGoodsReceiptsAsync(StartDate, EndDate);
                    goodsReceiptByTimes = goodsReceiptByTime.ToList();
                    var goodsReceiptByTimeViewModels = goodsReceiptByTime.Select(g =>
                        new PendingGoodsReceiptViewModel(
                                                         g.GoodsReceiptId,
                                                         g.Timestamp,
                                                         g.Employee.EmployeeName));
                    GoodsReceiptByTimes = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptByTimeViewModels);
                    GoodsReceipts = GoodsReceiptByTimes;
                    goodsReceipts = goodsReceiptByTimes;
                    Lots = new();
                    //GoodsReceiptConfirmed += LoadReceivedGoodsReceiptsAsync;
                    GoodsReceiptUpdated += LoadReceivingGoodsReceiptsAsync;
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }

            
        }
        //public void ReloadWhenConfirm()
        //{
        //    Lots = new();
        //    OnPropertyChanged(nameof(GoodsReceiptIds));
        //    GoodsReceiptId = "";
        //    GoodsReceiptByIds = new();
        //    GoodsReceiptConfirmed += ReloadWhenConfirm;
        //}
        public void ReloadWhenDelete()
        {
            Lots = new();
            GoodsReceipts = new();
            OnPropertyChanged(nameof(GoodsReceiptIds));
            GoodsReceiptId = "";
        }

        public event Action? GoodsReceiptUpdated;
        public event Action? GoodsReceiptDeleted;

        public async void UpdateAsync()
        {
            var fixDto = Lots.Select(x => new FixUncompletedGoodsReceiptDto(
                x.LotId,
                x.LocationId,
                x.Quantity,
                sublotSize: null,
                "string",
                x.PurchaseOrderNumber,
                x.ProductionDate,
                x.ExpirationDate,
                note: null));
            try
            {
                await _apiService.FixUncompltedGoodsReceiptAsync(SelectedGoodsReceipt.GoodsReceiptId, fixDto);
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
            GoodsReceiptUpdated?.Invoke();
        }
        //private async void ConfirmAsync()
        //{
        //    try
        //    {
        //        await _apiService.ConfirmGoodsReceiptAsync(SelectedGoodsReceipt.GoodsReceiptId);
        //    }
        //    catch (HttpRequestException)
        //    {
        //        ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
        //    }
        //    GoodsReceiptConfirmed?.Invoke();
        //    GoodsReceiptId = "";
        //    GoodsReceiptByIds = new();
        //}

        private async void DeleteAsync()
        {
            try
            {
                await _apiService.DeleteGoodsReceiptAsync(SelectedGoodsReceipt.GoodsReceiptId);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            ReloadWhenDelete();
        }
    }
}
