   M S B A M L   `   `   `  ????O  KTD.WareHouse.DemoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:TD.WareHouse.DemoApp.Views.StockCard$TD.WareHouse.DemoApp.Views.StockCard  5      8    2TD.WareHouse.DemoApp.Views.StockCard.StockCardView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlocal2clr-namespace:TD.WareHouse.DemoApp.Views.StockCard     5      _ [MaterialDesignThemes.Wpf, Version=4.7.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5         ColorBackground#??[  5
      $??10q?5      .??5      ?   ? RowDefinitions  5   
   ?? 5      $
??auto ?6   ?? 5      $??* ?6   .+?5   
   #  MaterialDesignThemes.Wpf.Card  $??0??6     ColorSurface#??[ 5   &   $??10q?5   &   .??5      ?   5      ?? 5      $
??auto ?6$   ?? 5      $
??auto ?6$   .+?5      ? $??0??6    ? ColumnDefinitions 5      ?? 5      $
??auto ?6+   ?? 5      $??* ?6+   ?? 5      $
??auto ?6+   ?? 5       $??* ?6+   ?? 5!      $
??auto ?6+   ?? 5"      $??2* ?6+   .+?5%      ?? $??0??6    $??0??6-   $??10q?5&        
FieldLabel#??[ 5'      $??	Kho hàng??5(      ?? 5*      $??0??6   $??1??6,   $??10q?5+      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 	Kho hàng??5,        ComboBox#??[ 5-      ??5.      ?? *	ItemIds+ ?? Text 5/      ?? *ItemId+??51      ?? 52      .R?53   "   $? 54      56      ?? 58      $??0??6    $??2??6-   $??10q?59      #??[ 5:      $??Mã sản phẩm??5;      ?? 5=      $??0??6   $??3??6,   $??10q?5>      $ Mã sản phẩm??5?      #??[ 5@      ??5A      ?? *	ItemNames+ 5B      ?? *
ItemName+??5E      ?? 5F      .R?5G   "   $? 5H      5J      ?? 5L      $??0??6    $??4??6-   $??10q?5M      #??[ 5N      $??Tên sản phẩm??5O      ?? 5Q      $??0??6   $??5??6,   $??10q?5R      $ Tên sản phẩm??5S      #??[ 5T      ??5U      ?? *	ItemNames+ 5V      ?? *
ItemName+??5Y      ?? 5Z      .R?5[   "   $? 5\      5^      5a      ? 5c      $??1??6    5d      ?? 5e      $
??auto ?6+   ?? 5f      $
??1.1* ?6+   ?? 5g      $
??auto ?6+   ?? 5h      $
??1.1* ?6+   ?? 5i      $
??auto ?6+   ?? 5j      $??* ?6+   ?? 5k      $
??auto ?6+   .+?5n      ?? $??1??6    $??0??6-   $??10q?5o      #??[ 5p      $??Ngày bắt đầu??5q      (  "System.Windows.Controls.DatePicker  5s      $??1??6"   $??1??6/   $??10q?5t      $ Ngày bắt đầu??5u        
DatePicker#??[ 5v         SelectedDate 5w      ?? *	StartDate+?? 5z      $??1??6    $??2??6-   $??10q?5{      #??[ 5|      $??Ngày kết thúc??5}        5      $??1??6"   $??3??6/   $??10q?5?      $ Ngày kết thúc??5?      #??[ 5?       5?      ?? *	EndDate+?? 5?      $??1??6    $??4??6-   $??10q?5?      #??[ 5?      $??HSD còn lại??5?      ?? 5?      $??1??6   $??5??6,   $??10q?5?      $ HSD còn lại??5?      #??[ 5?      ??5?      ?? *	ItemIds+ 5?      ?? *ItemId+??5?      ?? 5?      .R?5?   "   $? 5?      5?      ?? 5?      $??0??6   $??6??6*   $??10q?5?        ButtonPrimary#??[ 5?      $??Truy xuất??5?      $??Center=?5?      $	??150??5?      5?      5?      5?      5?      ? 5?   
   $??2??6   .+?5?        $??1??6"   #??[ 5?   &   $??10q?5?   &   .??5?      &   System.Windows.Controls.DataGrid    MaterialDesignDataGrid#??[ 6   #??[ 5?         
IsReadOnly$
 True??5?         AutoGenerateColumns$ False??5?         SelectionUnit$ FullRow=?5?         SelectionMode$ Single=?5?      ??5?      ?? *StockCardEntries+	   Columns	 5?      0  *System.Windows.Controls.DataGridTextColumn  5?      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Mã sản phẩm??6-     5?      $
 Tên sản phẩm??6-     5?      $
 
Đơn vị??6-     5?      $
 Mã lô??6-     5?      $
 Tồn đầu kì??6-     5?      $
 Số lượng nhập??6-     5?      $
 Số lượng xuất??6-     5?      $
 Tồn cuối kì??6-     5?      $
 Stockmin??6-     5?      $
 	Kho hàng??6-     5?      $
 HSD còn lại??6-   5?      5?      5?      5?      5?      