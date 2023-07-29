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

        public List<Item> FinishedItems { get; private set; }
        public ObservableCollection<string> FinishedItemIds { get; private set; }
        public ObservableCollection<string> FinishedItemNames { get; private set; }
        public ObservableCollection<string> FinishedItemUnits { get; private set; }

        public List<Item> AllItems { get; private set; }
        public ObservableCollection<string> AllItemIds { get; private set; }
        public ObservableCollection<string> AllItemNames { get; private set; }
        public ObservableCollection<string> AllUnits { get; private set; }
        public ItemStore()
        {
            Items = new List<Item>();
            ItemIds = new ObservableCollection<string>();
            ItemNames = new ObservableCollection<string>();
            Units = new ObservableCollection<string>();

            FinishedItems = new List<Item>();
            FinishedItemIds = new ObservableCollection<string>();
            FinishedItemNames = new ObservableCollection<string>();
            FinishedItemUnits = new ObservableCollection<string>();

            AllItems = new List<Item>();
            AllItemIds = new ObservableCollection<string>();
            AllItemNames = new ObservableCollection<string>();
            AllUnits = new ObservableCollection<string>();
        }
        public void SetItem(IEnumerable<Item> items)
        {
            Items = items.Where(i => i.ItemClassId != "TP").ToList();
            ItemIds = new ObservableCollection<string>(Items.Select(i => i.ItemId).OrderBy(s => s));
            ItemNames = new ObservableCollection<string>(Items.Select(i => i.ItemName).OrderBy(s => s));
            Units = new ObservableCollection<string>(Items.Select(i => i.Unit).Distinct().OrderBy(s => s));

            FinishedItems = items.Where(i => i.ItemClassId == "TP").ToList();
            FinishedItemIds = new ObservableCollection<string>(FinishedItems.Select(i => i.ItemId).OrderBy(s => s));
            FinishedItemNames = new ObservableCollection<string>(FinishedItems.Select(i => i.ItemName).OrderBy(s => s));
            FinishedItemUnits = new ObservableCollection<string>(FinishedItems.Select(i => i.Unit).Distinct().OrderBy(s => s));

            AllItems = items.ToList();
            AllItemIds = new ObservableCollection<string>(AllItems.Select(i => i.ItemId).OrderBy(s => s));
            AllItemNames = new ObservableCollection<string>(AllItems.Select(i => i.ItemName).OrderBy(s => s));
            AllUnits = new ObservableCollection<string>(AllItems.Select(i => i.Unit).Distinct().OrderBy(s => s));
        }
    }
}
