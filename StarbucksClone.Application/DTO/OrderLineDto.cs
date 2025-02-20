using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application.DTO
{
    public class OrderLineDto
    {
        public int ProductId { get; set; }
        public int OrderLineId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string ProductSize { get; set; }
        public int ProductSizeId { get; set; }

        public IEnumerable<GetingAddInForCartDto> AddIns { get; set; }
    }
  
}
