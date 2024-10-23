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

        public decimal InitialPrice { get; set; }
    }

    public class ModifyProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public int Calories { get; set; }
        public int CategoryId { get; set; }

        public decimal InitialPrice { get; set; }
    }

    public class ProductSearchDto  : PagedSearchDto
    {
        public string Name { get; set; }

        public int? CategoryId { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public int Calories { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public IEnumerable<ProductSizeDto> Sizes { get; set; }
        public IEnumerable<ProductIncludedAddInDto> IncludedAddIns { get; set; }
    }

    public class ProductSizeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int? AdditionalCalories { get; set; }
    }

    public class ProductIncludedAddInDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Pump { get; set; }
        public bool Selected { get; set; }
        public IEnumerable<ProductIncludedAddInDto> Children { get; set; }

    }

    public class IDProductDto
    {
        public int Id { get; set; }
    }
}
