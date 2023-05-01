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
        public string WarehouseName { get; set; } = "";
        public string Receiver { get; set; } = "";
        public string PurchaseOrderNumber { get; set; } = "";
        public ObservableCollection<HistoryGoodsIssueLotViewModel> HistoryGoodsIssueLots { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.ItemIds;
        public ObservableCollection<string> ItemNames => _itemStore.ItemNames;
        public ObservableCollection<string> WarehouseNames => _warehouseStore.WarehouseNames;
        public ObservableCollection<string> PurchaseOrderNumbers => _itemLotStore.PurchaseOrderNumbers;
        public ObservableCollection<string> Receivers => _goodsIssueStore.Receivers;

        public ICommand LoadHistoryGoodsIssueLotCommand { get; set; }
        public HistoryGoodsIssueViewModel(IApiService apiService, IMapper mapper, ItemStore itemStore, ItemLotStore itemLotStore, WarehouseStore warehouseStore, GoodsIssueStore goodsIssueStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _itemStore = itemStore;
            _itemLotStore = itemLotStore;
            _warehouseStore = warehouseStore;
            _goodsIssueStore = goodsIssueStore;

            LoadHistoryGoodsIssueLotCommand = new RelayCommand(LoadHistoryGoodsIssueLot);
        }

        private async void LoadHistoryGoodsIssueLot()
        {
            try
            {
                var historyGoodsIssueLots = await _apiService.GetHistoryGoodsIssueLotsAsync(WarehouseName, ItemId, ItemName, StartDate, EndDate, Receiver, PurchaseOrderNumber);

                var viewModels = _mapper.Map<IEnumerable<GoodsIssueDto>, IEnumerable<HistoryGoodsIssueLotViewModel>>(historyGoodsIssueLots);
                HistoryGoodsIssueLots = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

       
    }
}
