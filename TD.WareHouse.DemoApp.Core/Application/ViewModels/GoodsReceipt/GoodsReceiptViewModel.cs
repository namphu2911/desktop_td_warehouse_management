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
        private List<GoodsReceiptDto> goodsReceiptByIds = new();
        private PendingGoodsReceiptViewModel? selectedGoodsReceipt;
        //
        private readonly WarehouseStore _warehouseStore;
        public ObservableCollection<string> LocationIds => _warehouseStore.LocationIds;
        private readonly GoodsReceiptStore _goodsReceiptStore;
        public ObservableCollection<string> GoodsReceiptIds => _goodsReceiptStore.GoodsReceiptIds;
        public string GoodsReceiptId { get; set; } = "";
        //
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceiptByIds { get; set; } = new();
        public ObservableCollection<GoodsReceiptLotForGoodsReceiptView> Lots { get; set; } = new();

        public PendingGoodsReceiptViewModel? SelectedGoodsReceipt
        {
            get => selectedGoodsReceipt;
            set
            {
                selectedGoodsReceipt = value;
                if (selectedGoodsReceipt is not null)
                {
                    var goodsReceipt = goodsReceiptByIds.First(g => g.GoodsReceiptId == selectedGoodsReceipt.GoodsReceiptId);
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
        public ICommand ConfirmCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadGoodsReceiptViewCommand { get; set; }
        public GoodsReceiptViewModel(IApiService apiService, GoodsReceiptStore goodsReceiptStore, WarehouseStore warehouseStore)
        {
            _apiService = apiService;
            _goodsReceiptStore = goodsReceiptStore;
            _warehouseStore = warehouseStore;
            LoadReceivingGoodsReceiptsCommand = new RelayCommand(LoadReceivingGoodsReceiptsAsync);

            UpdateCommand = new RelayCommand(UpdateAsync);
            ConfirmCommand = new RelayCommand(ConfirmAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
            LoadGoodsReceiptViewCommand = new RelayCommand(LoadGoodsReceiptView);
        }

        private void LoadGoodsReceiptView()
        {
            OnPropertyChanged(nameof(GoodsReceiptIds));
        }
        
        public async void LoadReceivingGoodsReceiptsAsync()
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
            GoodsReceiptUpdated += LoadReceivingGoodsReceiptsAsync;
            GoodsReceiptConfirmed += LoadReceivingGoodsReceiptsAsync;
        }
        public void ReloadWhenDelete()
        {
            OnPropertyChanged(nameof(GoodsReceiptIds));
            GoodsReceiptId = "";
            GoodsReceiptByIds = new();
            Lots = new();
            GoodsReceiptDeleted += ReloadWhenDelete;
        }

        public event Action? GoodsReceiptUpdated;
        public event Action? GoodsReceiptConfirmed;
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
        private async void ConfirmAsync()
        {
            try
            {
                await _apiService.ConfirmGoodsReceiptAsync(SelectedGoodsReceipt.GoodsReceiptId);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            GoodsReceiptConfirmed?.Invoke();
            GoodsReceiptId = "";
            GoodsReceiptByIds = new();
        }

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
            GoodsReceiptDeleted?.Invoke();
        }
    }
}
