﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.MiscellaneousData
{
    public class MiscellaneousDataViewModel
    {
        public ManageItemViewModel ManageItem { get; set; }
        public MiscellaneousDataViewModel(ManageItemViewModel manageItem)
        {
            ManageItem = manageItem;
        }
    }
}
