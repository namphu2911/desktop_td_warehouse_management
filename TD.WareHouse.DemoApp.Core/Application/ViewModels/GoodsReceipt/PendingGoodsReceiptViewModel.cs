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
    public class PendingGoodsReceiptViewModel : BaseViewModel
    {
        public string GoodsReceiptId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EmployeeName { get; set; }
       
        public PendingGoodsReceiptViewModel(string goodsReceiptId, DateTime timestamp, string employeeName)
        {
            GoodsReceiptId = goodsReceiptId;
            Timestamp = timestamp;
            EmployeeName = employeeName;
        }
    }
}
