using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueExternalProgressViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
        private List<FinishedProductIssueDto> goodsIssueByIds = new();
        private List<FinishedProductIssueDto> goodsIssueByTimes = new();
        private List<FinishedProductIssueDto> goodsIssuesTotal = new();
        private PendingGoodsIssueViewModel? selectedGoodsIssue;
        //
        private readonly GoodsIssueStore _goodsIssueStore;
        public ObservableCollection<string> GoodsIssueIds => _goodsIssueStore.FinishedProductIssueIds;
        public string GoodsIssueId { get; set; } = "";
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
        //
        public ObservableCollection<PendingGoodsIssueViewModel> GoodsIssues { get; set; } = new();
        public ObservableCollection<PendingGoodsIssueViewModel> GoodsIssueByIds { get; set; } = new();
        public ObservableCollection<PendingGoodsIssueViewModel> GoodsIssueByTimes { get; set; } = new();
        public ObservableCollection<GoodsIssueEntryForGoodsIssueExternalProgressViewModel> Entries { get; set; } = new();
        public GoodsIssueEntryForGoodsIssueExternalProgressViewModel? SelectedEntry { get; set; }
        public PendingGoodsIssueViewModel? SelectedGoodsIssue
        {
            get => selectedGoodsIssue;
            set
            {
                selectedGoodsIssue = value;
                if (selectedGoodsIssue is not null)
                {
                    foreach (var goodIssue in goodsIssueByIds)
                    {
                        goodsIssuesTotal.Add(goodIssue);
                    }
                    foreach (var goodIssue in goodsIssueByTimes)
                    {
                        goodsIssuesTotal.Add(goodIssue);
                    }
                    FinishedProductIssueDto goodsIssue = goodsIssuesTotal.Last(g => g.FinishedProductIssueId == selectedGoodsIssue.GoodsIssueId);

                    var entries = goodsIssue.Entries.Select(e =>
                    new GoodsIssueEntryForGoodsIssueExternalProgressViewModel(
                        e.PurchaseOrderNumber,
                        e.Quantity,
                        e.Note,
                        e.Item.ItemClassId,
                        e.Item.ItemId,
                        e.Item.ItemName,
                        e.Item.Unit));

                    Entries = new(entries);
                    foreach (var entry in Entries)
                    {
                        entry.OnRemoved += DeleteEntry;
                        OnPropertyChanged(nameof(Entries));
                    }
                }
            }
        }
        //public ICommand UpdateCommand { get; set; }
        public ICommand LoadExternalGoodsIssuesProgressCommand { get; set; }
        public ICommand LoadGoodsIssueProgressViewCommand { get; set; }
        public ICommand CreateEntryCommand { get; set; }
        public GoodsIssueExternalProgressViewModel(IApiService apiService, IDatabaseSynchronizationService databaseSynchronizationService, GoodsIssueStore goodsIssueStore, ItemStore itemStore)
        {
            _apiService = apiService;
            _databaseSynchronizationService = databaseSynchronizationService;
            _goodsIssueStore = goodsIssueStore;
            _itemStore = itemStore;

            LoadExternalGoodsIssuesProgressCommand = new RelayCommand(LoadExternalGoodsIssuesAsync);

            //UpdateCommand = new RelayCommand(UpdateAsync);
            LoadGoodsIssueProgressViewCommand = new RelayCommand(LoadGoodsIssueProgressView);
            CreateEntryCommand = new RelayCommand(CreateEntryAsync);
        }
        private void LoadGoodsIssueProgressView()
        {
            _databaseSynchronizationService.SynchronizeGoodIssuesData();
            OnPropertyChanged(nameof(GoodsIssueIds));
            OnPropertyChanged(nameof(ItemIds));
            OnPropertyChanged(nameof(ItemNames));
            OnPropertyChanged(nameof(Units));
        }

        public async void LoadExternalGoodsIssuesAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(GoodsIssueId))
                {
                    goodsIssueByIds = new();
                    var goodsIssueById = await _apiService.GetFinishedProductIssuesByIdAsync(GoodsIssueId);
                    goodsIssueByIds.Add(goodsIssueById);
                    var goodsIssueByIdViewModels = goodsIssueByIds.Select(g =>
                          new PendingGoodsIssueViewModel(g.FinishedProductIssueId,
                                                         g.Timestamp,
                                                         g.Employee.EmployeeName,
                                                         g.Receiver));
                    GoodsIssueByIds = new ObservableCollection<PendingGoodsIssueViewModel>(goodsIssueByIdViewModels);
                    GoodsIssues = GoodsIssueByIds;
                    Entries = new();
                }
                else
                {
                    var goodsIssueByTime = await _apiService.GetFinishedProductIssuesByTimeAsync(StartDate, EndDate);
                    goodsIssueByTimes = goodsIssueByTime.ToList();
                    var goodsIssueByTimeViewModels = goodsIssueByTime.Select(g =>
                        new PendingGoodsIssueViewModel(g.FinishedProductIssueId,
                                                         g.Timestamp,
                                                         g.Employee.EmployeeName,
                                                         g.Receiver));
                    GoodsIssueByTimes = new ObservableCollection<PendingGoodsIssueViewModel>(goodsIssueByTimeViewModels);
                    GoodsIssues = GoodsIssueByTimes;
                    Entries = new();
                }

            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        //public async void UpdateAsync()
        //{
        //    if (SelectedGoodsIssue is not null)
        //    {
        //        var fixDto = Entries.Select(x => new FixGoodsIssueEntryMaterialsDto(
        //        x.ItemId,
        //        x.Unit,
        //        x.RequestedQuantity));

        //        try
        //        {

        //            await _apiService.FixGoodsIssueEntryMaterialsAsync(SelectedGoodsIssue.GoodsIssueId, fixDto);
        //            Entries = new();
        //            MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }

        //        catch (HttpRequestException)
        //        {
        //            ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
        //        }
        //        catch (DuplicateEntityException)
        //        {
        //            ShowErrorMessage("Đã có lỗi xảy ra: Phiếu xuất kho đã tồn tại.");
        //        }
        //        catch (Exception ex)
        //        {
        //            ShowErrorMessage("Đã có lỗi xảy ra: " + ex.Message);
        //        }
        //    }
        //}

        public async void DeleteEntry()
        {
            if (selectedGoodsIssue is not null)
            {
                if (SelectedEntry is not null)
                {
                    if (MessageBox.Show("Xác nhận xóa PO " + SelectedEntry.PurchaseOrderNumber + "Mã hàng " + SelectedEntry.ItemId, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            await _apiService.RemovedFinishedProductIssueEntryAsync(selectedGoodsIssue.GoodsIssueId, SelectedEntry.ItemId, SelectedEntry.Unit, SelectedEntry.PurchaseOrderNumber);
                            LoadExternalGoodsIssuesAsync();
                            LoadGoodsIssueProgressView();
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
                        LoadGoodsIssueProgressView();
                        }
                    else { }
                }
            }
        }

        public async void CreateEntryAsync()
        {
            var newEntryDto = new CreateExternalGoodsIssueEntryDto(PurchaseOrderNumber, Quantity, ItemId, Unit, Note);
            if (selectedGoodsIssue is not null)
            {
                if (MessageBox.Show("Xác nhận thêm dòng PO " + PurchaseOrderNumber + " Mã hàng " + ItemId, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _apiService.AddFinishedProductIssueEntryAsync(selectedGoodsIssue.GoodsIssueId, newEntryDto);
                        LoadExternalGoodsIssuesAsync();
                        PurchaseOrderNumber = "";
                        ItemId = "";
                        Quantity = 0;
                        Note = "";
                        LoadGoodsIssueProgressView();
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
                    LoadGoodsIssueProgressView();
                }
                else { }
            }
        }
    }
}
