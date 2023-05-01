using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm
{
    public class AlarmViewModel : BaseViewModel
    {
        public ExpirationDateAlarmViewModel ExpiryDateAlarmViewModel { get; set; }
        public QuantityAlarmViewModel QuantityAlarmViewModel { get; set; }
        public AlarmViewModel(ExpirationDateAlarmViewModel expiryDateAlarmViewModel, QuantityAlarmViewModel quantityAlarmViewModel)
        {
            ExpiryDateAlarmViewModel = expiryDateAlarmViewModel;
            QuantityAlarmViewModel = quantityAlarmViewModel;
        }
    }
}
