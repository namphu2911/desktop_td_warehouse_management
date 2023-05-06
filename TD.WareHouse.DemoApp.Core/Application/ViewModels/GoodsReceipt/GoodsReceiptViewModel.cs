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

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private List<GoodsReceiptDto> goodsReceiptByIds = new();
        private List<GoodsReceiptDto> goodsReceiptByTimes = new();
        private List<GoodsReceiptDto> goodsReceiptsTotal = new(); 
        private PendingGoodsReceiptViewModel? selectedGoodsReceipt;
        //
        private readonly GoodsReceiptStore _goodsReceiptStore;
        public ObservableCollection<string> GoodsReceiptIds => _goodsReceiptStore.GoodsReceiptIds;
        public string GoodsReceiptId { get; set; } = "";
        //
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime EndDate { get; set; } = DateTime.Today;
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
                        goodsReceiptsTotal.Add(goodReceipt);
                    }
                    foreach (var goodReceipt in goodsReceiptByTimes)
                    {
                        goodsReceiptsTotal.Add(goodReceipt);
                    }
                    var goodsReceipt = goodsReceiptsTotal.First(g => g.GoodsReceiptId == selectedGoodsReceipt.GoodsReceiptId);
                    var lotViewModels = goodsReceipt.Lots.Select(c => new GoodsReceiptLotForGoodsReceiptView(
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
        public ICommand LoadReceivingGoodsReceiptsCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadGoodsReceiptViewCommand { get; set; }
        public GoodsReceiptViewModel(IApiService apiService, GoodsReceiptStore goodsReceiptStore)
        {
            _apiService = apiService;
            _goodsReceiptStore = goodsReceiptStore;
            LoadReceivedGoodsReceiptsCommand = new RelayCommand(LoadReceivedGoodsReceiptsAsync);
            LoadReceivingGoodsReceiptsCommand = new RelayCommand(LoadReceivingGoodsReceiptsAsync);
            
            ConfirmCommand = new RelayCommand(ConfirmAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
            LoadGoodsReceiptViewCommand = new RelayCommand(LoadGoodsReceiptView);
        }

        private void LoadGoodsReceiptView()
        {
            OnPropertyChanged(nameof(GoodsReceiptIds));
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
        }

        public event Action? GoodsReceiptConfirmed;
        public event Action? GoodsReceiptDeleted;
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
