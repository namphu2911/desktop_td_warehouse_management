using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory;

namespace TD.WareHouse.DemoApp.Views.Inventory
{
    /// <summary>
    /// Interaction logic for InventoryHistoryView.xaml
    /// </summary>
    public partial class InventoryHistoryView : UserControl
    {
        public InventoryHistoryView()
        {
            InitializeComponent();
        }

        private void SaveFileButtonClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                var viewModel = (InventoryHistoryViewModel)DataContext;
                if (viewModel.ExportToExcelCommand.CanExecute(saveFileDialog.FileName))
                    viewModel.ExportToExcelCommand.Execute(saveFileDialog.FileName);
            }

        }
    }
}
