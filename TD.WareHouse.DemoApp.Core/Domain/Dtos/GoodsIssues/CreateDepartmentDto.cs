using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class CreateDepartmentDto
    {
        public string name { get; set; }
        public CreateDepartmentDto(string name)
        {
            this.name = name;
        }
    }
}
