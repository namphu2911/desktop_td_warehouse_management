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

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public AlarmViewModel Alarm { get; set; }
        public GoodsIssueViewModel GoodsIssue { get; set; }
        public GoodsReceiptViewModel GoodsReceipt { get; set; }
        public HistoryViewModel History { get; set; }
        public HomeViewModel Home { get; set; }
        public InventoryViewModel Inventory { get; set; }
        public IsolationViewModel Isolation { get; set; }
        public ShelfManagementViewModel ShelfManagement { get; set; }
        public StockCardViewModel StockCard { get; set; }
        public MainViewModel(AlarmViewModel alarm, GoodsIssueViewModel goodsIssue, GoodsReceiptViewModel goodsReceipt, HistoryViewModel history, HomeViewModel home, InventoryViewModel inventory, IsolationViewModel isolation, ShelfManagementViewModel shelfManagement, StockCardViewModel stockCard)
        {
            Alarm = alarm;
            GoodsIssue = goodsIssue;
            GoodsReceipt = goodsReceipt;
            History = history;
            Home = home;
            Inventory = inventory;
            Isolation = isolation;
            ShelfManagement = shelfManagement;
            StockCard = stockCard;
        }
    }
}
