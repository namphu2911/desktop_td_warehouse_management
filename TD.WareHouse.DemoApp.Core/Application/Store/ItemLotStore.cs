using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;

namespace TD.WareHouse.DemoApp.Core.Application.Store
{
    public class ItemLotStore
    {
        public ObservableCollection<string> FinishedPurchaseOrderNumbers { get; private set; }
        public ItemLotStore()
        {
            FinishedPurchaseOrderNumbers = new ObservableCollection<string>();
        }
       
        public void SetFinishedPurchaseOrderNumbers(List<string> finishedPurchaseOrderNumbersDtos)
        {
            FinishedPurchaseOrderNumbers = new ObservableCollection<string>(finishedPurchaseOrderNumbersDtos.OrderBy(s => s));
        }
    }
}
