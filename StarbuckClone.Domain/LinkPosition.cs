using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class LinkPosition : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<NavigationLink> NavigationLinks { get; set; } = new HashSet<NavigationLink>();
    }
}
