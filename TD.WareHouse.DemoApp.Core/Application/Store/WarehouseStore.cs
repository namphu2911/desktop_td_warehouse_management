using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;
using TD.WareHouse.DemoApp.Core.Domain.Models.Warehouses;

namespace TD.WareHouse.DemoApp.Core.Application.Store
{
    public class WarehouseStore
    {
        public List<Warehouse> Warehouses { get; private set; }
        public List<Location> Locations { get; private set; }
        public ObservableCollection<string> WarehouseIds { get; private set; }
        public ObservableCollection<string> LocationIds { get; private set; }
        public WarehouseStore()
        {
            Warehouses = new List<Warehouse>();
            Locations = new List<Location>();
            WarehouseIds = new ObservableCollection<string>();
            LocationIds = new ObservableCollection<string>();
        }
        public void SetWarehouse(IEnumerable<Warehouse> warehouses)
        {
            Warehouses = warehouses.ToList();
            WarehouseIds = new ObservableCollection<string>(Warehouses.Select(i => i.WarehouseId).OrderBy(s => s));
            LocationIds = new ObservableCollection<string>(Warehouses.SelectMany(i => i.Locations.Select(g => g.LocationId)).OrderBy(s => s));
        }
    }
}
