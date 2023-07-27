using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm
{
    public class LocationsForAlarmViewModel : BaseViewModel
    {
        public string LocationId { get; set; }
        public double QuantityPerLocation { get; set; }
        public LocationsForAlarmViewModel(string locationId, double quantityPerLocation)
        {
            LocationId = locationId;
            QuantityPerLocation = quantityPerLocation;
        }
    }
}
