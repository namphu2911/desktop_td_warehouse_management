using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues
{
    public class Department
    {
        public string Name { get; set; }
        public Department(string name)
        {
            Name = name;
        }
    }
}
