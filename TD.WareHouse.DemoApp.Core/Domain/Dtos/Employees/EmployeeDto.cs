using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees
{
    public class EmployeeDto
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public EmployeeDto(string employeeId, string employeeName)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
        }
    }
}
