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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class StockCardViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
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
                var stockCardEntry = await _apiService.GetStockCardItemLotsAsync(EndDate, ItemId);
                List<InventoryLogEntryItemLotDto> stockCardEntries = new();
                stockCardEntries.Add(stockCardEntry);
                var viewModels = stockCardEntries.Select(i => new StockCardEntryViewModel(
                    i.Item.ItemClassId,
                    i.Item.ItemId,
                    i.Item.ItemName,
                    i.Item.Unit,
                    i.Item.PacketSize,
                    i.Item.PacketUnit,
                    i.TotalQuantity,
                    i.Lots.Select(x => new StockCardLotViewModel(
                        x.LotId,
                        x.Quantity,
                        x.NumOfPackets)).ToList()));
                StockCardEntries = new(viewModels);

                //var stockCardEntries = await _apiService.GetStockCardItemLotsAsync(EndDate, ItemId);
                //var viewModels = (stockCardEntries.Select(i => new StockCardEntryViewModel(
                //    i.Item.ItemClassId,
                //    i.Item.ItemId,
                //    i.Item.ItemName,
                //    i.Item.Unit,
                //    i.Item.PacketSize,
                //    i.Item.PacketUnit,
                //    stockCardEntries.Sum(x => x.Quantity),
                //    stockCardEntries.SelectMany(i => i.ItemLotLocations.Select(x => new StockCardLotViewModel(
                //        i.LotId,
                //        i.Quantity,
                //        i.NumOfPackets,
                //        x.LocationId,
                //        x.QuantityPerLocation))).ToList()))).FirstOrDefault();
                //if (viewModels != null)
                //{
                //    for (int i = 0; i < viewModels.StockCardLots.Count - 1; i++)
                //    {
                //        if (viewModels.StockCardLots[i + 1].ItemLotId == viewModels.StockCardLots[i].ItemLotId)
                //        {
                //            viewModels.StockCardLots[i + 1].ItemLotId = "";
                //            viewModels.StockCardLots[i + 1].Quantity = null;
                //        }
                //    }
                //    StockCardEntries = new();
                //    StockCardEntries.Add(viewModels);
                //}
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }


    }
}

