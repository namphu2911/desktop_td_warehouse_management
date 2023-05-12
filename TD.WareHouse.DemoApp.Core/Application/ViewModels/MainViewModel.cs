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
        public GoodsReceiptNavigationViewModel GoodsReceipt { get; set; }
        public HistoryViewModel History { get; set; }
        public HomeViewModel Home { get; set; }
        public InventoryNavigationViewModel InventoryNavigation { get; set; }
        public IsolationViewModel Isolation { get; set; }
        public ShelfManagementViewModel ShelfManagement { get; set; }
        public StockCardNavigationViewModel StockCardNavigation { get; set; }
        public ICommand LoadDataStoreCommand { get; set; }
        public MainViewModel(IDatabaseSynchronizationService databaseSynchronizationService, AlarmViewModel alarm, GoodsIssueViewModel goodsIssue, GoodsReceiptNavigationViewModel goodsReceipt, HistoryViewModel history, HomeViewModel home, InventoryNavigationViewModel inventoryNavigation, IsolationViewModel isolation, ShelfManagementViewModel shelfManagement, StockCardNavigationViewModel stockCardNavigation)
        {
            _databaseSynchronizationService = databaseSynchronizationService;
            Alarm = alarm;
            GoodsIssue = goodsIssue;
            GoodsReceipt = goodsReceipt;
            History = history;
            Home = home;
            InventoryNavigation = inventoryNavigation;
            Isolation = isolation;
            ShelfManagement = shelfManagement;
            StockCardNavigation = stockCardNavigation;
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
