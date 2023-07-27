using System;
using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using CommunityToolkit.Mvvm.Input;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using System.Net.Http;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptCompletedViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
        private List<FinishedProductReceiptDto> goodsReceipts = new();
        private List<FinishedProductReceiptDto> goodsReceiptByIds = new();
        private List<FinishedProductReceiptDto> goodsReceiptByTimes = new();

        private PendingGoodsReceiptViewModel? selectedGoodsReceipt;
        private readonly GoodsReceiptStore _goodsReceiptStore;
        public ObservableCollection<string> GoodsReceiptIds => _goodsReceiptStore.FinishedProductReceiptIds;
        public string GoodsReceiptId { get; set; } = "";
        //
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime EndDate { get; set; } = DateTime.Today;
        //
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
        //
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceipts { get; set; } = new();
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceiptByIds { get; set; } = new();
        public ObservableCollection<PendingGoodsReceiptViewModel> GoodsReceiptByTimes { get; set; } = new();
        public ObservableCollection<GoodsReceiptEntryForGoodsReceiptCompletedView> Entries { get; set; } = new();
        public GoodsReceiptEntryForGoodsReceiptCompletedView? SelectedEntry { get; set; }
        public PendingGoodsReceiptViewModel? SelectedGoodsReceipt
        {
            get => selectedGoodsReceipt;
            set
            {
                selectedGoodsReceipt = value;
                if (selectedGoodsReceipt is not null)
                {
                    foreach (var goodReceipt in goodsReceiptByIds)
                    {
                        goodsReceipts.Add(goodReceipt);
                    }
                    foreach (var goodReceipt in goodsReceiptByTimes)
                    {
                        goodsReceipts.Add(goodReceipt);
                    }
                    var goodsReceipt = goodsReceipts.Last(g => g.FinishedProductReceiptId == selectedGoodsReceipt.GoodsReceiptId);
                    var lotViewModels = goodsReceipt.Entries.Select(c => new GoodsReceiptEntryForGoodsReceiptCompletedView(
                    c.Item.ItemClassId,
                    c.Item.ItemId,
                    c.Item.ItemName,
                    c.Item.Unit,
                    c.PurchaseOrderNumber,
                    c.PurchaseOrderNumber,
                    c.Quantity,
                    c.Note));

                    Entries = new(lotViewModels);
                    foreach (var entry in Entries)
                    {
                        entry.OnRemoved += DeleteEntry;
                        OnPropertyChanged(nameof(Entries));
                    }
                }
            }
        }
        public ICommand LoadCompletedGoodsReceiptsCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand LoadGoodsReceiptViewCommand { get; set; }
        public ICommand CreateEntryCommand { get; set; }
        public GoodsReceiptCompletedViewModel(IApiService apiService, IDatabaseSynchronizationService databaseSynchronizationService, GoodsReceiptStore goodsReceiptStore, ItemStore itemStore)
        {
            _apiService = apiService;
            _databaseSynchronizationService = databaseSynchronizationService;
            _goodsReceiptStore = goodsReceiptStore;
            _itemStore = itemStore;

            LoadCompletedGoodsReceiptsCommand = new RelayCommand(LoadCompletedGoodsReceiptsAsync);
            UpdateCommand = new RelayCommand(UpdateAsync);
            LoadGoodsReceiptViewCommand = new RelayCommand(LoadGoodsReceiptView);
            CreateEntryCommand = new RelayCommand(CreateEntryAsync);
        }

        private void LoadGoodsReceiptView()
        {
            _databaseSynchronizationService.SynchronizeGoodReceiptsData();
            OnPropertyChanged(nameof(GoodsReceiptIds));
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(Units));
        }

        public async void LoadCompletedGoodsReceiptsAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(GoodsReceiptId))
                {
                    goodsReceiptByIds = new();
                    var goodsReceiptById = await _apiService.GetFinishedProductReceiptsByIdAsync(GoodsReceiptId);
                    goodsReceiptByIds.Add(goodsReceiptById);
                    var goodsReceiptByIdViewModels = goodsReceiptByIds.Select(g =>
                        new PendingGoodsReceiptViewModel(g.FinishedProductReceiptId,
                                                         g.Timestamp,
                                                         g.Employee.EmployeeName));
                    GoodsReceiptByIds = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptByIdViewModels);
                    GoodsReceipts = GoodsReceiptByIds;
                    Entries = new();
                    //GoodsReceiptReceivingUpdated += LoadUncompletedGoodsReceiptsAsync;
                }
                else
                {
                    var goodsReceiptByTime = await _apiService.GetFinishedProductReceiptsByTimeAsync(StartDate, EndDate);
                    goodsReceiptByTimes = goodsReceiptByTime.ToList();
                    var goodsReceiptByTimeViewModels = goodsReceiptByTime.Select(g =>
                        new PendingGoodsReceiptViewModel(g.FinishedProductReceiptId,
                                                         g.Timestamp,
                                                         g.Employee.EmployeeName));
                    GoodsReceiptByTimes = new ObservableCollection<PendingGoodsReceiptViewModel>(goodsReceiptByTimeViewModels);
                    GoodsReceipts = GoodsReceiptByTimes;
                    Entries = new();
                    //GoodsReceiptReceivedUpdated += LoadCompletedGoodsReceiptsAsync;
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }
       
        public async void UpdateAsync()
        {
            var fixDto = Entries.Select(x => new FixCompletedGoodsReceiptDto(
                x.ItemId,
                x.PurchaseOrderNumber,
                x.NewPurchaseOrderNumber,
                x.Unit,
                x.Quantity));
            try
            {
                if (SelectedGoodsReceipt is not null)
                {
                    await _apiService.FixFinishedProductReceiptsAsync(SelectedGoodsReceipt.GoodsReceiptId, fixDto);
                    Entries = new();
                    LoadGoodsReceiptView();
                    if (GoodsReceiptId == SelectedGoodsReceipt.GoodsReceiptId)
                    {
                        GoodsReceiptByIds = new();
                        LoadCompletedGoodsReceiptsAsync();
                        GoodsReceiptId = "";
                    }
                    else
                    {
                        GoodsReceiptByTimes = new();
                        LoadCompletedGoodsReceiptsAsync();
                        //GoodsReceiptReceivedUpdated?.Invoke();
                    }
                    MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            catch (DuplicateEntityException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Phiếu xuất kho đã tồn tại.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: " + ex.Message);
            }

        }

        public async void DeleteEntry()
        {
            if (selectedGoodsReceipt is not null)
            {
                if (SelectedEntry is not null)
                {
                    if (MessageBox.Show("Xác nhận xóa PO " + SelectedEntry.NewPurchaseOrderNumber + "Mã hàng " + SelectedEntry.ItemId, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            await _apiService.RemovedFinishedProductReceiptEntryAsync(selectedGoodsReceipt.GoodsReceiptId, SelectedEntry.ItemId, SelectedEntry.Unit, SelectedEntry.NewPurchaseOrderNumber);
                            LoadCompletedGoodsReceiptsAsync();
                            LoadGoodsReceiptView();
                        }
                        catch (HttpRequestException)
                        {
                            ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
                        }
                        catch (DuplicateEntityException)
                        {
                            ShowErrorMessage("Đã có lỗi xảy ra: Phiếu xuất kho đã tồn tại.");
                        }
                        catch (Exception ex)
                        {
                            ShowErrorMessage("Đã có lỗi xảy ra: " + ex.Message);
                        }
                        LoadGoodsReceiptView();
                    }
                    else { }
                }
            }
        }

        public async void CreateEntryAsync()
        {
            var newEntryDto = new CreateFinishedProductReceiptEntryDto(PurchaseOrderNumber, ItemId, Unit, Quantity, Note);
            if (selectedGoodsReceipt is not null)
            {
                if (MessageBox.Show("Xác nhận thêm dòng PO " + PurchaseOrderNumber + " Mã hàng " + ItemId, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _apiService.AddFinishedProductReceiptEntryAsync(selectedGoodsReceipt.GoodsReceiptId, newEntryDto);
                        LoadCompletedGoodsReceiptsAsync();
                        PurchaseOrderNumber = "";
                        ItemId = "";
                        Quantity = 0;
                        Note = "";
                        LoadGoodsReceiptView();
                    }
                    catch (HttpRequestException)
                    {
                        ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
                    }
                    catch (DuplicateEntityException)
                    {
                        ShowErrorMessage("Đã có lỗi xảy ra: Phiếu xuất kho đã tồn tại.");
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Đã có lỗi xảy ra: " + ex.Message);
                    }
                    LoadGoodsReceiptView();
                }
                else { }
            }
        }
    }
}
