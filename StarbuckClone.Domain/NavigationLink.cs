using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class NavigationLink : Entity
    {
        public string Name { get; set; }
        public string LinkHref { get; set; }
        public int? ParentId { get; set; }
        public int PositionId { get; set; }

        public virtual NavigationLink Parent { get; set; }
        public virtual ICollection<NavigationLink> Children { get; set; } = new HashSet<NavigationLink>();
        public virtual LinkPosition LinkPosition { get; set; }
    }
}
