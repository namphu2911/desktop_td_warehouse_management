using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard
{
    public class StockCardNavigationViewModel
    {
        public StockCardViewModel StockCard { get; set; }
        public StockCardExtendedViewModel StockCardExtended { get; set; }
        public StockCardNavigationViewModel(StockCardViewModel stockCard, StockCardExtendedViewModel stockCardExtended)
        {
            StockCard = stockCard;
            StockCardExtended = stockCardExtended;
        }
    }
}
