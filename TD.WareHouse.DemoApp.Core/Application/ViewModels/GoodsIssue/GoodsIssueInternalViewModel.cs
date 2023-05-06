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
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using GoodsIssuesModels = TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueInternalViewModel : BaseViewModel
    {
        private readonly IExcelReader _excelReader;
        private readonly IExportRequestsToGoodsIssuesConverter _converter;
        private readonly IApiService _apiService;
        private readonly GoodsIssueStore _goodsIssueStore;

        private GoodsIssueInternalToCreateViewModel? selectedGoodsIssue;
        private List<GoodsIssueDto> goodsIssues = new();

        public DateTime Date { get; set; } = DateTime.Now;
        public string FilePath { get; set; } = "";
        public string? GoodsIssueId
        {
            get => selectedGoodsIssue?.GoodsIssueId;
            set
            {
                if (selectedGoodsIssue is not null && value is not null)
                {
                    selectedGoodsIssue.GoodsIssueId = value;
                }
            }
        }

        public string? Receiver
        {
            get => selectedGoodsIssue?.Receiver;
            set
            {
                if (selectedGoodsIssue is not null && value is not null)
                {
                    selectedGoodsIssue.Receiver = value;
                }
            }
        }
       

        public GoodsIssueInternalToCreateViewModel? SelectedGoodsIssue
        {
            get => selectedGoodsIssue;
            set
            {
                selectedGoodsIssue = value;
                if (selectedGoodsIssue is not null)
                {
                    var goodsIssue = goodsIssues.First(g => g.GoodsIssueId == selectedGoodsIssue.GoodsIssueId);
                    var entries = goodsIssue.Entries.Select(e => new GoodsIssueEntryForGoodsIssueInternalView(
                        e.Item.ItemId,
                        e.Item.ItemName,
                        e.RequestedQuantity,
                        e.Item.Unit));

                    Entries = new(entries);
                }
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Receivers => _goodsIssueStore.Receivers;
        public ObservableCollection<GoodsIssueEntryForGoodsIssueInternalView> Entries { get; set; } = new();

        public ObservableCollection<GoodsIssueInternalToCreateViewModel> GoodsIssues { get; set; } = new();

        public ICommand ImportGoodsIssuesCommand { get; set; }

        public GoodsIssueInternalViewModel(IExcelReader excelReader, IExportRequestsToGoodsIssuesConverter converter, IApiService apiService, GoodsIssueStore goodsIssueStore)
        {
            _excelReader = excelReader;
            _converter = converter;
            _apiService = apiService;
            _goodsIssueStore = goodsIssueStore;


            ImportGoodsIssuesCommand = new RelayCommand(ImportGoodsIssueAsync);
        }

        private async void ImportGoodsIssueAsync()
        {
            try
            {
                var requests = _excelReader.ReadExportRequests(FilePath, Date);
                List<GoodsIssueDto> newGoodsIssues = new();
                try
                {
                    //newGoodsIssues = await _converter.ConvertAsync(requests);
                }
                catch (EntityNotFoundException e)
                {
                    ShowErrorMessage($"Không tìm thấy mã vật tư {e.EntityId}. Vui lòng tạo mới.");
                }

                foreach (var goodsIssue in newGoodsIssues)
                {
                    var existingGoodsIssue = goodsIssues.FirstOrDefault(g => g.GoodsIssueId == goodsIssue.GoodsIssueId);

                    if (existingGoodsIssue is not null)
                    {
                        existingGoodsIssue.Entries.AddRange(goodsIssue.Entries);
                    }
                    else
                    {
                        goodsIssues.Add(goodsIssue);
                    }
                    GoodsIssues = new ObservableCollection<GoodsIssueInternalToCreateViewModel>
                        (goodsIssues.Select(x => new GoodsIssueInternalToCreateViewModel(
                             _apiService,
                            x.GoodsIssueId,
                            x.Employee.EmployeeId,
                            x.Employee.EmployeeName,
                            x.Receiver,
                            x.PurchaseOrderNumber,
                            x.Entries)));

                    foreach (var goodsIssueViewModel in GoodsIssues)
                    {
                        goodsIssueViewModel.GoodsIssueDeleted += OnGoodsIssueRemove;
                        goodsIssueViewModel.GoodsIssueCreated += OnGoodsIssueRemove;
                    }
                }
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
            }
        }
    }
}
