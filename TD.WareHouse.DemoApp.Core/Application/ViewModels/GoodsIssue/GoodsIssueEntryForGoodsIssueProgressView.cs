using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueEntryForGoodsIssueProgressView : BaseViewModel
    {
        private readonly IApiService _apiService;
        public string ItemClassId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public List<GoodsIssueEntryLotForGoodsIssueProgressView> Lots { get; set; }
        public double Quantity { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string Receiver { get; set; }
        public string EmployeeName { get; set; }
        
        
        
        public GoodsIssueEntryForGoodsIssueProgressView(IApiService apiService, string itemClassId, string itemId, string itemName, string unit, List<GoodsIssueEntryLotForGoodsIssueProgressView> lots, double quantity, string purchaseOrderNumber, string receiver, string employeeName   )
        {
            _apiService = apiService;
            ItemClassId = itemClassId;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            Lots = lots;
            Quantity = quantity;
            PurchaseOrderNumber = purchaseOrderNumber;
            Receiver = receiver;
            EmployeeName = employeeName;
        }
    }
}
