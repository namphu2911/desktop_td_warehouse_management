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
        public ObservableCollection<string> WarehouseNames { get; private set; }
        public WarehouseStore()
        {
            Warehouses = new List<Warehouse>();
            WarehouseNames = new ObservableCollection<string>();
        }
        public void SetWarehouse(IEnumerable<Warehouse> warehouses)
        {
            Warehouses = warehouses.ToList();
            WarehouseNames = new ObservableCollection<string>(Warehouses.Select(i => i.WarehouseName).OrderBy(s => s));
        }
    }
}
