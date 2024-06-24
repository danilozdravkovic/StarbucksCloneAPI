using FluentValidation;
using StarbuckClone.Implementation.Extensions;
using StarbucksClone.Application.DTO;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        private SCContext _context;
        public RegisterUserDtoValidator(SCContext context) {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email can't be empty.")
                                 .EmailAddress().WithMessage("Email is not in right format.")
                                 .Must(x => !context.Users.Any(u => u.Email == x)).WithMessage("Email is already in use.");

            RuleFor(x => x.FirstName).FirstNameMustBeValid();

            RuleFor(x => x.LastName).LastNameMustBeValid();

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can't be empty.")
                                    .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$").WithMessage("Password must be at least 8 characters long, must contain one uppercase  letter,one lowercase letter and one number.");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username name can't be empty.")
                                  .Matches("^(?=.{3,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$").WithMessage("Username can't contain _ or . and must be between 3 and 20 chatacters long")
                                  .Must(x => !context.Users.Any(u => u.Username == x)).WithMessage("Username is already in use");

            RuleFor(x => x.RoleId).Must(DoesRoleExistsWhenRoleIdNotNull).WithMessage("Role id does not exist.");
        }

        private bool DoesRoleExistsWhenRoleIdNotNull(int? roleId)
        {
            if (!roleId.HasValue)
            {
                return true;
            }

            return _context.Roles.Any(p => p.Id == roleId && p.IsActive);
        }
    }

    public class ModifyUserDtoValidator : AbstractValidator<ModifyUserDto>
    {
        private SCContext _context;
        public ModifyUserDtoValidator(SCContext context)
        {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email can't be empty.")
                                 .EmailAddress().WithMessage("Email is not in right format.")
                                 .Must((dto,x)=> !_context.Users.Any(u => u.Email == x && u.Id!=dto.Id)).WithMessage("Email is already in use.");

            RuleFor(x => x.FirstName).FirstNameMustBeValid();

            RuleFor(x => x.LastName).LastNameMustBeValid();

            RuleFor(x => x.Password).Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$")
                                    .When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Password must be at least 8 characters long, must contain one uppercase  letter,one lowercase letter and one number.");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username name can't be empty.")
                                  .Matches("^(?=.{3,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$").WithMessage("Username can't contain _ or . and must be between 3 and 20 chatacters long")
                                  .Must((dto,x) => !_context.Users.Any(u => u.Username == x && u.Id!=dto.Id)).WithMessage("Username is already in use");




        }

       
    }
}
