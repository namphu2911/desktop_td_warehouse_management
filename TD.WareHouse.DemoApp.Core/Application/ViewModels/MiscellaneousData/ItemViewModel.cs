using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.MiscellaneousData
{
    public class ItemViewModel : BaseViewModel
    {
        private IApiService? _apiService;
        private IMapper? _mapper;
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double MinimumStockLevel { get; set; }
        public decimal Price { get; set; }
        public string ItemClassId { get; set; }

        public event Action? Updated;
        public ICommand SaveCommand { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ItemViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            SaveCommand = new RelayCommand(SaveAsync);
        }
        public ItemViewModel(string itemId, string itemName, string unit, double minimumStockLevel, decimal price, string itemClassId) : this()
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            MinimumStockLevel = minimumStockLevel;
            Price = price;
            ItemClassId = itemClassId;
        }

        public void SetApiService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public void SetMapper(IMapper mapper)
        {
            _mapper = mapper;
            OnPropertyChanged();
        }

        private async void SaveAsync()
        {

            FixItemDto fixDto = new FixItemDto(ItemId, ItemName, MinimumStockLevel, Price, Unit, ItemClassId);
            if (_mapper is not null && _apiService is not null)
            {
                try
                {
                    await _apiService.FixItemAsync(fixDto);
                    MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (HttpRequestException)
                {
                    ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
                }
            }
            Updated?.Invoke();  

               
        }
    }
}
