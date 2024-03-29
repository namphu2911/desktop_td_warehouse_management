﻿using Microsoft.Win32;
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

namespace TD.WareHouse.DemoApp.Views.MiscellaneousData
{
    /// <summary>
    /// Interaction logic for ManageItemView.xaml
    /// </summary>
    public partial class ManageItemView : UserControl
    {
        public ManageItemView()
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

        private void DataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dataGrid && e.OriginalSource is FrameworkElement frameworkElement && frameworkElement.DataContext == dataGrid.SelectedItem)
            {
                if (dataGrid.RowDetailsVisibilityMode == DataGridRowDetailsVisibilityMode.VisibleWhenSelected)
                    dataGrid.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
                else
                    dataGrid.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
            }
        }
    }
}
