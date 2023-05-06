using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;

namespace TD.WareHouse.DemoApp.Core.Application.Store
{
    public class ItemStore : BaseViewModel
    {
        public List<Item> Items { get; private set; }
        public ObservableCollection<string> ItemIds { get; private set; }
        public ObservableCollection<string> ItemNames { get; private set; }
        public ObservableCollection<string> Units { get; private set; }
        public ItemStore()
        {
            Items = new List<Item>();
            ItemIds = new ObservableCollection<string>();
            ItemNames = new ObservableCollection<string>();
            Units = new ObservableCollection<string>();
        }
        public void SetItem(IEnumerable<Item> items)
        {
            Items = items.ToList();
            ItemIds = new ObservableCollection<string>(Items.Select(i => i.ItemId).OrderBy(s => s));
            ItemNames = new ObservableCollection<string>(Items.Select(i => i.ItemName).OrderBy(s => s));
            Units = new ObservableCollection<string>(Items.Select(i => i.Unit).OrderBy(s => s));
        }

    }
}
