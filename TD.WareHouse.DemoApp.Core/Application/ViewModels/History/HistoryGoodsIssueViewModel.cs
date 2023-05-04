using AutoMapper;
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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.History
{
    public class HistoryGoodsIssueViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        private readonly ItemLotStore _itemLotStore;
        private readonly WarehouseStore _warehouseStore;
        private readonly GoodsIssueStore _goodsIssueStore;
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-30).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        private string itemId = "";
        private string itemName = "";

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                var item = _itemStore.Items.First(i => i.ItemId == itemId);
                itemName = item.ItemName;
                OnPropertyChanged(nameof(ItemName));
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
                var item = _itemStore.Items.First(i => i.ItemName == itemName);
                itemId = item.ItemId;
                OnPropertyChanged(nameof(ItemId));
            }
        }
        public string WarehouseId { get; set; } = "";
        public string Receiver { get; set; } = "";
        public string PurchaseOrderNumber { get; set; } = "";
        public ObservableCollection<HistoryGoodsIssueLotViewModel> HistoryGoodsIssueLots { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.ItemIds;
        public ObservableCollection<string> ItemNames => _itemStore.ItemNames;
        public ObservableCollection<string> WarehouseIds => _warehouseStore.WarehouseIds;
        public ObservableCollection<string> PurchaseOrderNumbers => _itemLotStore.PurchaseOrderNumbers;
        public ObservableCollection<string> Receivers => _goodsIssueStore.Receivers;

        public ICommand LoadHistoryGoodsIssueLotCommand { get; set; }
        public ICommand LoadHistoryGoodsIssueViewCommand { get; set; }
        public HistoryGoodsIssueViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore, ItemLotStore itemLotStore, WarehouseStore warehouseStore, GoodsIssueStore goodsIssueStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
            _itemLotStore = itemLotStore;
            _warehouseStore = warehouseStore;
            _goodsIssueStore = goodsIssueStore;

            LoadHistoryGoodsIssueLotCommand = new RelayCommand(LoadHistoryGoodsIssueLot);
            LoadHistoryGoodsIssueViewCommand = new RelayCommand(LoadHistoryGoodsIssueView);
        }

        private void LoadHistoryGoodsIssueView()
        {
            OnPropertyChanged(nameof(WarehouseIds));
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(PurchaseOrderNumbers));
            OnPropertyChanged(nameof(Receivers));
        }
        private async void LoadHistoryGoodsIssueLot()
        {
            try
            {
                var historyGoodsIssueLots = await _apiService.GetHistoryGoodsIssueLotsAsync(WarehouseId, ItemId, ItemName, StartDate, EndDate, Receiver, PurchaseOrderNumber);
                var viewModels = historyGoodsIssueLots.SelectMany(g =>
                                                      g.Entries.SelectMany(gi =>
                                                        gi.Lots.Select(gie =>
                                                            new HistoryGoodsIssueLotViewModel(
                                                                g.Receiver,
                                                                g.PurchaseOrderNumber,
                                                                gi.Item.ItemClass.ItemClassId,
                                                                gi.Item.ItemId,
                                                                gi.Item.ItemName,
                                                                gi.Item.Unit,
                                                                gie.GoodsIssueLotId,
                                                                gie.Quantity,
                                                                gie.Note))));
                HistoryGoodsIssueLots = new ObservableCollection<HistoryGoodsIssueLotViewModel>(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

       
    }
}
