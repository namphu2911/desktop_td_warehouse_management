using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;

namespace TD.WareHouse.DemoApp.Core.Application.Store
{
    public class DepartmentStore
    {
        public List<Department> Departments { get; private set; }
        public ObservableCollection<string> Names { get; private set; }
        public DepartmentStore()
        {
            Departments = new List<Department>();
            Names = new ObservableCollection<string>();
        }
        public void SetDepartment(IEnumerable<Department> departments)
        {
            Departments = departments.ToList();
            Names = new ObservableCollection<string>(Departments.Select(i => i.Name).OrderBy(s => s));
        }
    }
}
