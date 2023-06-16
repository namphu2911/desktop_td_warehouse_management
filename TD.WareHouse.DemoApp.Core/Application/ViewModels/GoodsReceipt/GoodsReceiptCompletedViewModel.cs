using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptCompletedViewModel : BaseViewModel
    {
        private readonly IExcelReader _excelReader;
        private readonly IApiService _apiService;
        private readonly GoodsReceiptStore _goodsReceiptStore;
        public ObservableCollection<string> Suppliers => _goodsReceiptStore.Suppliers;
        public ObservableCollection<string> GoodsReceiptIds => _goodsReceiptStore.GoodsReceiptIds;
        private readonly ItemLotStore _itemLotStore;
        public List<string> LotIds => _itemLotStore.LotIds;
        
        private List<GoodsReceiptDb> goodsReceipts = new();
        private GoodsReceiptDb GoodsReceiptDb { get; set; }
        public string GoodsReceiptId { get; set; } = "";
        public string EmployeeId { get; set; } = "";
        public string? Supplier { get; set; } = "";

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
        public string GoodsReceiptLotId { get; set; } = "";
        public double Quantity { get; set; } = 0;
        public string PurchaseOrderNumber { get; set; } = "";
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
                    GoodsReceiptDb = goodsReceipts.First(g => g.GoodsReceiptId == selectedGoodsReceipt.GoodsReceiptId);
                    var lots = GoodsReceiptDb.Lots.Select(e => new GoodsReceiptLotForGoodsReceiptCompletedView(
                        e.GoodsReceiptLotId,
                        e.ItemId,
                        e.ItemName,
                        e.Quantity,
                        e.Unit,
                        e.PurchaseOrderNumber));
                    Lots = new(lots);
                    foreach (var lot in Lots)
                    {
                        lot.OnRemoved += DeleteRow;
                        OnPropertyChanged(nameof(Lots));
                    }
                }
                OnPropertyChanged();
            }
        }
        public GoodsReceiptLotForGoodsReceiptCompletedView? SelectedLot { get; set; }
        public ObservableCollection<GoodsReceiptLotForGoodsReceiptCompletedView> Lots { get; set; }

        public ObservableCollection<GoodsReceiptToCreateViewModel> GoodsReceipts { get; set; } = new();

        public ICommand SaveReceiptByHandCommand { get; set; }
        public ICommand ImportGoodsReceiptsCommand { get; set; }
        public ICommand LoadGoodsReceiptCommand { get; set; }
        public ICommand CreateEntryCommand { get; set; }
        public GoodsReceiptCompletedViewModel(IExcelReader excelReader, IApiService apiService, GoodsReceiptStore goodsReceiptStore, ItemLotStore itemLotStore, ItemStore itemStore)
        {
            _excelReader = excelReader;
            _apiService = apiService;
            _goodsReceiptStore = goodsReceiptStore;
            _itemLotStore = itemLotStore;
            _itemStore = itemStore;

            ImportGoodsReceiptsCommand = new RelayCommand(ImportGoodsReceiptAsync);
            LoadGoodsReceiptCommand = new RelayCommand(LoadGoodsReceiptView);
            SaveReceiptByHandCommand = new RelayCommand(SaveReceiptByHand);
            CreateEntryCommand = new RelayCommand(CreateEntry);
        }
        private void LoadGoodsReceiptView()
        {
            OnPropertyChanged(nameof(Suppliers));
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(Units));
        }
        private void CreateEntry()
        {
            if(LotIds.Contains(GoodsReceiptLotId))
            {
                ShowErrorMessage($"Mã lô đã tồn tại.");
            }
            else
            {
                GoodsReceiptDb.Lots.Add(new GoodsReceiptLot(GoodsReceiptLotId, ItemId, ItemName, Quantity, Unit, PurchaseOrderNumber));
                GoodsReceiptDb = goodsReceipts.First(g => g.GoodsReceiptId == selectedGoodsReceipt.GoodsReceiptId);
                var lots = GoodsReceiptDb.Lots.Select(e => new GoodsReceiptLotForGoodsReceiptCompletedView(
                            e.GoodsReceiptLotId,
                            e.ItemId,
                            e.ItemName,
                            e.Quantity,
                            e.Unit,
                            e.PurchaseOrderNumber));
                Lots = new(lots);
                foreach (var lot in Lots)
                {
                    lot.OnRemoved += DeleteRow;
                    OnPropertyChanged(nameof(Lots));
                }
            }
            
        }
        private void DeleteRow()
        {
            GoodsReceiptDb.Lots.Remove(GoodsReceiptDb.Lots.First(x => x.ItemId == SelectedLot.GoodsReceiptLotId));
            var lots = GoodsReceiptDb.Lots.Select(e => new GoodsReceiptLotForGoodsReceiptCompletedView(
                        e.GoodsReceiptLotId,
                        e.ItemId,
                        e.ItemName,
                        e.Quantity,
                        e.Unit,
                        e.PurchaseOrderNumber));
            Lots = new(lots);
            foreach (var lot in Lots)
            {
                lot.OnRemoved += DeleteRow;
                OnPropertyChanged(nameof(Lots));
            }
        }
        private void SaveReceiptByHand()
        {
            if (GoodsReceiptIds.Contains(GoodsReceiptId))
            {
                ShowErrorMessage($"Mã phiếu nhập đã tồn tại.");
            }
            else
            {
                var NewGoodsReceiptByHand = new GoodsReceiptDb(GoodsReceiptId, Supplier, EmployeeId, new List<GoodsReceiptLot>());
                goodsReceipts.Add(NewGoodsReceiptByHand);
                GoodsReceipts = new ObservableCollection<GoodsReceiptToCreateViewModel>
                            (goodsReceipts.Select(x => new GoodsReceiptToCreateViewModel(
                                 _apiService,
                                x.GoodsReceiptId,
                                x.EmployeeId,
                                x.Supplier,
                                x.Lots)));
                foreach (var goodsReceiptViewModel in GoodsReceipts)
                {
                    goodsReceiptViewModel.GoodsReceiptDeleted += OnGoodsReceiptRemove;
                    goodsReceiptViewModel.GoodsReceiptCreated += OnGoodsReceiptSave;
                }
            }
            
        }
        private async void ImportGoodsReceiptAsync()
        {
            try
            {
                var request = _excelReader.ReadReceiptExportRequests(FilePath, "frmInspectingRefPrintPNK", Date);
                goodsReceipts.Add(request);
                GoodsReceipts = new ObservableCollection<GoodsReceiptToCreateViewModel>
                        (goodsReceipts.Select(x => new GoodsReceiptToCreateViewModel(
                             _apiService,
                            x.GoodsReceiptId,
                            x.EmployeeId,
                            x.Supplier,
                            x.Lots)));
                foreach (var goodsReceiptViewModel in GoodsReceipts)
                {
                    goodsReceiptViewModel.GoodsReceiptDeleted += OnGoodsReceiptRemove;
                    goodsReceiptViewModel.GoodsReceiptCreated += OnGoodsReceiptSave;
                }
                TypeEnable = false;

            }
            catch (IOException)
            {
                ShowErrorMessage($"Vui lòng tắt file trước khi nhập vào phần mềm.");
            }
        }

        private void OnGoodsReceiptRemove(object? sender, EventArgs args)
        {
            if (sender is not null)
            {
                var goodsReceiptViewModel = (GoodsReceiptToCreateViewModel)sender;

                GoodsReceipts.Remove(goodsReceiptViewModel);

                var goodsReceipt = goodsReceipts.First(g => g.GoodsReceiptId == goodsReceiptViewModel.GoodsReceiptId);
                goodsReceipts.Remove(goodsReceipt);
                Lots = new();
                ItemId = "";
                ItemName = "";
                Unit = "";
                Quantity = 0;
                GoodsReceiptLotId = "";
                PurchaseOrderNumber = "";
                TypeEnable = false;
                FilePath = "";
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
                GoodsReceiptLotId = "";
                PurchaseOrderNumber = "";
                TypeEnable = false;
                FilePath = "";
            }
            OnPropertyChanged();
        }
    }
}
