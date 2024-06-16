using StarbucksClone.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation
{
    public class DefaultActorProvider : IApplicationActorProvider
    {
        public IApplicationActor GetActor()
        {
            return new Actor
            {
                Username = "test",
                Email = "test",
                Id = 9,
                FirstName = "Test",
                LastName = "Test",
                AllowedUseCases = new List<int> { 1,2,3,4,5,6,7,8,9,10,11,12}
            };
        }
    }
}
