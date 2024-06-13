using FluentValidation;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Commands.Users;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.Users
{
    public class EFRegisterUserCommand : IRegisterUserCommand
    {
        public int Id => 1;

        public string Name => "User registration";

        private SCContext _context;
        private RegisterUserDtoValidator _validator;

        public EFRegisterUserCommand(SCContext context, RegisterUserDtoValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(RegisterUserDto data)
        {
            _validator.ValidateAndThrow(data);

            User user = new User
            {
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Username = data.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
