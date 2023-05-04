using System;
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
        private List<GoodsReceiptDto> goodsReceipts = new();
        private PendingGoodsReceiptViewModel? selectedGoodsReceipt;

        private readonly GoodsReceiptStore _goodsReceiptStore;
        public ObservableCollection<string> GoodsReceiptIds => _goodsReceiptStore.GoodsReceiptIds;
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime EndDate { get; set; } = DateTime.Today;
        public string GoodsReceiptId { get; set; } = "";
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceipts { get; set; } = new();
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
                    _apiService,
                    goodsReceipt.GoodsReceiptId, goodsReceipt.Employee.EmployeeName,
                    c.Item.ItemClass.ItemClassId,
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
            var queryResult = await _apiService.GetReceivedGoodsReceiptsAsync(StartDate, EndDate);
            goodsReceipts = queryResult.Items.ToList();

            var goodsReceiptViewModels = goodsReceipts.Select(g =>
                new PendingGoodsReceiptViewModel(_apiService,
                                                 g.GoodsReceiptId,
                                                 g.Timestamp,
                                                 g.Employee.EmployeeName));
            GoodsReceipts = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptViewModels);
            Lots = new();
            
        }

        public async void LoadReceivingGoodsReceiptsAsync()
        {
            var queryResult = await _apiService.GetReceivingGoodsReceiptsAsync(GoodsReceiptId);
            goodsReceipts = queryResult.Items.ToList();

            var goodsReceiptViewModels = goodsReceipts.Select(g =>
                new PendingGoodsReceiptViewModel(_apiService,
                                                 g.GoodsReceiptId,
                                                 g.Timestamp,
                                                 g.Employee.EmployeeName));
            GoodsReceipts = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptViewModels);
            Lots = new();
            
        }

        private async void ConfirmAsync()
        {
            try
            {
                await _apiService.ConfirmGoodsReceiptAsync(GoodsReceiptId);
                LoadReceivedGoodsReceiptsAsync();
                LoadReceivingGoodsReceiptsAsync();
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
                await _apiService.DeleteGoodsReceiptAsync(GoodsReceiptId);
                LoadReceivedGoodsReceiptsAsync();
                LoadReceivingGoodsReceiptsAsync();
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            
        }
    }
}
