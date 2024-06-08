using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class Product : Entity
    {
        public string ImageSrc { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public int CategoryId { get; set; }
        public decimal InitialPrice { get; set; }

        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<Size> Sizes { get; set; } = new HashSet<Size>();
        public virtual ICollection<AddIn> IncludedAddIns{ get; set; } = new HashSet<AddIn>();
        public virtual ICollection<AddIn> CustomAddIns { get; set; } = new HashSet<AddIn>();
        public virtual ICollection<CartLine> CartLines { get; set; } = new HashSet<CartLine>();
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new HashSet<OrderLine>();
    }
}
