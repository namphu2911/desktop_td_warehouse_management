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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptCompletedViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private List<GoodsReceiptDto> goodsReceiptByTimes = new();
        private PendingGoodsReceiptViewModel? selectedGoodsReceipt;
        //
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(+1);
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
                    var goodsReceipt = goodsReceiptByTimes.First(g => g.GoodsReceiptId == selectedGoodsReceipt.GoodsReceiptId);
                    var lotViewModels = goodsReceipt.Lots.Select(c => new GoodsReceiptLotForGoodsReceiptView(
                    null,
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

        public ICommand LoadReceivedGoodsReceiptsCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public GoodsReceiptCompletedViewModel(IApiService apiService)
        {
            _apiService = apiService;
            LoadReceivedGoodsReceiptsCommand = new RelayCommand(LoadReceivedGoodsReceiptsAsync);

            UpdateCommand = new RelayCommand(UpdateAsync);
            ConfirmCommand = new RelayCommand(ConfirmAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
        }

        public async void LoadReceivedGoodsReceiptsAsync()
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
            GoodsReceiptConfirmed += LoadReceivedGoodsReceiptsAsync;
            GoodsReceiptDeleted += LoadReceivedGoodsReceiptsAsync;
            GoodsReceiptUpdated += LoadReceivedGoodsReceiptsAsync;
        }

        public event Action? GoodsReceiptConfirmed;
        public event Action? GoodsReceiptDeleted;
        public event Action? GoodsReceiptUpdated;

        public async void UpdateAsync()
        {
            var fixDto = Lots.Select(x => new FixCompletedGoodsReceiptDto(
                x.LotId,
                x.Quantity));
            try
            {
                await _apiService.FixCompltedGoodsReceiptAsync(SelectedGoodsReceipt.GoodsReceiptId, fixDto);
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
