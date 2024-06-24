using FluentValidation;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Commands.Products;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.Products
{
    public class EFModifyProductCommand : IModifyProductCommand
    {
        private readonly SCContext _context;
        private readonly ModifyProductDtoValidator _validator;


        public EFModifyProductCommand(SCContext context, ModifyProductDtoValidator validator)
        {
            _context = context;
            _validator = validator;

        }
        public int Id => 19;

        public string Name => "Modify product";

        public void Execute(ModifyProductDto data)
        {
            var productForModification = _context.Products.Find(data.Id);
            if (productForModification == null)
            {
                throw new NotFoundException(typeof(Product).ToString(), data.Id);
            }

            var tempFile = Path.Combine("wwwroot", "temp", data.ImageSrc);
            if (File.Exists(tempFile))
            {
                var destinationFile = Path.Combine("wwwroot", "posts", data.ImageSrc);
                System.IO.File.Move(tempFile, destinationFile);
            }

            _validator.ValidateAndThrow(data);

               
               


            productForModification.Name = data.Name;
            productForModification.InitialPrice = data.InitialPrice;
            productForModification.CategoryId = data.CategoryId;
            productForModification.ImageSrc = data.ImageSrc;
            productForModification.Calories = data.Calories;

            _context.SaveChanges();
        }
    }
}
