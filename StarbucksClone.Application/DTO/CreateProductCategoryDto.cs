using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application.DTO
{

    public class BaseProductCategoryDto
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<int> ChildIds { get; set; }
    }
    public class CreateProductCategoryDto : BaseProductCategoryDto
    {
     
    }

    public class ModifyProductCategoryDto : BaseProductCategoryDto
    {
        public int Id { get; set; }
       
    }


}
