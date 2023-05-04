using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.History;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Home;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Isolation;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.ShelfManagement;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
        public AlarmViewModel Alarm { get; set; }
        public GoodsIssueViewModel GoodsIssue { get; set; }
        public GoodsReceiptViewModel GoodsReceipt { get; set; }
        public HistoryViewModel History { get; set; }
        public HomeViewModel Home { get; set; }
        public InventoryViewModel Inventory { get; set; }
        public IsolationViewModel Isolation { get; set; }
        public ShelfManagementViewModel ShelfManagement { get; set; }
        public StockCardViewModel StockCard { get; set; }
        public ICommand LoadDataStoreCommand { get; set; }
        public MainViewModel(IDatabaseSynchronizationService databaseSynchronizationService, AlarmViewModel alarm, GoodsIssueViewModel goodsIssue, GoodsReceiptViewModel goodsReceipt, HistoryViewModel history, HomeViewModel home, InventoryViewModel inventory, IsolationViewModel isolation, ShelfManagementViewModel shelfManagement, StockCardViewModel stockCard)
        {
            _databaseSynchronizationService = databaseSynchronizationService;
            Alarm = alarm;
            GoodsIssue = goodsIssue;
            GoodsReceipt = goodsReceipt;
            History = history;
            Home = home;
            Inventory = inventory;
            Isolation = isolation;
            ShelfManagement = shelfManagement;
            StockCard = stockCard;
            LoadDataStoreCommand = new RelayCommand(LoadDataStoreAsync);
        }

        private async void LoadDataStoreAsync()
        {
            await Task.WhenAll(
                _databaseSynchronizationService.SynchronizeItemsData(),
                _databaseSynchronizationService.SynchronizeItemLotsData(),
                _databaseSynchronizationService.SynchronizeWarehousesData(),
                _databaseSynchronizationService.SynchronizeGoodReceiptsData(),
                _databaseSynchronizationService.SynchronizeGoodIssuesData()
                );
        }
    }
}
