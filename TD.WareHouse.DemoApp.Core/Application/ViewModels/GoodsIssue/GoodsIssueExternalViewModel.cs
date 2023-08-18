using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueExternalViewModel : BaseViewModel
    {
        private readonly IExcelReader _excelReader;
        private readonly IApiService _apiService;
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
        private readonly GoodsIssueStore _goodsIssueStore;
        public ObservableCollection<string> GoodsIssueIds => _goodsIssueStore.FinishedProductIssueIds;
        private readonly EmployeeStore _employeeStore;
        public ObservableCollection<string> EmployeeIds => _employeeStore.EmployeeIds;
        private List<FinishedProductIssueDb> goodsIssues = new();
        private FinishedProductIssueDb GoodsIssueDb { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string GoodsIssueId { get; set; } = "";
        public string EmployeeId { get; set; } = "";
        public string Receiver { get; set; } = "";
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

        private GoodsIssueExternalToCreateViewModel? selectedGoodsIssue;

        public DateTime Date { get; set; } = DateTime.Now;
        public string FilePath { get; set; } = "";
        
        public GoodsIssueExternalToCreateViewModel? SelectedGoodsIssue
        {
            get => selectedGoodsIssue;
            set
            {
                selectedGoodsIssue = value;
                if (selectedGoodsIssue is not null)
                {
                    TypeEnable = true;
                    GoodsIssueDb = goodsIssues.First(g => g.GoodsIssueId == selectedGoodsIssue.GoodsIssueId);
                    var entries = GoodsIssueDb.Entries.Select(e => new GoodsIssueEntryForGoodsIssueExternalView(
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
        public GoodsIssueEntryForGoodsIssueExternalView? SelectedEntry { get; set; }
        public ObservableCollection<GoodsIssueEntryForGoodsIssueExternalView> Entries { get; set; } = new();

        public ObservableCollection<GoodsIssueExternalToCreateViewModel> GoodsIssues { get; set; } = new();

        public ICommand SaveIssueByHandCommand { get; set; }
        public ICommand ImportGoodsIssuesCommand { get; set; }
        public ICommand LoadGoodsIssueExternalCommand { get; set; }
        public ICommand CreateEntryCommand { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public GoodsIssueExternalViewModel(IExcelReader excelReader, IApiService apiService, IDatabaseSynchronizationService databaseSynchronizationService, ItemStore itemStore, GoodsIssueStore goodsIssueStore, EmployeeStore employeeStore)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _excelReader = excelReader;
            _apiService = apiService;
            _databaseSynchronizationService = databaseSynchronizationService;
            _itemStore = itemStore;
            _goodsIssueStore = goodsIssueStore;
            _employeeStore = employeeStore;

            ImportGoodsIssuesCommand = new RelayCommand(ImportGoodsIssue);
            LoadGoodsIssueExternalCommand = new RelayCommand(LoadGoodsIssueExternalView);
            SaveIssueByHandCommand = new RelayCommand(SaveIssueByHand);
            CreateEntryCommand = new RelayCommand(CreateEntry);
        }

        private void LoadGoodsIssueExternalView()
        {
            _databaseSynchronizationService.SynchronizeGoodIssuesData();
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(Units));
            OnPropertyChanged(nameof(GoodsIssueIds));
            OnPropertyChanged(nameof(EmployeeIds));
        }
        private void CreateEntry()
        {
            if (selectedGoodsIssue is not null)
            {
                GoodsIssueDb = goodsIssues.First(g => g.GoodsIssueId == selectedGoodsIssue.GoodsIssueId);
                GoodsIssueDb.Entries.Add(new FinishedProductIssueEntry(ItemId, ItemName, Unit, Quantity, PurchaseOrderNumber, Note));
                var entries = GoodsIssueDb.Entries.Select(e => new GoodsIssueEntryForGoodsIssueExternalView(
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
            if (SelectedEntry is not null & GoodsIssueDb.IsSaved == false)
            {
                GoodsIssueDb.Entries.Remove(GoodsIssueDb.Entries.First(x => x.ItemId == SelectedEntry!.ItemId));
                var entries = GoodsIssueDb.Entries.Select(e => new GoodsIssueEntryForGoodsIssueExternalView(
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
        private void SaveIssueByHand()
        {
            LoadGoodsIssueExternalView();
            if (GoodsIssueIds.Contains(GoodsIssueId))
            {
                ShowErrorMessage($"Mã phiếu xuất đã tồn tại.");
            }
            else
            {
                var NewGoodsIssueByHand = new FinishedProductIssueDb(GoodsIssueId, DateTime.Now, EmployeeId, Receiver, false, new List<FinishedProductIssueEntry>());
                goodsIssues.Add(NewGoodsIssueByHand);
                LoadGoodsIssueExternalView();
                foreach (var goodsIssue in goodsIssues)
                {
                    if (GoodsIssueIds.Contains(goodsIssue.GoodsIssueId))
                    {
                        goodsIssue.IsSaved = true;
                    }
                }
                GoodsIssues = new ObservableCollection<GoodsIssueExternalToCreateViewModel>
                            (goodsIssues.Select(x => new GoodsIssueExternalToCreateViewModel(
                                 _apiService,
                                x.GoodsIssueId,
                                x.Timestamp,
                                x.EmployeeId,
                                x.Receiver,
                                x.IsSaved,
                                x.Entries)));
                foreach (var goodsIssueViewModel in GoodsIssues)
                {
                    goodsIssueViewModel.GoodsIssueDeleted += OnGoodsIssueRemove;
                    goodsIssueViewModel.GoodsIssueCreated += OnGoodsIssueSave;
                    if (goodsIssueViewModel.IsSaved)
                    {
                        goodsIssueViewModel.ButtonVisibility = Visibility.Hidden;
                        goodsIssueViewModel.SavedVisibility = Visibility.Visible;
                    }
                }
            }
        }
        private void ImportGoodsIssue()
        {
            if (!String.IsNullOrEmpty(FilePath))
            {
                try
                {
                    var request = _excelReader.ReadGoodsIssueExternalRequests(FilePath, "Phieu XK TP", Date);
                    goodsIssues.Add(request);
                    foreach (var goodsIssue in goodsIssues)
                    {
                        if (GoodsIssueIds.Contains(goodsIssue.GoodsIssueId))
                        {
                            goodsIssue.IsSaved = true;
                        }
                    }
                    GoodsIssues = new ObservableCollection<GoodsIssueExternalToCreateViewModel>
                            (goodsIssues.Select(x => new GoodsIssueExternalToCreateViewModel(
                                 _apiService,
                                x.GoodsIssueId,
                                x.Timestamp,
                                x.EmployeeId,
                                x.Receiver,
                                x.IsSaved,
                                x.Entries)));

                    foreach (var goodsIssueViewModel in GoodsIssues)
                    {
                        goodsIssueViewModel.GoodsIssueDeleted += OnGoodsIssueRemove;
                        goodsIssueViewModel.GoodsIssueCreated += OnGoodsIssueSave;
                        if (goodsIssueViewModel.IsSaved)
                        {
                            goodsIssueViewModel.ButtonVisibility = Visibility.Hidden;
                            goodsIssueViewModel.SavedVisibility = Visibility.Visible;
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

        private void OnGoodsIssueRemove(object? sender, EventArgs args)
        {
            if (sender is not null)
            {
                var goodsIssueViewModel = (GoodsIssueExternalToCreateViewModel)sender;

                GoodsIssues.Remove(goodsIssueViewModel);

                var goodsIssue = goodsIssues.First(g => g.GoodsIssueId == goodsIssueViewModel.GoodsIssueId);
                goodsIssues.Remove(goodsIssue);
                Entries = new();
                ItemId = "";
                ItemName = "";
                Unit = "";
                Quantity = 0;
                PurchaseOrderNumber = "";
                Note = "";
                TypeEnable = false;
                FilePath = "";
                LoadGoodsIssueExternalView();
            }
            OnPropertyChanged();
        }

        private void OnGoodsIssueSave(object? sender, EventArgs args)
        {
            if (sender is not null)
            {
                var goodsIssueViewModel = (GoodsIssueExternalToCreateViewModel)sender;
                LoadGoodsIssueExternalView();
                ItemId = "";
                ItemName = "";
                Unit = "";
                Quantity = 0;
                PurchaseOrderNumber = "";
                Note = "";
                TypeEnable = false;
                FilePath = "";
                foreach (var goodsIssue in goodsIssues)
                {
                    if (GoodsIssueIds.Contains(goodsIssue.GoodsIssueId))
                    {
                        goodsIssue.IsSaved = true;
                    }
                }
            }
            OnPropertyChanged();
        }
    }
}
