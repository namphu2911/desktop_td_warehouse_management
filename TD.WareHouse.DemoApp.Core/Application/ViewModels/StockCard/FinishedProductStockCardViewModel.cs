using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class FinishedProductStockCardViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly ItemStore _itemStore;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public ObservableCollection<StockCardEntryViewModel> StockCardEntries { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.FinishedItemIds;
        public ObservableCollection<string> Units => _itemStore.FinishedItemUnits;
        private string itemId = "";
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                if (String.IsNullOrEmpty(value))
                {
                    Unit = "";
                    OnPropertyChanged(nameof(Unit));
                }
                else
                {
                    var item = _itemStore.FinishedItems.First(i => i.ItemId == itemId);
                    Unit = item.Unit;
                    OnPropertyChanged(nameof(Unit));
                }
            }

        }
        public string Unit { get; set; } = "";

        public ICommand LoadStockCardEntryCommand { get; set; }
        public ICommand LoadStockCardViewCommand { get; set; }
        public FinishedProductStockCardViewModel(IApiService apiService, ItemStore itemStore)
        {
            _apiService = apiService;
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
                var stockCardEntries = await _apiService.GetFinishedProductStockCardAsync(EndDate, ItemId, Unit);
                var viewModels = (stockCardEntries.Select(i => new StockCardEntryViewModel(
                    i.Item.ItemClassId,
                    i.Item.ItemId,
                    i.Item.ItemName,
                    i.Item.Unit,
                    i.Item.PacketSize,
                    i.Item.PacketUnit,
                    stockCardEntries.Sum(x => x.Quantity),
                    stockCardEntries.Select(i => new StockCardLotViewModel(
                        i.PurchaseOrderNumber,
                        i.Quantity,
                        i.NumOfPackets,
                        null,
                        0)).ToList()))).FirstOrDefault();
                
                if(viewModels != null)
                {
                    StockCardEntries = new();
                    StockCardEntries.Add(viewModels);
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }


    }
}
