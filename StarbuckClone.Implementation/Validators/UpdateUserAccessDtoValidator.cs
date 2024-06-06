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
    public class UpdateUserAccessDtoValidator : AbstractValidator<UpdateUserAccessDto>
    {
        public UpdateUserAccessDtoValidator(SCContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.UserId)
                    .Must(x => context.Users.Any(u => u.Id == x && u.IsActive))
                    .WithMessage("Requested user doesn't exist.");
  
            RuleFor(x => x.UseCaseIds)
                .NotEmpty().WithMessage("Parameter is required.")
                .Must(x => x.All(id => id > 0 && id <= UseCaseInfo.MaxUseCaseId)).WithMessage("Invalid usecase id range.")
                .Must(x => x.Distinct().Count() == x.Count()).WithMessage("Only unique usecase ids must be delivered.");


        }
    }
}
