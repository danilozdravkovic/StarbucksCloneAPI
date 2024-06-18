using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application.DTO
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductCategoryDto> Children { get; set; }
    }

    public class ProductCategorySearchDto : PagedSearchDto
    {
        public int?  ParentId { get; set; }
        public string Name { get; set; }
    }

}
