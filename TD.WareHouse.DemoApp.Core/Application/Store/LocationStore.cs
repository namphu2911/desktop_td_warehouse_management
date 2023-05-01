using System.Collections.ObjectModel;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;

namespace TD.WareHouse.DemoApp.Core.Application.Store
{
    public class LocationStore
    {
        public List<Location> Locations { get; private set; }
        public ObservableCollection<string> LocationIds { get; private set; }
        public LocationStore()
        {
            Locations = new List<Location>();
            LocationIds = new ObservableCollection<string>();
        }

        public void SetLocation(IEnumerable<Location> locations)
        {
            Locations = locations.ToList();
            LocationIds = new ObservableCollection<string>(locations.Select(e => e.LocationId).OrderBy(s => s));
        }
    }
}
