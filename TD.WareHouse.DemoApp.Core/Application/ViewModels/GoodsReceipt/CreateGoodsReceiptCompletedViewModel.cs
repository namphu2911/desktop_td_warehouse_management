﻿using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class CreateGoodsReceiptCompletedViewModel : BaseViewModel
    {
        private readonly IExcelReader _excelReader;
        private readonly IApiService _apiService;
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
        private readonly GoodsReceiptStore _goodsReceiptStore;
        public ObservableCollection<string> GoodsReceiptIds => _goodsReceiptStore.FinishedProductReceiptIds;
        private readonly EmployeeStore _employeeStore;
        public ObservableCollection<string> EmployeeIds => _employeeStore.EmployeeIds;

        private List<FinishedProductReceiptDb> FinishedProductReceipts = new();
        private FinishedProductReceiptDb FinishedProductReceiptDb { get; set; }
        public string GoodsReceiptId { get; set; } = "";
        public string EmployeeId { get; set; } = "";
        public DateTime Timestamp { get; set; } = DateTime.Now;

        private readonly ItemStore _itemStore;
        public ObservableCollection<string> ItemIds => _itemStore.FinishedItemIds;
        public ObservableCollection<string> ItemNames => _itemStore.FinishedItemNames;
        public ObservableCollection<string> Units => _itemStore.FinishedItemUnits;
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
                if (String.IsNullOrEmpty(value))
                {
                    itemName = "";
                    Unit = "";
                    OnPropertyChanged(nameof(ItemName));
                    OnPropertyChanged(nameof(Unit));
                }
                else
                {
                    if (ItemIds.Contains(itemId))
                    {
                        var item = _itemStore.FinishedItems.First(i => i.ItemId == itemId);
                        itemName = item.ItemName;
                        Unit = item.Unit;
                        OnPropertyChanged(nameof(ItemName));
                        OnPropertyChanged(nameof(Unit));
                    }
                    else { }
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
                    Unit = "";
                    OnPropertyChanged(nameof(ItemId));
                    OnPropertyChanged(nameof(Unit));
                }
                else
                {
                    if (ItemIds.Contains(itemId))
                    {
                        var item = _itemStore.FinishedItems.First(i => i.ItemName == itemName);
                        itemId = item.ItemId;
                        Unit = item.Unit;
                        OnPropertyChanged(nameof(ItemId));
                        OnPropertyChanged(nameof(Unit));
                    }
                    else { }
                }
            }
        }

        public string Unit { get; set; } = "";
        public double Quantity { get; set; } = 0;
        public string PurchaseOrderNumber { get; set; } = "";
        public string Note { get; set; } = "";
        public bool TypeEnable { get; set; } = false;
        public DateTime Date { get; set; } = DateTime.Now;
        public string FilePath { get; set; } = "";

        private GoodsReceiptToCreateViewModel? selectedGoodsReceipt;
        public GoodsReceiptToCreateViewModel? SelectedGoodsReceipt
        {
            get => selectedGoodsReceipt;
            set
            {
                selectedGoodsReceipt = value;
                if (selectedGoodsReceipt is not null)
                {
                    TypeEnable = true;
                    FinishedProductReceiptDb = FinishedProductReceipts.First(g => g.FinishedProductReceiptId == selectedGoodsReceipt.FinishedProductReceiptId);
                    var entries = FinishedProductReceiptDb.Entries.Select(e => new GoodsReceiptEntryForCreateGoodsReceiptCompletedView(
                        e.ItemId,
                        e.ItemName,
                        e.Quantity,
                        e.Unit,
                        e.PurchaseOrderNumber,
                        e.Note));
                    Entries = new(entries);
                    foreach (var entry in Entries)
                    {
                        entry.OnRemoved += DeleteRow;
                        OnPropertyChanged(nameof(Entries));
                    }
                }
                OnPropertyChanged();
            }
        }
        public GoodsReceiptEntryForCreateGoodsReceiptCompletedView? SelectedEntry { get; set; }
        public ObservableCollection<GoodsReceiptEntryForCreateGoodsReceiptCompletedView> Entries { get; set; } = new();

        public ObservableCollection<GoodsReceiptToCreateViewModel> GoodsReceipts { get; set; } = new();

        public ICommand SaveReceiptByHandCommand { get; set; }
        public ICommand ImportGoodsReceiptsCommand { get; set; }
        public ICommand LoadGoodsReceiptCommand { get; set; }
        public ICommand CreateEntryCommand { get; set; }
        public CreateGoodsReceiptCompletedViewModel(IExcelReader excelReader, IApiService apiService, IDatabaseSynchronizationService databaseSynchronizationService, GoodsReceiptStore goodsReceiptStore, ItemStore itemStore, EmployeeStore employeeStore)
        {
            _excelReader = excelReader;
            _apiService = apiService;
            _databaseSynchronizationService = databaseSynchronizationService;
            _goodsReceiptStore = goodsReceiptStore;
            _itemStore = itemStore;
            _employeeStore = employeeStore;

            ImportGoodsReceiptsCommand = new RelayCommand(ImportGoodsReceipt);
            LoadGoodsReceiptCommand = new RelayCommand(LoadGoodsReceiptView);
            SaveReceiptByHandCommand = new RelayCommand(SaveReceiptByHand);
            CreateEntryCommand = new RelayCommand(CreateEntry);
        }
        private void LoadGoodsReceiptView()
        {
            _databaseSynchronizationService.SynchronizeGoodReceiptsData();
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(Units));
            OnPropertyChanged(nameof(GoodsReceiptIds));
            OnPropertyChanged(nameof(EmployeeIds));
        }
        private void CreateEntry()
        {
            if (selectedGoodsReceipt is not null)
            {
                FinishedProductReceiptDb = FinishedProductReceipts.First(g => g.FinishedProductReceiptId == selectedGoodsReceipt.FinishedProductReceiptId);
                FinishedProductReceiptDb.Entries.Add(new FinishedProductReceiptEntry(PurchaseOrderNumber, ItemId, ItemName, Unit, Quantity, Note));
                var entries = FinishedProductReceiptDb.Entries.Select(e => new GoodsReceiptEntryForCreateGoodsReceiptCompletedView(
                         e.ItemId,
                         e.ItemName,
                         e.Quantity,
                         e.Unit,
                         e.PurchaseOrderNumber,
                         e.Note));
                Entries = new(entries);
                foreach (var entry in Entries)
                {
                    entry.OnRemoved += DeleteRow;
                    OnPropertyChanged(nameof(Entries));
                }
            }
        }
        private void DeleteRow()
        {
            if (SelectedEntry is not null & FinishedProductReceiptDb.IsSaved == false)
            {
                FinishedProductReceiptDb.Entries.Remove(FinishedProductReceiptDb.Entries.First(x => x.ItemId == SelectedEntry!.ItemId));
                var entries = FinishedProductReceiptDb.Entries.Select(e => new GoodsReceiptEntryForCreateGoodsReceiptCompletedView(
                            e.ItemId,
                            e.ItemName,
                            e.Quantity,
                            e.Unit,
                            e.PurchaseOrderNumber,
                            e.Note));
                Entries = new(entries);
                foreach (var entry in Entries)
                {
                    entry.OnRemoved += DeleteRow;
                    OnPropertyChanged(nameof(Entries));
                }
            }
            else
            {
                MessageBox.Show("Không thể xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void SaveReceiptByHand()
        {
            LoadGoodsReceiptView();
            if (GoodsReceiptIds.Contains(GoodsReceiptId))
            {
                ShowErrorMessage($"Mã phiếu nhập đã tồn tại.");
            }
            else
            {
                var NewGoodsReceiptByHand = new FinishedProductReceiptDb(GoodsReceiptId, EmployeeId, DateTime.Now, false, new List<FinishedProductReceiptEntry>());
                FinishedProductReceipts.Add(NewGoodsReceiptByHand);
                LoadGoodsReceiptView();
                foreach (var finishedProductReceipt in FinishedProductReceipts)
                {
                    if (GoodsReceiptIds.Contains(finishedProductReceipt.FinishedProductReceiptId))
                    {
                        finishedProductReceipt.IsSaved = true;
                    }
                }
                GoodsReceipts = new ObservableCollection<GoodsReceiptToCreateViewModel>
                            (FinishedProductReceipts.Select(x => new GoodsReceiptToCreateViewModel(
                                 _apiService,
                                x.FinishedProductReceiptId,
                            x.EmployeeId,
                            x.Timestamp,
                            x.IsSaved,
                            x.Entries)));
                foreach (var goodsReceiptViewModel in GoodsReceipts)
                {
                    goodsReceiptViewModel.GoodsReceiptDeleted += OnGoodsReceiptRemove;
                    goodsReceiptViewModel.GoodsReceiptCreated += OnGoodsReceiptSave;
                    if (goodsReceiptViewModel.IsSaved)
                    {
                        goodsReceiptViewModel.ButtonVisibility = Visibility.Hidden;
                        goodsReceiptViewModel.SavedVisibility = Visibility.Visible;
                    }

                }
            }
            
        }
        private void ImportGoodsReceipt()
        {
            if (!String.IsNullOrEmpty(FilePath))
            {
                try
                {
                    var request = _excelReader.ReadReceiptExportRequests(FilePath, "frmInspectingRefPrintPNK", Date);
                    FinishedProductReceipts.Add(request);
                    foreach (var finishedProductReceipt in FinishedProductReceipts)
                    {
                        if (GoodsReceiptIds.Contains(finishedProductReceipt.FinishedProductReceiptId))
                        {
                            finishedProductReceipt.IsSaved = true;
                        }
                    }
                    GoodsReceipts = new ObservableCollection<GoodsReceiptToCreateViewModel>
                            (FinishedProductReceipts.Select(x => new GoodsReceiptToCreateViewModel(
                                 _apiService,
                                x.FinishedProductReceiptId,
                                x.EmployeeId,
                                x.Timestamp,
                                x.IsSaved,
                                x.Entries)));
                    foreach (var goodsReceiptViewModel in GoodsReceipts)
                    {
                        goodsReceiptViewModel.GoodsReceiptDeleted += OnGoodsReceiptRemove;
                        goodsReceiptViewModel.GoodsReceiptCreated += OnGoodsReceiptSave; 
                        if (goodsReceiptViewModel.IsSaved)
                        {
                            goodsReceiptViewModel.ButtonVisibility = Visibility.Hidden;
                            goodsReceiptViewModel.SavedVisibility = Visibility.Visible;
                        }
                    }
                    TypeEnable = false;

                }
                catch (IOException)
                {
                    ShowErrorMessage($"Vui lòng tắt file trước khi nhập vào phần mềm.");
                }
            }
            else { }
        }

        private void OnGoodsReceiptRemove(object? sender, EventArgs args)
        {
            if (sender is not null)
            {
                var goodsReceiptViewModel = (GoodsReceiptToCreateViewModel)sender;

                GoodsReceipts.Remove(goodsReceiptViewModel);

                var goodsReceipt = FinishedProductReceipts.First(g => g.FinishedProductReceiptId == goodsReceiptViewModel.FinishedProductReceiptId);
                FinishedProductReceipts.Remove(goodsReceipt);
                Entries = new();
                ItemId = "";
                ItemName = "";
                Unit = "";
                Quantity = 0;
                PurchaseOrderNumber = "";
                Note = "";
                TypeEnable = false;
                FilePath = "";
                LoadGoodsReceiptView();
            }
            OnPropertyChanged();
        }

        private void OnGoodsReceiptSave(object? sender, EventArgs args)
        {
            if (sender is not null)
            {
                var goodsReceiptViewModel = (GoodsReceiptToCreateViewModel)sender;
                ItemId = "";
                ItemName = "";
                Unit = "";
                Quantity = 0;
                PurchaseOrderNumber = "";
                Note = "";
                TypeEnable = false;
                FilePath = "";
                LoadGoodsReceiptView();
                foreach (var finishedProductReceipt in FinishedProductReceipts)
                {
                    if (GoodsReceiptIds.Contains(finishedProductReceipt.FinishedProductReceiptId))
                    {
                        finishedProductReceipt.IsSaved = true;
                    }
                }
            }
            OnPropertyChanged();
        }
    }
}
