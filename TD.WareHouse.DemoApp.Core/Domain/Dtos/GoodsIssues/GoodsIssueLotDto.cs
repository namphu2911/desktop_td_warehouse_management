using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues
{
    public class GoodsIssueLotDto
    {
        public string GoodsIssueLotId { get; set; }
        public double Quantity { get; set; }
        public string Note { get; set; }
        public EmployeeDto Employee { get; set; }
        public GoodsIssueLotDto(string goodsIssueLotId, double quantity, string note, EmployeeDto employee)
        {
            GoodsIssueLotId = goodsIssueLotId;
            Quantity = quantity;
            Note = note;
            Employee = employee;
        }
    }
}
