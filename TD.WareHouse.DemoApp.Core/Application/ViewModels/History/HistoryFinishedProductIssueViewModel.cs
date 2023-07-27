﻿using AutoMapper;
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
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.History
{
    public class HistoryFinishedProductIssueViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
        private readonly IMapper _mapper;
        private readonly ItemStore _itemStore;
        private readonly ItemLotStore _itemLotStore;
        private readonly GoodsIssueStore _goodsIssueStore;
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;

        private string itemId = "";
        private string itemName = "";
        private string purchaseOrderNumber = "";
        private string receiver = "";

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
                    var item = _itemStore.FinishedItems.First(i => i.ItemId == itemId);
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
                    var item = _itemStore.FinishedItems.First(i => i.ItemName == itemName);
                    itemId = item.ItemId;
                    OnPropertyChanged(nameof(ItemId));
                }
            }
        }

        public string PurchaseOrderNumber
        {
            get
            {
                return purchaseOrderNumber;
            }
            set
            {
                purchaseOrderNumber = value;
                OnPropertyChanged();
            }
        }

        public string Receiver
        {
            get
            {
                return receiver;
            }
            set
            {
                receiver = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<HistoryFinishedProductIssueEntryViewModel> HistoryFinishedProductIssueEntries { get; set; } = new();
        public ObservableCollection<string> ItemIds => _itemStore.FinishedItemIds;
        public ObservableCollection<string> ItemNames => _itemStore.FinishedItemNames;
        public ObservableCollection<string> PurchaseOrderNumbers => _itemLotStore.FinishedPurchaseOrderNumbers;
        public ObservableCollection<string> Receivers => _goodsIssueStore.FinishedProductIssueReceivers;

        public ICommand LoadHistoryGoodsIssueEntryCommand { get; set; }
        public ICommand LoadHistoryGoodsIssueViewCommand { get; set; }

        public HistoryFinishedProductIssueViewModel(IApiService apiService, IDatabaseSynchronizationService databaseSynchronizationService, IMapper mapper, ItemStore itemStore, ItemLotStore itemLotStore, GoodsIssueStore goodsIssueStore)
        {
            _apiService = apiService;
            _databaseSynchronizationService = databaseSynchronizationService;
            _mapper = mapper;
            _itemStore = itemStore;
            _itemLotStore = itemLotStore;
            _goodsIssueStore = goodsIssueStore;

            LoadHistoryGoodsIssueEntryCommand = new RelayCommand(LoadHistoryFinishedProductIssueEntry);
            LoadHistoryGoodsIssueViewCommand = new RelayCommand(LoadHistoryGoodsIssueView);
        }

        private void LoadHistoryGoodsIssueView()
        {
            _databaseSynchronizationService.SynchronizeItemLotsData();
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(PurchaseOrderNumbers));
            OnPropertyChanged(nameof(Receivers));
        }

        private async void LoadHistoryFinishedProductIssueEntry()
        {

            try
            {
                var historyGoodsIssueLots = await _apiService.GetHistoryFinishedProductIssueEntriesAsync(Receiver, ItemId, PurchaseOrderNumber, StartDate, EndDate);
                var viewModels = _mapper.Map<IEnumerable<FinishedProductIssueEntryDto>, IEnumerable<HistoryFinishedProductIssueEntryViewModel>>(historyGoodsIssueLots);
                HistoryFinishedProductIssueEntries = new(viewModels);
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

    }
}
