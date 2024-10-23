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
            var product = _context.Products.Include(p=>p.Category)
                                                   .Include(p=>p.Sizes)
                                                   .Include(p => p.IncludedProductAddIns)         
                                                   .ThenInclude(ipa => ipa.AddIn)
                                                   .FirstOrDefault(p => p.Id == search.Id);
            if (product == null)
            {
                throw new NotFoundException(typeof(Product).ToString(), search.Id);
            }

            var productToReturn =new ProductDto
            {
                Name = product.Name,
                Id = product.Id,
                ImageSrc = product.ImageSrc,
                Category = product.Category.Name,
                Calories = product.Calories,
                Price = product.InitialPrice,
                CategoryId = product.CategoryId,
                Sizes = product.Sizes.Select(x => new ProductSizeDto
                {
                    Id=x.Id,
                    Name = x.Name,
                    Size = x.SizeVolume,
                    AdditionalCalories = x.AdditionalCalories
                }).ToList(),
                IncludedAddIns = product.IncludedProductAddIns.Where(ipa=>ipa.AddIn.ParentId==null).Select(x => new ProductIncludedAddInDto
                {
                    Id = x.AddInId,
                    Name = x.AddIn.Name,
                    Pump = null,
                    Selected = false,
                }).ToList()
            
            };


            foreach(var addIn in productToReturn.IncludedAddIns)
            {
                FillChildAddIns(addIn);
            }


            return productToReturn;
        }

        private void FillChildAddIns(ProductIncludedAddInDto addIn)
        {
            var id = addIn.Id;

            var children = _context.AddIns.Where(p => p.ParentId == id).Select(x => new ProductIncludedAddInDto
            {
                Id = x.Id,
                Name = x.Name,
                Pump = _context.IncludedProductAddIns.Where(ipa=>ipa.SelectedId==x.Id).FirstOrDefault().Pump,
                Selected = _context.IncludedProductAddIns.Any(ipa=>ipa.SelectedId==x.Id)
            }).ToList();


            addIn.Children = children.Any() ? children : null;

            if (addIn.Children != null)
            {
                foreach (var child in addIn.Children)
                {
                    FillChildAddIns(child);
                }
            }

        }
    }
}
