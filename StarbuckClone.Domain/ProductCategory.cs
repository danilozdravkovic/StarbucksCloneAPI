using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class ProductCategory : Entity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual ProductCategory Parent { get; set; }
        public virtual ICollection<ProductCategory> Children { get; set; } = new HashSet<ProductCategory>();
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
