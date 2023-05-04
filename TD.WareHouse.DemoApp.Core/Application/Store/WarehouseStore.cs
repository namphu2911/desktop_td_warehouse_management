using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.Warehouses;

namespace TD.WareHouse.DemoApp.Core.Application.Store
{
    public class WarehouseStore
    {
        public List<Warehouse> Warehouses { get; private set; }
        public ObservableCollection<string> WarehouseIds { get; private set; }
        public WarehouseStore()
        {
            Warehouses = new List<Warehouse>();
            WarehouseIds = new ObservableCollection<string>();
        }
        public void SetWarehouse(IEnumerable<Warehouse> warehouses)
        {
            Warehouses = warehouses.ToList();
            WarehouseIds = new ObservableCollection<string>(Warehouses.Select(i => i.WarehouseId).OrderBy(s => s));
        }
    }
}
