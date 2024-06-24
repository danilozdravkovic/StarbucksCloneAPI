using StarbucksClone.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation
{
    public class Actor : IApplicationActor
    {
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public IEnumerable<int> AllowedUseCases { get; set; } 

    }

    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 0;

        public int? RoleId => null;

        public string Username => "unauthorized";

        public string Email => "/";

        public string FirstName => "unauthorized";

        public string LastName => "unauthorized";

        public IEnumerable<int> AllowedUseCases => new List<int> { 1,7};
    }
}
