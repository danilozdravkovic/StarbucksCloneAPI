using FluentValidation;
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
        private SCContext context;
        public RegisterUserDtoValidator(SCContext context) {
            this.context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email can't be empty.")
                                 .EmailAddress().WithMessage("Email is not in right format.")
                                 .Must(x => !context.Users.Any(u => u.Email == x)).WithMessage("Email is already in use.");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name can't be empty.")
                                     .MinimumLength(3).WithMessage("First name must be at lesat 3 characters long.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name can't be empty.")
                                     .MinimumLength(3).WithMessage("Last name must be at lesat 3 characters long.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password name can't be empty.")
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

            return context.Roles.Any(p => p.Id == roleId && p.IsActive);
        }
    }
}
