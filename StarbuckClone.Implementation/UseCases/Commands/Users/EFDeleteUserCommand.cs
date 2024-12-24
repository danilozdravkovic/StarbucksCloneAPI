using StarbuckClone.Domain;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Command.Users;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.Users
{
    public class EFDeleteUserCommand : IDeleteUserCommand
    {
        private SCContext _context;


        public EFDeleteUserCommand(SCContext context)
        {
            _context = context;


        }
        public int Id => 20;

        public string Name => "Soft delete user";

        public void Execute(int data)
        {
            var userForRemoving = _context.Users.Find(data);
            if (userForRemoving == null)
            {
                throw new NotFoundException(typeof(User).ToString(), data);
            }
            userForRemoving.IsActive = false;
            _context.SaveChanges();
        }
    }
}
