using FluentValidation;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Command.Users;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.Users
{
    public class EFUpdateUserAccessCommand : IUpdateUserAccessCommand
    {
        private SCContext _context;
        private UpdateUserAccessDtoValidator _validator;

        public EFUpdateUserAccessCommand(SCContext context, UpdateUserAccessDtoValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 4;

        public string Name => "Change user access";

        public void Execute(UpdateUserAccessDto data)
        {
            _validator.ValidateAndThrow(data);

            var userUseCases = _context.UserUseCases.Where(x => x.UserId == data.UserId).ToList();
            _context.UserUseCases.RemoveRange(userUseCases);

            _context.UserUseCases.AddRange(data.UseCaseIds.Select(x=> new Domain.UserUseCase
            {
                UserId=data.UserId,
                UseCaseId=x
            }));

            _context.SaveChanges();
        }
    }
}
