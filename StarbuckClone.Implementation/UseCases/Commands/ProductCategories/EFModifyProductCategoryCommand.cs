using FluentValidation;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Command.ProductCategories;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.ProductCategories
{
    public class EFModifyProductCategoryCommand : IModifyProductCategoryCommand
    {
        private readonly SCContext _context;
        private readonly ModifyProductCategoryDtoValidator _validator;


        public EFModifyProductCategoryCommand(SCContext context, ModifyProductCategoryDtoValidator validator)
        {
            _context = context;
            _validator = validator;

        }
        public int Id => 25;

        public string Name => "Modify product category";

        public void Execute(ModifyProductCategoryDto data)
        {
            var productCatForModification = _context.ProductCategories.Find(data.Id);
            if (productCatForModification == null)
            {
                throw new NotFoundException(typeof(ProductCategory).ToString(), data.Id);
            }

            _validator.ValidateAndThrow(data);


            productCatForModification.Name = data.Name;
            productCatForModification.ParentId = data.ParentId;
            productCatForModification.Children = _context.ProductCategories.Where(p => data.ChildIds.Contains(p.Id)).ToList();

            _context.SaveChanges();
        }
    }
}
