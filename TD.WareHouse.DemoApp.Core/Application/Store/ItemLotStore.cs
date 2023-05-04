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
        public List<ItemLot> ItemLots { get; private set; }
        public ObservableCollection<string> PurchaseOrderNumbers { get; private set; }
        public ObservableCollection<string> LocationIds { get; private set; }
        public ItemLotStore()
        {
            ItemLots = new List<ItemLot>();
            PurchaseOrderNumbers = new ObservableCollection<string>();
            LocationIds = new ObservableCollection<string>();
        }
        public void SetItemLot(IEnumerable<ItemLot> itemlots)
        {
            ItemLots = itemlots.ToList();
            PurchaseOrderNumbers = new ObservableCollection<string>(ItemLots.Select(i => i.PurchaseOrderNumber).OrderBy(s => s));
            LocationIds = new ObservableCollection<string>(ItemLots.Select(i => i.Location.LocationId).OrderBy(s => s));
        }
    }
    }
