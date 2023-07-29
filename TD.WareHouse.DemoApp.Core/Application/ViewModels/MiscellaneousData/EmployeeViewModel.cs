using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.MiscellaneousData
{
    public class EmployeeViewModel
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public EmployeeViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
        public EmployeeViewModel(string employeeId, string employeeName)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
        }
    }
}
