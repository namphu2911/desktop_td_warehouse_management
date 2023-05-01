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

namespace TD.WareHouse.DemoApp.Views.GoodsIssue
{
    /// <summary>
    /// Interaction logic for GoodsIssueExternalView.xaml
    /// </summary>
    public partial class GoodsIssueExternalView : UserControl
    {
        public GoodsIssueExternalView()
        {
            InitializeComponent();
        }

        private void BrowseFileButtonClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                textBoxFilePath.Text = openFileDialog.FileName;
            }
        }
    }
}
