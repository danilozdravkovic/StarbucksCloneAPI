using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class Size : Entity
    {
        public string Name { get; set; }
        public int SizeVolume { get; set; }
        public int AdditionalCalories { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
