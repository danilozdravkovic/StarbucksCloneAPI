﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }


        public virtual Role Role { get; set; }

        public virtual ICollection<CartLine> OrderLines { get; set; } = new HashSet<CartLine>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<UserUseCase> UseCases { get; set; } = new HashSet<UserUseCase>();
    }
}
