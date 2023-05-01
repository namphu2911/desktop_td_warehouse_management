using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptLotForGoodsReceiptView : BaseViewModel
    {
        private readonly IApiService _apiService;

        public string GoodsReceiptId { get; set; }
        public string EmployeeName { get; set; }
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string LotId { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? LocationId { get; set; }

        public double NewQuantity { get; set; } = 0;
        public ICommand SaveQuantityCommand { get; set; }
        public GoodsReceiptLotForGoodsReceiptView(IApiService apiService, string goodsReceiptId, string employeeName, string itemClassId, string itemId, string itemName, string lotId, string unit, double quantity, string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, string? locationId)
        {
            _apiService = apiService;
            GoodsReceiptId = goodsReceiptId;
            EmployeeName = employeeName;
            ItemClassId = itemClassId;
            ItemId = itemId;
            ItemName = itemName;
            LotId = lotId;
            Unit = unit;
            Quantity = quantity;
            PurchaseOrderNumber = purchaseOrderNumber;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
            LocationId = locationId;
            
            //SaveQuantityCommand = new RelayCommand(SaveQuantityAsync);
        }
        //private async void SaveQuantityAsync()
        //{
        //    try
        //    {
        //        await _apiService.SetGoodsReceiptLotQuantityAsync(GoodsReceiptId, LotId, NewQuantity);
        //        Quantity = NewQuantity;
        //    }
        //    catch (HttpRequestException)
        //    {
        //        ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
        //    }
        //}
    }
}
