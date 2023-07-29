using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Models.Employees;

namespace TD.WareHouse.DemoApp.Core.Application.Store
{
    public class EmployeeStore : BaseViewModel
    {
        public List<Employee> Employees { get; private set; }
        public ObservableCollection<string> EmployeeIds { get; private set; }
        public ObservableCollection<string> EmployeeNames { get; private set; }
        public EmployeeStore()
        {
            Employees = new List<Employee>();
            EmployeeIds = new ObservableCollection<string>();
            EmployeeNames = new ObservableCollection<string>();
        }
        public void SetEmployee(IEnumerable<Employee> employees)
        {
            Employees = employees.ToList();
            EmployeeIds = new ObservableCollection<string>(Employees.Select(i => i.EmployeeId).OrderBy(s => s));
            EmployeeNames = new ObservableCollection<string>(Employees.Select(i => i.EmployeeName).OrderBy(s => s));
        }
    }
}
