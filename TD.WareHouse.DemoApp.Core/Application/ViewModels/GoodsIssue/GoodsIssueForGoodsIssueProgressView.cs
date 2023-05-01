using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class GoodsIssueForGoodsIssueProgressView : BaseViewModel
    {
        private readonly IApiService _apiService;
        public string PurchaseOrderNumber { get; set; }
        public List<GoodsIssueEntryForGoodsIssueProgressView> Entries { get; set; }
        public string Receiver { get; set; }
        public string EmployeeName { get; set; }
        public string GoodsIssueId { get; set; }
        public GoodsIssueForGoodsIssueProgressView(IApiService apiService, string purchaseOrderNumber, List<GoodsIssueEntryForGoodsIssueProgressView> entries, string receiver, string employeeName, string goodsIssueId)
        {
            _apiService = apiService;
            PurchaseOrderNumber = purchaseOrderNumber;
            Entries = entries;
            Receiver = receiver;
            EmployeeName = employeeName;
            GoodsIssueId = goodsIssueId;
        }

       
    }
}

