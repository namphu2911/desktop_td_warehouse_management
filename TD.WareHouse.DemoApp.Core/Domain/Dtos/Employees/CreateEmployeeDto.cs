using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees
{
    public class CreateEmployeeDto
    {
        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public CreateEmployeeDto(string employeeId, string employeeName)
        {
            this.employeeId = employeeId;
            this.employeeName = employeeName;
        }
    }
}
