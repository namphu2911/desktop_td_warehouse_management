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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.History
{
    public class HistoryGoodsReceiptViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        private readonly WarehouseStore _warehouseStore;
        private readonly GoodsReceiptStore _goodsReceiptStore;
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        
        private string itemId = "";
        private string itemName = "";
        private string warehouseId = "";
        private string supplier = "";

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
                    itemName = "";
                    OnPropertyChanged(nameof(ItemName));
                }
                else
                {
                    var item = _itemStore.Items.First(i => i.ItemId == itemId);
                    itemName = item.ItemName;
                    OnPropertyChanged(nameof(ItemName));
                }
            }

        }
        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
                if (String.IsNullOrEmpty(value))
                {
                    itemId = "";
                    OnPropertyChanged(nameof(ItemId));
                }
                else
                {
                    var item = _itemStore.Items.First(i => i.ItemName == itemName);
                    itemId = item.ItemId;
                    OnPropertyChanged(nameof(ItemId));
                }
            }
        }
        public string WarehouseId
        {
            get
            {
                return warehouseId;
            }
            set
            {
                warehouseId = value;
                OnPropertyChanged();
            }
        }
        public string Supplier
        {
            get
            {
                return supplier;
            }
            set 
            { 
                supplier = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<HistoryGoodsReceiptLotViewModel> HistoryGoodsReceiptLots { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.ItemIds;
        public ObservableCollection<string> ItemNames => _itemStore.ItemNames;
        public ObservableCollection<string> WarehouseIds => _warehouseStore.WarehouseIds;
        public ObservableCollection<string> Suppliers => _goodsReceiptStore.Suppliers;

        public bool ButtonEnable => (!String.IsNullOrEmpty(Supplier) && String.IsNullOrEmpty(WarehouseId) && String.IsNullOrEmpty(ItemId) && String.IsNullOrEmpty(ItemName))
            || (!String.IsNullOrEmpty(ItemId) && String.IsNullOrEmpty(WarehouseId) && String.IsNullOrEmpty(Supplier))
            || (!String.IsNullOrEmpty(WarehouseId) && String.IsNullOrEmpty(Supplier) && String.IsNullOrEmpty(ItemId) && String.IsNullOrEmpty(ItemName));
        
        public ICommand LoadHistoryGoodsReceiptLotCommand { get; set; }
        public ICommand LoadHistoryGoodsReceiptViewCommand { get; set; }

        public HistoryGoodsReceiptViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore, ItemLotStore itemLotStore, WarehouseStore warehouseStore, GoodsReceiptStore goodsReceiptStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
            _warehouseStore = warehouseStore;
            _goodsReceiptStore = goodsReceiptStore;
            
            LoadHistoryGoodsReceiptLotCommand = new RelayCommand(LoadHistoryGoodsReceiptLot);
            LoadHistoryGoodsReceiptViewCommand = new RelayCommand(LoadHistoryGoodsReceiptView);
        }

        private void LoadHistoryGoodsReceiptView()
        {
            OnPropertyChanged(nameof(WarehouseIds));
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(Suppliers));
            OnPropertyChanged(nameof(ButtonEnable));
        }

        private async void LoadHistoryGoodsReceiptLot()
        {
            try
            {
                if (!String.IsNullOrEmpty(Supplier) && String.IsNullOrEmpty(WarehouseId) && String.IsNullOrEmpty(ItemId) && String.IsNullOrEmpty(ItemName))
                {
                    var historyGoodsReceiptLots = await _apiService.GetHistoryGoodsReceiptLotsSupplierAsync(StartDate, EndDate, Supplier);
                    var viewModels = historyGoodsReceiptLots.SelectMany(g =>
                                                          g.Lots.Select(gi =>
                                                                new HistoryGoodsReceiptLotViewModel(
                                                                    gi.Item.ItemClassId,
                                                                    g.Supplier,
                                                                    gi.Item.ItemId,
                                                                    gi.Item.ItemName,
                                                                    gi.Item.Unit,
                                                                    gi.GoodsReceiptLotId,
                                                                    gi.Quantity,
                                                                    gi.Note)));
                    HistoryGoodsReceiptLots = new(viewModels);
                }
                else if ((!String.IsNullOrEmpty(ItemId) && String.IsNullOrEmpty(WarehouseId) && String.IsNullOrEmpty(Supplier))
                    || (!String.IsNullOrEmpty(WarehouseId) && String.IsNullOrEmpty(Supplier) && String.IsNullOrEmpty(ItemId) && String.IsNullOrEmpty(ItemName)))
                {
                    var historyGoodsReceiptLots = await _apiService.GetHistoryGoodsReceiptLotsAsync(WarehouseId, ItemId, StartDate, EndDate);
                    var viewModels = historyGoodsReceiptLots.SelectMany(g =>
                                                          g.Lots.Select(gi =>
                                                                new HistoryGoodsReceiptLotViewModel(
                                                                    gi.Item.ItemClassId,
                                                                    g.Supplier,
                                                                    gi.Item.ItemId,
                                                                    gi.Item.ItemName,
                                                                    gi.Item.Unit,
                                                                    gi.GoodsReceiptLotId,
                                                                    gi.Quantity,
                                                                    gi.Note)));
                    HistoryGoodsReceiptLots = new(viewModels);
                }

            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

    }
}
