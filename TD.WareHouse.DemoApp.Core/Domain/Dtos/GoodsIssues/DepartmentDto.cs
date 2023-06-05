using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class DepartmentDto
    {
        public string Name { get; set; }
        public DepartmentDto(string name)
        {
            Name = name;
        }
    }
}
