using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class AddIn : Entity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual AddIn Parent { get; set; }
        public virtual ICollection<AddIn> Children { get; set; } = new HashSet<AddIn>();
        public virtual ICollection<Product> IncludedProducts { get; set; } = new HashSet<Product>();
        public virtual ICollection<Product> CustomProducts { get; set; } = new HashSet<Product>();
    }
}
