using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application.DTO
{
    public class CreateProductDto 
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public int Calories { get; set; }
        public int CategoryId { get; set; }
    }
}
