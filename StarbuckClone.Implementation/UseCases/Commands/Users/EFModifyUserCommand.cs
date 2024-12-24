using FluentValidation;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application.DTO;
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
    public class EFModifyUserCommand : IModifyUserCommand
    {
        private readonly SCContext _context;
        private readonly ModifyUserDtoValidator _validator;


        public EFModifyUserCommand(SCContext context, ModifyUserDtoValidator validator)
        {
            _context = context;
            _validator = validator;

        }
        public int Id => 22;

        public string Name => "Modify user";

        public void Execute(ModifyUserDto data)
        {
            var userForModification = _context.Users.Find(data.Id);
            if (userForModification == null)
            {
                throw new NotFoundException(typeof(User).ToString(), data.Id);
            }

            _validator.ValidateAndThrow(data);


            userForModification.Username = data.Username;
            userForModification.Email = data.Email;
            userForModification.FirstName = data.FirstName;
            userForModification.LastName = data.LastName;

            if (!string.IsNullOrEmpty(data.Password))
            {
                userForModification.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            }


            _context.SaveChanges();
        }
    }
}
