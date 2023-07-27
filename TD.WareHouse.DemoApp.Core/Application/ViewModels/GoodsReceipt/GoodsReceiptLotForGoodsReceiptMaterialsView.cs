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
    public class GoodsReceiptLotForGoodsReceiptMaterialsView : BaseViewModel
    {
        public ObservableCollection<string>? LocationIds { get; set; }
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string LotId { get; set; }
        public string NewLotId { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsExported { get; set; }
        public string State => (IsExported == true) ? "Đã xuất" : "Chưa xuất";
        public List<GoodsReceiptSublotViewModel> GoodsReceiptSublots { get; set; }

        //
        public string LocationId { get; set; } = "";
        public double QuantityPerLocation { get; set; }
        public ICommand CreateSublotCommand { get; set; }
        public ICommand DeleteLotCommand { get; set; }
        public event Action? OnRemoved;
        public event EventHandler? SublotCreated;
        public GoodsReceiptLotForGoodsReceiptMaterialsView(ObservableCollection<string>? locationIds, string itemClassId, string itemId, string itemName, string lotId, string newLotId, string unit, double quantity, DateTime? productionDate, DateTime? expirationDate, bool isExported, List<GoodsReceiptSublotViewModel> goodsReceiptSublots)
        {
            LocationIds = locationIds;
            ItemClassId = itemClassId;
            ItemId = itemId;
            ItemName = itemName;
            LotId = lotId;
            NewLotId = newLotId;
            Unit = unit;
            Quantity = quantity;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
            IsExported = isExported;
            GoodsReceiptSublots = goodsReceiptSublots;

            CreateSublotCommand = new RelayCommand(CreateSublot);
            DeleteLotCommand = new RelayCommand(DeleteLot);
        }

        private void CreateSublot()
        {
            SublotCreated?.Invoke(this, EventArgs.Empty);
        }

        private void DeleteLot()
        {
            OnRemoved?.Invoke();
        }
    }
}
