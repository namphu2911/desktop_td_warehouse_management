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

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private List<GoodsReceiptDto> goodsReceipts = new();
        private PendingGoodsReceiptViewModel? selectedGoodsReceipt;
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime EndDate { get; set; } = DateTime.Today;
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceipts { get; set; } = new();
       
        public PendingGoodsReceiptViewModel? SelectedGoodsReceipt
        {
            get => selectedGoodsReceipt;
            set
            {
                selectedGoodsReceipt = value;
                if (selectedGoodsReceipt is not null)
                {
                    var goodsReceipt = goodsReceipts.First(g => g.GoodsReceiptId == selectedGoodsReceipt.GoodsReceiptId);

                    
                }
            }
        }

        public ICommand LoadReceivedGoodsReceiptsCommand { get; set; }

        public GoodsReceiptViewModel(IApiService apiService)
        {
            _apiService = apiService;
            LoadReceivedGoodsReceiptsCommand = new RelayCommand(LoadReceivedGoodsReceiptsAsync);
        }

        public async void LoadReceivedGoodsReceiptsAsync()
        {
            var queryResult = (await _apiService.GetGoodsReceiptsAsync(StartDate, EndDate));
            goodsReceipts = queryResult.Items.ToList();

            var goodsReceiptViewModels = goodsReceipts.Select(g =>
                new PendingGoodsReceiptViewModel(_apiService,
                                                 g.GoodsReceiptId,
                                                 g.Timestamp,
                                                 g.Employee.EmployeeId,
                                                 g.Employee.EmployeeName));
            GoodsReceipts = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptViewModels);
            //Lots = new();
            foreach (var goodsReceipt in GoodsReceipts)
            {
                goodsReceipt.GoodsReceiptConfirmed += LoadReceivedGoodsReceiptsAsync;
                goodsReceipt.GoodsReceiptDeleted += LoadReceivedGoodsReceiptsAsync;
            }
        }
    }
}
