using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public bool isDefault { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
        public virtual ICollection<RoleUseCase> UseCases { get; set; } = new HashSet<RoleUseCase>();
    }
}
