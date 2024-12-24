using FluentValidation;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Command.Products;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.Products
{
    public class EFCreateProductCommand : ICreateProductCommand
    {
        private SCContext _context;
        private CreateProductDtoValidator _validator;

        public EFCreateProductCommand(SCContext context, CreateProductDtoValidator validator)
        {
            _context = context;
            _validator = validator;
   
        }
        public int Id => 5;

        public string Name => "Create product";

        public void Execute(CreateProductDto data)
        {
            _validator.ValidateAndThrow(data);

            var tempFile = Path.Combine("wwwroot", "temp", data.ImageSrc);
            var destinationFile = Path.Combine("wwwroot", "posts", data.ImageSrc);
            System.IO.File.Move(tempFile, destinationFile);

            Product product = new Product
            {
                Name = data.Name,
                CategoryId = data.CategoryId,
                Calories = data.Calories,
                ImageSrc = data.ImageSrc,
                InitialPrice=data.InitialPrice
            };

            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}
