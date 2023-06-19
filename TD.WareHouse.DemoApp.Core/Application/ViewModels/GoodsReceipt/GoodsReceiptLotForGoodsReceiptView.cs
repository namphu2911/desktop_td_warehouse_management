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
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptLotForGoodsReceiptView : BaseViewModel
    {
        public ObservableCollection<string>? LocationIds { get; set; }
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
        public GoodsReceiptLotForGoodsReceiptView(ObservableCollection<string>? locationIds, string itemClassId, string itemId, string itemName, string lotId, string unit, double quantity, string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, string? locationId)
        {
            LocationIds = locationIds;
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
        }
    }
}
