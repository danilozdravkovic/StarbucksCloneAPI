using Microsoft.EntityFrameworkCore;
using StarbuckClone.Domain;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Queries.Products;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Queries.Products
{
    public class EFGetProductQuery : IGetProductQuery
    {
        public readonly SCContext _context;

        public EFGetProductQuery(SCContext context)
        {
            _context = context;
        }
        public int Id => 18;

        public string Name => "Get product";

        public ProductDto Execute(IDProductDto search)
        {
            var productToReturn = _context.Products.Include(p=>p.Category).FirstOrDefault(p => p.Id == search.Id);
            if (productToReturn == null)
            {
                throw new NotFoundException(typeof(Product).ToString(), search.Id);
            }

            return new ProductDto
            {
                Name = productToReturn.Name,
                Id = productToReturn.Id,
                ImageSrc = productToReturn.ImageSrc,
                Category = productToReturn.Category.Name,
                Calories = productToReturn.Calories,
                Price = productToReturn.InitialPrice,
                CategoryId=productToReturn.CategoryId
            };
        }
    }
}
