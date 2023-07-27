using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueExternalViewModel : BaseViewModel
    {
        private readonly IExcelReader _excelReader;
        private readonly IApiService _apiService;
        private List<FinishedProductIssueDb> goodsIssues = new();
        private FinishedProductIssueDb GoodsIssueDb { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string GoodsIssueId { get; set; } = "";
        public string EmployeeId { get; set; } = "";
        public string Receiver { get; set; } = "";
        private readonly GoodsIssueStore _goodsIssueStore;
        public ObservableCollection<string> GoodsIssueIds => _goodsIssueStore.FinishedProductIssueIds;
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
                    var item = _itemStore.FinishedItems.First(i => i.ItemId == itemId);
                    itemName = item.ItemName;
                    Unit = item.Unit;
                    OnPropertyChanged(nameof(ItemName));
                    OnPropertyChanged(nameof(Unit));
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
                    var item = _itemStore.FinishedItems.First(i => i.ItemName == itemName);
                    itemId = item.ItemId;
                    Unit = item.Unit;
                    OnPropertyChanged(nameof(ItemId));
                    OnPropertyChanged(nameof(Unit));
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

        public GoodsIssueExternalViewModel(IExcelReader excelReader, IApiService apiService, ItemStore itemStore, GoodsIssueStore goodsIssueStore)
        {
            _excelReader = excelReader;
            _apiService = apiService;
            _itemStore = itemStore;
            _goodsIssueStore = goodsIssueStore;

            ImportGoodsIssuesCommand = new RelayCommand(ImportGoodsIssue);
            LoadGoodsIssueExternalCommand = new RelayCommand(LoadGoodsIssueExternalView);
            SaveIssueByHandCommand = new RelayCommand(SaveIssueByHand);
            CreateEntryCommand = new RelayCommand(CreateEntry);
        }

        private void LoadGoodsIssueExternalView()
        {
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(Units));
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
            if (SelectedEntry is not null)
            {
                GoodsIssueDb.Entries.Remove(GoodsIssueDb.Entries.First(x => x.ItemId == SelectedEntry.ItemId));
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
        private void SaveIssueByHand()
        {
            if (GoodsIssueIds.Contains(GoodsIssueId))
            {
                ShowErrorMessage($"Mã phiếu xuất đã tồn tại.");
            }
            else
            {
                var NewGoodsIssueByHand = new FinishedProductIssueDb(GoodsIssueId, DateTime.Now, EmployeeId, Receiver, new List<FinishedProductIssueEntry>());
                goodsIssues.Add(NewGoodsIssueByHand);
                GoodsIssues = new ObservableCollection<GoodsIssueExternalToCreateViewModel>
                            (goodsIssues.Select(x => new GoodsIssueExternalToCreateViewModel(
                                 _apiService,
                                x.GoodsIssueId,
                                x.Timestamp,
                                x.EmployeeId,
                                x.Receiver,
                                x.Entries)));
                foreach (var goodsIssueViewModel in GoodsIssues)
                {
                    goodsIssueViewModel.GoodsIssueDeleted += OnGoodsIssueRemove;
                    goodsIssueViewModel.GoodsIssueCreated += OnGoodsIssueRemove;
                }
            }
        }
        private void ImportGoodsIssue()
        {
            try
            {
                var request = _excelReader.ReadGoodsIssueExternalRequests(FilePath, "Phieu XK TP", Date);
                goodsIssues.Add(request);
                GoodsIssues = new ObservableCollection<GoodsIssueExternalToCreateViewModel>
                        (goodsIssues.Select(x => new GoodsIssueExternalToCreateViewModel(
                             _apiService,
                            x.GoodsIssueId,
                            x.Timestamp,
                            x.EmployeeId,
                            x.Receiver,
                            x.Entries)));

                foreach (var goodsIssueViewModel in GoodsIssues)
                {
                    goodsIssueViewModel.GoodsIssueDeleted += OnGoodsIssueRemove;
                    goodsIssueViewModel.GoodsIssueCreated += OnGoodsIssueRemove;
                }
                TypeEnable = false;

            }
            catch (IOException)
            {
                ShowErrorMessage($"Vui lòng tắt file trước khi nhập vào phần mềm.");
            }
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
            }
            OnPropertyChanged();
        }
    }
}
