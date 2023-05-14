using CommunityToolkit.Mvvm.Input;
using AutoMapper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueInternalViewModel : BaseViewModel
    {
        private readonly IExcelReader _excelReader;
        private readonly IApiService _apiService;
        private readonly GoodsIssueStore _goodsIssueStore;
        public ObservableCollection<string> Receivers => _goodsIssueStore.Receivers;
        private List<GoodsIssueDb> goodsIssues = new();
        private GoodsIssueDb GoodsIssueDb { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string GoodsIssueId { get; set; } = "";
        public string EmployeeId { get; set; } = "";
        public string Receiver { get; set; } = "";

        private readonly ItemStore _itemStore;
        public ObservableCollection<string> ItemIds => _itemStore.ItemIds;
        public ObservableCollection<string> ItemNames => _itemStore.ItemNames;
        public ObservableCollection<string> Units => _itemStore.Units;
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
                    var item = _itemStore.Items.First(i => i.ItemId == itemId);
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
                    var item = _itemStore.Items.First(i => i.ItemName == itemName);
                    itemId = item.ItemId;
                    Unit = item.Unit;
                    OnPropertyChanged(nameof(ItemId));
                    OnPropertyChanged(nameof(Unit));
                }
            }
        }

        public string Unit { get; set; } = "";
        public double RequestedQuantity { get; set; } = 0;
        public bool TypeEnable { get; set; } = false;

        private GoodsIssueInternalToCreateViewModel? selectedGoodsIssue;
        public DateTime Date { get; set; } = DateTime.Now;
        public string FilePath { get; set; } = "";
        
        public GoodsIssueInternalToCreateViewModel? SelectedGoodsIssue
        {
            get => selectedGoodsIssue;
            set
            {
                selectedGoodsIssue = value;
                if (selectedGoodsIssue is not null)
                {
                    TypeEnable = true;
                    GoodsIssueDb = goodsIssues.First(g => g.GoodsIssueId == selectedGoodsIssue.GoodsIssueId);
                    var entries = GoodsIssueDb.Entries.Select(e => new GoodsIssueEntryForGoodsIssueInternalView(
                        e.ItemId,
                        e.ItemName,
                        e.RequestedQuantity,
                        e.Unit));
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
        public GoodsIssueEntryForGoodsIssueInternalView? SelectedEntry { get; set; }
        public ObservableCollection<GoodsIssueEntryForGoodsIssueInternalView> Entries { get; set; }

        public ObservableCollection<GoodsIssueInternalToCreateViewModel> GoodsIssues { get; set; } = new();
        
        public ICommand SaveIssueByHandCommand { get; set; }
        public ICommand ImportGoodsIssuesCommand { get; set; }
        public ICommand LoadGoodsIssueInternalCommand { get; set; }
        public ICommand CreateEntryCommand { get; set; }

        public GoodsIssueInternalViewModel(IExcelReader excelReader, IApiService apiService, ItemStore itemStore, GoodsIssueStore goodsIssueStore)
        {
            _excelReader = excelReader;
            _apiService = apiService;
            _itemStore = itemStore;
            _goodsIssueStore = goodsIssueStore;

            ImportGoodsIssuesCommand = new RelayCommand(ImportGoodsIssueAsync);
            LoadGoodsIssueInternalCommand = new RelayCommand(LoadGoodsIssueInternalView);
            SaveIssueByHandCommand = new RelayCommand(SaveIssueByHand);
            CreateEntryCommand = new RelayCommand(CreateEntry);
        }

        private void LoadGoodsIssueInternalView()
        {
            OnPropertyChanged(nameof(Receivers));
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(Units));
        }
        private void CreateEntry()
        {
            GoodsIssueDb.Entries.Add(new GoodsIssueEntry(ItemId, ItemName, Unit, RequestedQuantity));
            GoodsIssueDb = goodsIssues.First(g => g.GoodsIssueId == selectedGoodsIssue.GoodsIssueId);
            var entries = GoodsIssueDb.Entries.Select(e => new GoodsIssueEntryForGoodsIssueInternalView(
                e.ItemId,
                e.ItemName,
                e.RequestedQuantity,
                e.Unit));
            Entries = new(entries);
            foreach (var entry in Entries)
            {
                entry.OnRemoved += DeleteRow;
                OnPropertyChanged(nameof(Entries));
            }
        }
        private void DeleteRow()
        {
            GoodsIssueDb.Entries.Remove(GoodsIssueDb.Entries.First(x => x.ItemId == SelectedEntry.ItemId));
            var entries = GoodsIssueDb.Entries.Select(e => new GoodsIssueEntryForGoodsIssueInternalView(
                e.ItemId,
                e.ItemName,
                e.RequestedQuantity,
                e.Unit));
            Entries = new(entries);
            foreach (var entry in Entries)
            {
                entry.OnRemoved += DeleteRow;
                OnPropertyChanged(nameof(Entries));
            }
        }
        private void SaveIssueByHand()
        {
            var NewGoodsIssueByHand = new GoodsIssueDb(GoodsIssueId, DateTime.Now, EmployeeId, Receiver, new List<GoodsIssueEntry>());
            goodsIssues.Add(NewGoodsIssueByHand);
            GoodsIssues = new ObservableCollection<GoodsIssueInternalToCreateViewModel>
                        (goodsIssues.Select(x => new GoodsIssueInternalToCreateViewModel(
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
        private async void ImportGoodsIssueAsync()
        {
            try
            {
                var request = _excelReader.ReadExportRequests(FilePath, "Phieu XK NVL", Date);
                goodsIssues.Add(request);
                GoodsIssues = new ObservableCollection<GoodsIssueInternalToCreateViewModel>
                        (goodsIssues.Select(x => new GoodsIssueInternalToCreateViewModel(
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
                var goodsIssueViewModel = (GoodsIssueInternalToCreateViewModel)sender;

                GoodsIssues.Remove(goodsIssueViewModel);

                var goodsIssue = goodsIssues.First(g => g.GoodsIssueId == goodsIssueViewModel.GoodsIssueId);
                goodsIssues.Remove(goodsIssue);
                Entries = new();
                ItemId = "";
                ItemName = "";
                Unit = "";
                RequestedQuantity = 0;
                TypeEnable = false;
                FilePath = "";
            }
            OnPropertyChanged();
        }
    }
}
