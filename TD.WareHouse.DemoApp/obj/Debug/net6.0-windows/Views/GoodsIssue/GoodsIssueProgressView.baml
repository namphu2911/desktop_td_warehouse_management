   M S B A M L   `   `   `  ????O  KTD.WareHouse.DemoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:TD.WareHouse.DemoApp.Views.GoodsIssue%TD.WareHouse.DemoApp.Views.GoodsIssue  5      B    <TD.WareHouse.DemoApp.Views.GoodsIssue.GoodsIssueProgressView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Elocal3clr-namespace:TD.WareHouse.DemoApp.Views.GoodsIssue     5      _ [MaterialDesignThemes.Wpf, Version=4.7.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5         ColorBackground#??[  5
      $??10q?5      .??5      ?   ? ColumnDefinitions  5   
   ?? 5      $	??370 ?6   ?? 5      $??* ?6   .+?5   
   ? $??0??6    ? RowDefinitions 5      ?? 5      $??* ?6    ?? 5      $??* ?6    .+?5      #  MaterialDesignThemes.Wpf.Card  $??0??6"   $??10q?5   "   .??5      ?  5      ?? 5      $
??auto ?6(   ?? 5      $??* ?6(   .+?5      ?? $??0??6    $??10q?5         	CardTitle#??[ 5!      $%??Danh sách phiếu đang xuất??5"      $??18(?69   ?? 5#      $??1??6   $??5q?5$      $??1q?5%        	ColorGray#??[ 5&      ._?5'      ?? $??Stretch=?6#   ??5(   #   ?? *GoodsIssues+ ?? SelectedItem 5)   #   ?? *SelectedGoodsIssue+5/      50      51      52        54      $??1??6"   $??10q?55   "   .??56      ?  57      ?? 58      $
??auto ?6(   ?? 59      $??* ?6(   .+?5;      ?? $??0??6    $??10q?5<      #??[ 5=      $$??Danh sách phiếu đã xuất??5>      $??18(?67   ?? 5?      $??1??6   $??5q?5@      $??1q?5A      #??[ 5B      ._?5C      ?? $??Stretch=?6#   ??5D   #   ?? *GoodsIssues+ 5E   #   ?? *SelectedGoodsIssue+5K      5L      5M      5N      5O        5Q   
   $??1??6   $??10q?6.   .??5R      ?  5S      ?? 5T      $
??auto ?6$   ?? 5U      $
??auto ?6$   ?? 5V      $??* ?6$   .+?5Y      ?? $??0??6   .+?5Z      ?? #??[ 5[      $??Thông tin chi tiết??5\      ?? 5]      5^      ? 5`      $??1??6     5a      ?? 5b      $
??auto ?6+   ?? 5c      $??* ?6+   ?? 5d      $
??auto ?6+   ?? 5e      $??* ?6+   .+?5h      ?? $??0??6    $??5q?5i       $??Center=?5j       $??
Mã đơn:??5k       ?? 5l      $??1??6   $??5q?5m      ??5n      ?? *GoodsIssueId+?? 5p      $??2??6    $??5q?5q       $??Center=?5r       $??Khách hàng:??5s       ?? 5t      $??3??6   $??5q?5u      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Mã nhân viên??5v      ??5w      ?? *EmployeeIds+ ?? Text 5x      ?? *
EmployeeId+5y      &   System.Windows.Controls.DataGrid  5{      $??2??6     MaterialDesignDataGrid#??[ 5|        ColorSurface#??[ 5}      $??10q?5~         
IsReadOnly$
 True??5         AutoGenerateColumns$ False??5?         SelectionUnit$ FullRow=?5?         SelectionMode$ Single=?5?      ??5?      ?? *	Entries+	   Columns	 5?      0  *System.Windows.Controls.DataGridTextColumn  5?      ,  &System.Windows.Controls.DataGridColumn
   Header$
 	Kho hàng??6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   ?? *PlannedQuantity+ ?? StringFormat$ 0.###??  5?      $
 Mã sản phẩm??6-    6B   ?? *ItemId+  5?      $
 Tên sản phẩm??6-    6C   ?? *
ItemName+  5?      $
 
Đơn vị??6-    6=   ?? *Unit+  5?      $
 Số lượng??6-    6?   ?? *Unit+  5?      $
 
Đơn vị??6-    6=   ?? *Unit+  5?      $
 Số PO??6-    6<   ?? *Unit+5?      5?      5?      5?      5?      