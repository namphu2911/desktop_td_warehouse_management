using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class StockCardEntryViewModel : BaseViewModel
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string LotId { get; set; }
        public double BeforeQuantity { get; set; }
        public double ChangedQuantity { get; set; }
        public double ReceiptQuantity
        {
            get
            {
                if (ChangedQuantity > 0)
                    return ChangedQuantity;
                else
                    return 0;
            }
        }

        public double IssueQuantity
        {
            get
            {
                if (ChangedQuantity < 0)
                    return ChangedQuantity;
                else
                    return 0;
            }
        }
        public double AfterQuantity => BeforeQuantity + ChangedQuantity;
        public double MinimumStockLevel { get; set; }
        public string ItemClassId { get; set; }
        public double TimeLeft { get; set; }
        public StockCardEntryViewModel(string itemId, string itemName, string unit, string lotId, double beforeQuantity, double changedQuantity, double minimumStockLevel, string itemClassId, double timeLeft)
        {
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            LotId = lotId;
            BeforeQuantity = beforeQuantity;
            ChangedQuantity = changedQuantity;
            MinimumStockLevel = minimumStockLevel;
            ItemClassId = itemClassId;
            TimeLeft = timeLeft;
        }
    }
}
