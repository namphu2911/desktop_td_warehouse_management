   M S B A M L   `   `   `  ????O  KTD.WareHouse.DemoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:TD.WareHouse.DemoAppTD.WareHouse.DemoApp  5   	   Q-clr-namespace:TD.WareHouse.DemoApp.Views.HomeTD.WareHouse.DemoApp.Views.Home  5   	   a5clr-namespace:TD.WareHouse.DemoApp.Views.GoodsReceipt'TD.WareHouse.DemoApp.Views.GoodsReceipt  5   	   ]3clr-namespace:TD.WareHouse.DemoApp.Views.GoodsIssue%TD.WareHouse.DemoApp.Views.GoodsIssue  5	   	   [2clr-namespace:TD.WareHouse.DemoApp.Views.Inventory$TD.WareHouse.DemoApp.Views.Inventory  5
   	   g8clr-namespace:TD.WareHouse.DemoApp.Views.ShelfManagement*TD.WareHouse.DemoApp.Views.ShelfManagement  5   	   [2clr-namespace:TD.WareHouse.DemoApp.Views.StockCard$TD.WareHouse.DemoApp.Views.StockCard  5   	   W0clr-namespace:TD.WareHouse.DemoApp.Views.History"TD.WareHouse.DemoApp.Views.History  5   	   [2clr-namespace:TD.WareHouse.DemoApp.Views.Isolation$TD.WareHouse.DemoApp.Views.Isolation  5   	   S.clr-namespace:TD.WareHouse.DemoApp.Views.Alarm TD.WareHouse.DemoApp.Views.Alarm  5   	   %    TD.WareHouse.DemoApp.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:TD.WareHouse.DemoApp     5   	   C	ViewsHome-clr-namespace:TD.WareHouse.DemoApp.Views.Home     5   	   SViewsGoodsReceipt5clr-namespace:TD.WareHouse.DemoApp.Views.GoodsReceipt     5   	   OViewsGoodsIssue3clr-namespace:TD.WareHouse.DemoApp.Views.GoodsIssue     5	   	   LInventoryView2clr-namespace:TD.WareHouse.DemoApp.Views.Inventory     5
   	   XShelfManagementView8clr-namespace:TD.WareHouse.DemoApp.Views.ShelfManagement     5   	   LStockCardView2clr-namespace:TD.WareHouse.DemoApp.Views.StockCard     5   	   HHistoryView0clr-namespace:TD.WareHouse.DemoApp.Views.History     5   	   H	Isolation2clr-namespace:TD.WareHouse.DemoApp.Views.Isolation     5   	   D	AlarmView.clr-namespace:TD.WareHouse.DemoApp.Views.Alarm     5   	   _ [MaterialDesignThemes.Wpf, Version=4.7.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      ColorBackground#??[  5   	     ? Title$  
MainWindow??5   	   $	??800??6   $
??1300??6)   .??5      #  MaterialDesignThemes.Wpf.Card  .??5   
   ? .+?5      ??  ?? TabStripPlacement$
 Left=?6    " MaterialDesignFilledTabControl#??[ 62   .@?5      ?? $??50??6   $	??200??6&   $??0q?62   ??5      ?? 5      $f??`C:\Users\DELL\source\repos\TD.WareHouse.DemoApp\TD.WareHouse.DemoApp\Resources\Images\LogoTD.jpg??6    $
??Fill=?5       .??5      .   (TD.WareHouse.DemoApp.Views.Home.HomeView  6+   5      ?? 5       $??50??6   $	??200??6&   
???  ????5!      ?? 5"      $??
NHẬP KHO??62   
???????$??Center=?5$   B   $??20(?5%   B   $
??Bold"?5&   B   .??5(      >   8TD.WareHouse.DemoApp.Views.GoodsReceipt.GoodsReceiptView  6;   5)      ?? 5+      $??50??6   $	??200??6&   
???  ????5,      ?? 5-      $??
XUẤT KHO??62   
???????$??Center=?5/   B   $??20(?50   B   $
??Bold"?51   B   .??53      :   4TD.WareHouse.DemoApp.Views.GoodsIssue.GoodsIssueView  67   54      ?? 56      $??50??6   $	??200??6&   
???  ????57      ?? 58      $??
KIỂM KÊ??62   
???????$??Center=?5:   B   $??20(?5;   B   $
??Bold"?5<   B   .??5>      8   2TD.WareHouse.DemoApp.Views.Inventory.InventoryView  64   5?      ?? 5A      $??50??6   $	??200??6&   
???  ????5B      ?? 5C      $??KỆ KHO??62   
???????$??Center=?5E   B   $??20(?5F   B   $
??Bold"?5G   B   .??5I      D   >TD.WareHouse.DemoApp.Views.ShelfManagement.ShelfManagementView  6@   5J      ?? 5L      $??50??6   $	??200??6&   
???  ????5M      ?? 5N      $??	TỒN KHO??62   
???????$??Center=?5P   B   $??20(?5Q   B   $
??Bold"?5R   B   .??5T      8   2TD.WareHouse.DemoApp.Views.StockCard.StockCardView  64   5U      ?? 5W      $??50??6   $	??200??6&   
???  ????5X      ?? 5Y      $??LỊCH SỬ??62   
???????$??Center=?5[   B   $??20(?5\   B   $
??Bold"?5]   B   .??5_      4   .TD.WareHouse.DemoApp.Views.History.HistoryView  60   5`      ?? 5b      $??50??6   $	??200??6&   
???  ????5c      ?? 5d      $??CÁCH LY??62   
???????$??Center=?5f   B   $??20(?5g   B   $
??Bold"?5h   B   .??5j      8	   2TD.WareHouse.DemoApp.Views.Isolation.IsolationView	  60   5k      ?? 5m      $??50??6   $	??200??6&   
???  ????5n      ?? 5o      $??CẢNH BÁO??62   
???????$??Center=?5q   B   $??20(?5r   B   $
??Bold"?5s   B   .??5u      0
   *TD.WareHouse.DemoApp.Views.Alarm.AlarmView
  6,   5v      5w      5x      5y      5z      