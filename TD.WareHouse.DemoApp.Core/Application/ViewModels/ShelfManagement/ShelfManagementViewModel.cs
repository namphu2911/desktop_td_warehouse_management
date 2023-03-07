using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.ShelfManagement
{
    public class ShelfManagementViewModel
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public ShelfManagementViewModel(string itemId, string itemName)
        {
            ItemId = itemId;
            ItemName = itemName;
            LoadProductCommand = new RelayCommand(LoadProduct);
            LoadLocationCommand = new RelayCommand(LoadLocation);

        }
        public ICommand LoadProductCommand { get; set; }
        public ICommand LoadLocationCommand { get; set; }
        private async void LoadProduct()
        {
            //try
            //{
            //    var stockCardEntries = await _apiService.GetStockCardAsync(StartDate, EndDate, ItemId);
            //    StockCardEntries = new ObservableCollection<StockCardEntryDto>(stockCardEntries);
            //}
            //catch (HttpRequestException)
            //{
            //    ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            //}
        }

        private async void LoadLocation()
        {
            //try
            //{
            //    var stockCardEntries = await _apiService.GetStockCardAsync(StartDate, EndDate, ItemId);
            //    StockCardEntries = new ObservableCollection<StockCardEntryDto>(stockCardEntries);
            //}
            //catch (HttpRequestException)
            //{
            //    ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            //}
        }
    }
}
