using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class StockCardNavigationViewModel : BaseViewModel
    {
        public StockCardViewModel StockCard { get; set; }
        public StockCardExtendedViewModel StockCardExtended { get; set; }
        public FinishedProductStockCardExtendedViewModel FinishedProductStockCardExtended { get; set; }
        public StockCardNavigationViewModel(StockCardViewModel stockCard, StockCardExtendedViewModel stockCardExtended, FinishedProductStockCardExtendedViewModel finishedProductStockCardExtended)
        {
            StockCard = stockCard;
            StockCardExtended = stockCardExtended;
            FinishedProductStockCardExtended = finishedProductStockCardExtended;
        }
    }
}
