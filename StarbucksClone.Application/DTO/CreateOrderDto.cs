using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application.DTO
{
    public class CreateOrderDto
    { 
        public string Address { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
