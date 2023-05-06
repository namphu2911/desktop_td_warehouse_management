﻿using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class StockCardViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-30).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public string ItemId { get; set; } = "";
        public ObservableCollection<StockCardEntryViewModel> StockCardEntries { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.ItemIds;

        public ICommand LoadStockCardEntryCommand { get; set; }
        public ICommand LoadStockCardViewCommand { get; set; }
        public StockCardViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
            LoadStockCardEntryCommand = new RelayCommand(LoadStockCardEntry);
            LoadStockCardViewCommand = new RelayCommand(LoadStockCardView);
        }

        private void LoadStockCardView()
        {
            OnPropertyChanged(nameof(ItemIds));
        }
        private async void LoadStockCardEntry()
        {
            try
            {
                if(!String.IsNullOrEmpty(ItemId))
                {
                    var stockCardEntries = await _apiService.GetStockCardEntriesAsync(ItemId, StartDate, EndDate);
                    var viewModels = _mapper.Map<IEnumerable<InventoryLogEntryDto>, IEnumerable<StockCardEntryViewModel>>(stockCardEntries);
                    StockCardEntries = new(viewModels);
                }
                else
                {
                    var stockCardEntries = await _apiService.GetStockCardEntriesByTimeAsync(StartDate, EndDate);
                    var viewModels = _mapper.Map<IEnumerable<InventoryLogEntryDto>, IEnumerable<StockCardEntryViewModel>>(stockCardEntries);
                    StockCardEntries = new(viewModels);
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }


    }
}
